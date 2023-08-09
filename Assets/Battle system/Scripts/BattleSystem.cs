using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public enum BattleState { START, PLAYERTURN, INPUTspell, WAIT, ENEMYTURN, WON, LOST }

/*Creator's Notes:
if further down the Line we decide to add Paterner's or multuiple other enemy's in one battle
we have to adjust Battle States and how many Units that this script has accest too

moving forward, please be mindfull to all of the aspect that go into having another unit in the battle 
 */

public class BattleSystem : MonoBehaviour
{
    //this is where the system pulls in our prefab units that will fight
    public GameObject PlayerPrefab;
    public GameObject EnemyPrefab;

    //this is the position they will spawn in at
    public Transform PlayerSpawnPoint;
    public Transform EnemySpawnPoint;

    //this will determin what is happening in the battle
    public BattleState state;

    //this is the unit script reference of the prefabs we are using
    Unit PlayerUnit;
    Unit EnemyUnit;

    //this is the dialogue on the Main UI
    public Text BattleDialogueText;

    //this is the unit HUD for level and HP
    public BattleHUD PlayerHUD;
    public BattleHUD EnemyHUD;

    //Dez's Combat Script
    public DezCombat DezCombat;
    public bool InputOpportunity = true;
    public UnityEvent InputSpell;



    void Start()
    {
        //we start the battle!
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    private void Update()
    {
       
    }

    IEnumerator PlayerInputAttack()
    {
        state = BattleState.INPUTspell;
        InputOpportunity = true;


        Debug.Log("Input attack");
        InputSpell.Invoke();

        //display text
        BattleDialogueText.text = "Cast Your Spell!";

        yield return new WaitUntil(() => InputOpportunity == false);

        InputOpportunity = true;

        StartCoroutine(PlayerAction());

    }

    //this is to establish the setup
    IEnumerator SetupBattle()
    {
        //this along with EmemyGO just instantiate the prefabs and the unit script attached to them
        GameObject PlayerGO = Instantiate(PlayerPrefab, PlayerSpawnPoint);
        PlayerUnit = PlayerGO.GetComponent<Unit>();

        GameObject EnemyGo = Instantiate(EnemyPrefab, EnemySpawnPoint);
        EnemyUnit = EnemyGo.GetComponent<Unit>();

        //the introductuion text
        BattleDialogueText.text = "You face off against... " + EnemyUnit.unitName;

        //establsihes each HUD for the player and the enemy
        PlayerHUD.SetHUD(PlayerUnit);
        EnemyHUD.SetHUD(EnemyUnit);

        //waits 2 seconds
        yield return new WaitForSeconds(2f);

        //begins the battle with the player first
        //coder's note thinking about adding a starting if statement that
        //determins whose turn it is based on a speed stat
        state = BattleState.PLAYERTURN;
        PlayerTurn();

    }

    IEnumerator PlayerAction()
    {

        //Damage the Enemy
        bool isDead = EnemyUnit.TakeDamage(PlayerUnit.Damage);

        //update the EnemyHUD 
        EnemyHUD.SetHP(EnemyUnit.CurrentHP);

        //display the Enemy got hit
        BattleDialogueText.text = "The Attack Hit";

        //sets it to WAIT so it can move to the Enemy's turn, that way the player doesn't spawm the action button
        state = BattleState.WAIT;

        //wait 1 second
        yield return new WaitForSeconds(2f);

        //check if the enemy is dead and
        //change state based on what happened

        if (isDead) //if the enemy is dead
        {
            state = BattleState.WON;
            EndBattle();
        }
        else //if the enemy is still alive
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        
    }

    IEnumerator PlayerHeal()
    {
        //heals the player by 1 point
        PlayerUnit.Heal(1);

        //update the health on the health bar (PlayerHUD)
        PlayerHUD.SetHP(PlayerUnit.CurrentHP);
        BattleDialogueText.text = PlayerUnit.unitName + " gains 1 health point back";

        //wait 2 seconds
        yield return new WaitForSeconds(2f);

        //moves on to the next turn
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        /*Creator's Notes:
        Please be mindfull that further down the line this function might
        be edited as the possible of enemy AI might be added in so that the battles
        are more fresh and replayable
         */

        //Text display the enemy is attacking
        BattleDialogueText.text = EnemyUnit.unitName + " Attacks!";

        //wait 1 second
        yield return new WaitForSeconds(1f);

        //Damage the player
        bool isDead = PlayerUnit.TakeDamage(EnemyUnit.Damage);

        //update the damage on health bar (PlayerHUD)
        PlayerHUD.SetHP(PlayerUnit.CurrentHP);

        //wait 1 second
        yield return new WaitForSeconds(1f);

        //check if the player is dead
        if (isDead)
        {
            //if the player is dead set the game condition to lost
            state = BattleState.LOST;
            //end the battle
            EndBattle();
        }
        //if the player is not dead however
        else
        {
            //return to the game flow from enemy trun to player turn
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void EndBattle()
    {
        //if the player won and the enemy has 0 HP
        if (state == BattleState.WON)
        {
            BattleDialogueText.text = "You won Against " + EnemyUnit.unitName + " Great job.";
        }
        //if the player lost and with 0 HP
        else if (state == BattleState.LOST)
        {
            BattleDialogueText.text = "You lost Against " + EnemyUnit.unitName + "... what a great shame.";
        }
    }

    void PlayerTurn()
    {
        //this is the display text that indicates when it is the Player's turn
        BattleDialogueText.text = "Choose Your next move Wisely...";
    }

    public void OnAtionButton()
    {
        //just a simple check to make sure it's the player's turn before doing these actions
        if (state != BattleState.PLAYERTURN)
            return;

        //uses the action function
        StartCoroutine(PlayerAction());
    }
    public void OnHealButton()
    {
        //just a simple check to make sure it's the player's turn before doing these actions
        if (state != BattleState.PLAYERTURN)
            return;

        //uses the Heal function
        StartCoroutine(PlayerHeal());
    }


}

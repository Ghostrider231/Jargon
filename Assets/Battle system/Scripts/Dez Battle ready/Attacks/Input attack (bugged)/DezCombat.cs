using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DezCombat : MonoBehaviour
{
    public List<AttackSO> combo;
    float lastClickedTime;
    float lastComboEnd;
    int ComboCounter;
    public int Damage = 0;
    public BattleSystem battleSystem;
    public bool HELP;


    public BattleState state;
    Animator Animator;
    //[SerializeField] Unit Unit;

    void Start()
    {
        Animator = GetComponent<Animator>();
        Animator.SetTrigger("Battle Start");
    }

    void Update()
    {
        //check if the input opportunity is true
        if (battleSystem.InputOpportunity)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("R was pressed");
                Attack();
            }
            ExitAttack();

            if (Damage == 3)
            {
                battleSystem.InputOpportunity = false;
                //Set the input opportunity to false
            }
        }

    }

    public void Attack()
    {
        //                           the 0.2 can be changed with a public float later if I want to edit the timing
        if (Time.time - lastComboEnd > 0.2f && ComboCounter <= combo.Count)
        {
            CancelInvoke("EndCombo");
            //                           the 0.2 can be changed with a public float later if I want to edit the timing
            if (Time.time - lastClickedTime >= 0.2f)
            {
                Animator.runtimeAnimatorController = combo[ComboCounter].animatorOV;
                Animator.Play("Attack", 0, 0);
                
                ComboCounter++;
                Damage = ComboCounter;
                lastClickedTime = Time.time;

                if(ComboCounter > combo.Count)
                {
                    ComboCounter = 0;
                }
            }
        }
        
    }

    public void ExitAttack()
    {
        if(Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && Animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            Invoke("EndCombo", 1);
        }
    }

    public void EndCombo()
    {
        ComboCounter = 0;
        lastComboEnd = Time.time;
    }






}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text NameText;
    public Text RankText;
    public Slider HPslider;

    public void SetHUD(Unit unit)
    {
        NameText.text = unit.unitName;
        RankText.text = "Rank: " + unit.unitLevel;
        HPslider.maxValue = unit.maxHP;
        HPslider.value = unit.CurrentHP;

    }

    public void SetHP(int HP)
    {
        HPslider.value = HP;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUnit : BattleUnit
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI atText;
    public override void Setup(Battler battler)
    {
        base.Setup(battler);
        //plare:ステータスの設定
        nameText.text = battler.Base.Name;
        levelText.text = $"Level:{ battler.Level}";
        hpText.text = $"HP:{battler.HP}/{battler.MaxHP}";
        atText.text = $"AT:{battler.AT}";
    }

    public override void UpdateUI()
    {
        levelText.text = $"Level:{Battler.Level}";
        hpText.text = $"HP:{Battler.HP}/{Battler.MaxHP}";
        atText.text = $"AT:{Battler.AT}";
    }
}

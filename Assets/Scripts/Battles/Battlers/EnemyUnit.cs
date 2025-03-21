using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class EnemyUnit : BattleUnit
{
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI nameText;
    public override void Setup(Battler battler)
    {
        base.Setup(battler);
        //Enemy:画像と名前の設定
        image.sprite = battler.Base.Sprite;
        nameText.text = battler.Base.Name;
        //nameText.ForceMeshUpdate();
    }
}


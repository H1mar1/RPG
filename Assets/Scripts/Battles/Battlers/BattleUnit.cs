using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnit : MonoBehaviour
{
    //UIの管理
    //Batterの管理
   public Battler Battler { get; set; }

    public virtual void Setup(Battler battler)
    {
        Battler = battler;
        //UIの初期化
        //Enemy:画像と名前の設定
        //player:ステータスの設定
    }

    public virtual void UpdateUI()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnit : MonoBehaviour
{
    //UI�̊Ǘ�
    //Batter�̊Ǘ�
   public Battler Battler { get; set; }

    public virtual void Setup(Battler battler)
    {
        Battler = battler;
        //UI�̏�����
        //Enemy:�摜�Ɩ��O�̐ݒ�
        //player:�X�e�[�^�X�̐ݒ�
    }

    public virtual void UpdateUI()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Battler
{
    [SerializeField] BattlerBase _base;
    [SerializeField] int level;

    public int hasExp { get; set; }

    public BattlerBase Base { get => _base;}
    public int Level { get => level;}

    //ステータス
    public int MaxHP { get; set; }
    public int HP {  get; set; }
    public int AT {  get; set; }

    public List<Move> Moves { get; set; }

    //初期化
    public void Init()
    {
        //覚える技から使える技を生成

        Moves = new List<Move>();
        foreach (var learnableMove in Base.LearnableMoves)
        {
            if(learnableMove.Level<=level)
            {
                Moves.Add(new Move(learnableMove.MoveBase));
            }
        }

        Debug.Log(Moves.Count);

        MaxHP = _base.MaxHP;
        HP = MaxHP;
        AT = _base.AT;
    }

    public int TakeDamage(int movePower, Battler attacker)
    {
        int damage = attacker.AT + movePower;// + int.Parse(move.Base.Power);
        //HP -= damage;
        HP = Mathf.Clamp(HP - damage, 0, MaxHP);
        return damage;
    }

    public void Heal(int healPoint)
    {
        HP = Mathf.Clamp(HP + healPoint, 0, MaxHP);
    }

    public Move GetRandomMove()
    {
        int r = Random.Range(0, Moves.Count);
        return Moves[r];
    }

    public bool IsLevelUp()
    {
        if (hasExp >= 100)
        {
            hasExp -= 100;
            level++;
            return true;
        }
        return false;
    }

    //新しく技を覚えるのかどうか
   public Move LearnedMove()
    {
        foreach (var learnableMove in Base.LearnableMoves)
        {
            //まだ覚えていないもので覚える技があれば登録する
            if (learnableMove.Level <= level　&& !Moves.Exists(move=>move.Base==learnableMove.MoveBase))
            {
                Move move = new Move(learnableMove.MoveBase);
                Moves.Add(move);
                return move;
            }
        }
        return null;
    }
}

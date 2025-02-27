using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//‹Z‚ÌŠî‘bƒf[ƒ^
[CreateAssetMenu]
public class MoveBase : ScriptableObject
{
    [SerializeField]  new string name;
    //[SerializeField] string power;

    public string Name { get => name;}
    //public string Power { get => power;}

    public virtual string RunMoveResult(BattleUnit sourceUNit,BattleUnit targetUnit)
    {
        return "";
    }
}

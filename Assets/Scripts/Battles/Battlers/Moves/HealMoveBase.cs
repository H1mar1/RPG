using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HealMoveBase : MoveBase
{
    [SerializeField] int healPoint;
    
    public int HealPoint { get => healPoint; }

    public override string RunMoveResult(BattleUnit sourceUnit, BattleUnit targetUnit)
    {
        sourceUnit.Battler.Heal(healPoint);
        return $"{sourceUnit.Battler.Base.Name}‚Ì{Name}\n{sourceUnit.Battler.Base.Name}‚Í{healPoint}‚©‚¢‚Ó‚­";
    }
}

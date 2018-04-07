using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBonus : BonusBase
{
    [SerializeField] int healthBonus;

    override
    public void OnBonusPick(SpaceShip ship)
    {
        ship.Damage(-healthBonus);
    }
    override
    public GameParams.BonusType GetBonusType()
    {
        return GameParams.BonusType.Health;
    }
}

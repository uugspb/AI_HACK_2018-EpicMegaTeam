using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBonus : BonusBase
{
    public GameParams.ProjectileType projectileType;

    override
    public void OnBonusPick(SpaceShip ship)
    {
        ship.GetComponentInChildren<Weapon>().type = projectileType;
    }
    override
    public GameParams.BonusType GetBonusType()
    {
        return GameParams.BonusType.Weapon;
    }
}

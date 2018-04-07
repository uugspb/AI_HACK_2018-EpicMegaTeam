using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBonus : BonusBase
{
    public GameParams.ProjectileType projectileType;

    private void Awake()
    {
        projectileType = (GameParams.ProjectileType)Random.Range(0, 6);
    }

    override
    public void OnBonusPick(SpaceShip ship)
    {
        ship.SetWeapon(projectileType);
    }

    override
    public GameParams.BonusType GetBonusType()
    {
        return GameParams.BonusType.Weapon;
    }
}

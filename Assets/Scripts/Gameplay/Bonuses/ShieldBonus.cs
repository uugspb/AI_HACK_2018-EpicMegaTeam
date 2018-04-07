using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBonus : BonusBase
{
    override
    public void OnBonusPick(SpaceShip ship)
    {
        Debug.Log("SHIELD BONUS PICKED!");
    }
    override
    public GameParams.BonusType GetBonusType()
    {
        return GameParams.BonusType.Shield;
    }
}

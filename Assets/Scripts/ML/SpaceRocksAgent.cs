using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceRocksAgent : Agent {

    private void Start()
    {
        
    }

    public override void CollectObservations()
    {
        foreach (var ship in GameContext.Instance.ships)
        {
            AddVectorObs(ship.acceleration);
            AddVectorObs(ship.health);
            AddVectorObs(ship.ownerName.GetHashCode());
            AddVectorObs(ship.position);
            AddVectorObs(ship.rotation);
            AddVectorObs(ship.acceleration);
            AddVectorObs((int)ship.weapon);
            AddVectorObs(ship.health);
        }

        foreach (var projectile in GameContext.Instance.projectiles)
        {
            AddVectorObs(projectile.GetVelocity());
            AddVectorObs(projectile.GetOwnerName().GetHashCode());
        }

        foreach(var bonus in GameContext.Instance.bonuses)
        {
            AddVectorObs((int)bonus.GetBonusType());
            AddVectorObs(bonus.transform.position);
        }
    }

    public override void AgentReset()
    {
    }
}

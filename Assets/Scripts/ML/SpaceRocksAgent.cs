using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceRocksAgent : Agent {
    public SpaceShip target;
    public static SpaceRocksAgent Instance;
    private void Start()
    {
        Instance = this;
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

        foreach (var projectile in ObjectsPool.Instance.baseProjectiles)
        {
            AddVectorObs(projectile.GetVelocity());
            AddVectorObs(projectile.GetOwnerName().GetHashCode());
        }

        foreach (var projectile in ObjectsPool.Instance.missileProjectiles)
        {
            AddVectorObs(projectile.GetVelocity());
            AddVectorObs(projectile.GetOwnerName().GetHashCode());
        }

        foreach (var projectile in ObjectsPool.Instance.laserProjectiles)
        {
            AddVectorObs(projectile.GetVelocity());
            AddVectorObs(projectile.GetOwnerName().GetHashCode());
        }
        
        foreach (var bonus in GameContext.Instance.bonuses)
        {
            AddVectorObs((int)bonus.GetBonusType());
            AddVectorObs(bonus.transform.position);
            AddVectorObs(bonus.enable ? 1 : 0);
        }
    }
    int prevScore;
    int prevHP;
    public override void AgentAction(float[] vectorAction, string textAction)
    {
        if (vectorAction[0] > 0)
        {
            target.RotateLeft();
        }
        if (vectorAction[1] > 0)
        {
            target.RotateRight();
        }
        if (vectorAction[2] > 0)
        {
            target.Accelerate();
        }
        if (vectorAction[3] > 0)
        {
            target.Shot();
        }
    }

    public override void AgentReset()
    {
    }
}

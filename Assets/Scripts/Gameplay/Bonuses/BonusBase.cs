using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BonusBase : MonoBehaviour {
    public bool enable = true;

    private void OnTriggerEnter(Collider other)
    {

        var ship = other.GetComponent<SpaceShip>();
        if (ship != null)
        {
            OnBonusPick(ship);
            OnDestroy();
        }
        if (other.name.Contains("Planet"))
        {
            OnDestroy();
        }
    }

    public abstract void OnBonusPick(SpaceShip ship);
    public abstract GameParams.BonusType GetBonusType();

    private void Awake()
    {
        GameContext.Instance.bonuses.Add(this);
    }

    private void OnDestroy()
    {
        ObjectsPool.Instance.ReturnToPool(this);
        //Destroy(this.gameObject);
        //GameContext.Instance.bonuses.Remove(this);
    }
}

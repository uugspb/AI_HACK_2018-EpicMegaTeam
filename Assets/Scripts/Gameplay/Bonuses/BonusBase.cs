using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BonusBase : MonoBehaviour {


    private void OnTriggerEnter(Collider other)
    {

        var ship = other.GetComponent<SpaceShip>();
        if (ship != null)
        {
            OnBonusPick(ship);
            OnDestroy();
            Destroy(this.gameObject);
        }
        if (other.name.Contains("Planet"))
        {
            OnDestroy();
            Destroy(this.gameObject);
        }
    }

    public abstract void OnBonusPick(SpaceShip ship);
    public abstract GameParams.BonusType GetBonusType();

    private void Start()
    {
        GameContext.Instance.bonuses.Add(this);
    }

    private void OnDestroy()
    {
        GameContext.Instance.bonuses.Remove(this);
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{    
    Rigidbody rb;
    bool _initialized = false;
    string ownerName;
    GameParams.Type type;

    public string GetOwnerName()
    {
        return ownerName;
    }

    private void OnTriggerEnter(Collider other)
    {
        var ship = other.GetComponent<SpaceShip>();
        if (ship != null)
        {
            //Debug.Log(ship.GetOwner() + ":::" + ownerName);
            if (ship.GetOwner() != ownerName)
            {
                ship.Damage(GameParams.GetProjectileInfo(type).damage);
                StopAllCoroutines();
                Destruct();
            }
        }
    }
    

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Init(Vector3 velocity, float lifeTime, string ownerName, GameParams.Type type)
    {
        if (!_initialized)
        {
            this.type = type;
            this.ownerName = ownerName;
            this.transform.parent = null;
            rb.velocity = velocity;
            _initialized = true;
            StartCoroutine(WaitAndDie(lifeTime));
            GameContext.Instance.projectiles.Add(this);
        }
    }
    IEnumerator WaitAndDie(float time)
    {
        yield return new WaitForSeconds(time);
        Destruct();
    }

    void Destruct()
    {
        GameContext.Instance.projectiles.Remove(this);
        Destroy(this.gameObject);
    }

    public Vector3 GetVelocity()
    {
        return rb.velocity;
    }
}

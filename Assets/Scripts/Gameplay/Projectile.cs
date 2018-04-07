using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{    
    Rigidbody rb;
    bool _initialized = false;
    string ownerName;

    public string GetOwnerName()
    {
        return ownerName;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.other)
        {

        }
    }

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Init(Vector3 velocity, float lifeTime, string ownerName)
    {
        if (!_initialized)
        {
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
        DestroyImmediate(this.gameObject);
    }

    public Vector3 GetVelocity()
    {
        return rb.velocity;
    }
}

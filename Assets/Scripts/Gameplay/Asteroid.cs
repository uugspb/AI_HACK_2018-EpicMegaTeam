using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = new Vector3(Random.Range(0, 10)/10f, Random.Range(0, 10) / 10f, Random.Range(0, 10) / 10f);
        rb.velocity = new Vector3(Random.Range(0, 10) / 10f, 0, Random.Range(0, 10) / 10f);
        GameContext.Instance.asteroids.Add(this);
    }

    public void Destruct()
    {
        GameContext.Instance.asteroids.Remove(this);
        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {
    Rigidbody rb;
    [SerializeField] GameObject explosionProto;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = new Vector3(Random.Range(0, 10)/10f, Random.Range(0, 10) / 10f, Random.Range(0, 10) / 10f);
        rb.velocity = new Vector3(Random.Range(-10, 10) / 5f, 0, Random.Range(-10, 10) / 5f);
    }

    private void Start()
    {
        GameContext.Instance.asteroids.Add(this);
    }

    public void Destruct()
    {
        GameContext.Instance.asteroids.Remove(this);
        if (explosionProto != null)
        {
            Instantiate(explosionProto, this.transform.position, this.transform.rotation);
        }
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Planet"))
        {
            Destruct();
        }
    }
}

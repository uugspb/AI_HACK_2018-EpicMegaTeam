using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{    
    Rigidbody rb;
    bool _initialized = false;
    string ownerName;
    GameParams.ProjectileType type;
    [SerializeField] GameObject explosionProto;
    [SerializeField] GameObject spawnProto;

    public string GetOwnerName()
    {
        return ownerName;
    }

   

    public static Projectile SpawnProjectile(GameParams.ProjectStruct projectileInfo, Transform gun)
    {
        return Instantiate(projectileInfo.projectileProto, gun);
    }

    private void OnTriggerEnter(Collider other)
    {
        var ship = other.GetComponent<SpaceShip>();
        if (ship != null)
        {
            if (ship.GetOwner() != ownerName)
            {
                var hp = ship.GetHP();
                var result =  ship.Damage(GameParams.GetProjectileInfo(type).damage);
                if (result <= 0)
                {
                    ScoreCounter.AddScore(ownerName, 100);
                }
                else
                {
                    ScoreCounter.AddScore(ownerName, hp - result);
                }
                Destruct();
            }
        }
        else
        {
            if (other.name.Contains("Planet"))
            {
                Destruct();
            }
            if (other.name.Contains("Asteroid"))
            {
                other.GetComponent<Asteroid>().Destruct();
                Destruct();
            }
        }
    }
    

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    string TargetName;

    public void Init(Vector3 velocity, float lifeTime, string ownerName, GameParams.ProjectileType type)
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
            if (type == GameParams.ProjectileType.HomingMissile)
            {
                TargetName = GetBetterTarget();
            }
            if (spawnProto != null)
            {
                Instantiate(spawnProto, this.transform.position, this.transform.rotation);
            }
        }
    }

    string GetBetterTarget()
    {
        float minDist = 999999f;
        int idx = -1;
        var pos = this.transform.position + rb.velocity * 10;
        for (int i = 0; i < GameContext.Instance.ships.Count; i++)
        {
            float dist = Vector3.Distance(pos, GameContext.Instance.ships[i].position);
            if (dist < minDist && GameContext.Instance.ships[i].ownerName != ownerName)
            {
                idx = i;
                minDist = dist;
            }
        }

        return idx >= 0 ? GameContext.Instance.ships[idx].ownerName : "";
    }

    IEnumerator WaitAndDie(float time)
    {
        yield return new WaitForSeconds(time);
        Destruct();
    }

    private void Update()
    {
        if (type == GameParams.ProjectileType.HomingMissile)
        {
            int idx = GameContext.Instance.ships.FindIndex(s => s.ownerName == TargetName);
            if (idx >= 0)
            {
                rb.velocity = (rb.velocity + (GameContext.Instance.ships[idx].position - this.transform.position) * GameParams.HOMING_COEFF).normalized * GameParams.GetProjectileInfo(GameParams.ProjectileType.HomingMissile).velocity;
                //rb.AddForce((GameContext.Instance.ships[idx].position - (this.transform.position + rb.velocity)).normalized * 125);
            }
        }
    }

    void Destruct()
    {
        StopAllCoroutines();
        GameContext.Instance.projectiles.Remove(this);
        if (explosionProto != null)
        {
            Instantiate(explosionProto, this.transform.position, this.transform.rotation);
        }
        Destroy(this.gameObject);
    }

    public Vector3 GetVelocity()
    {
        return rb.velocity;
    }
}

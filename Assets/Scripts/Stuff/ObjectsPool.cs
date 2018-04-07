using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
    public static ObjectsPool Instance
    {
        get
        {
            return _instance;
        }
    }
    static ObjectsPool _instance;

    [SerializeField] int baseProjectilesLimit = 60;
    [SerializeField] int laserProjectilesLimit = 100;
    [SerializeField] int missileProjectilesLimit = 15;
    public List<Projectile> baseProjectiles = new List<Projectile>();
    public List<Projectile> laserProjectiles = new List<Projectile>();
    public List<Projectile> missileProjectiles = new List<Projectile>();

    [SerializeField] int healthBonusesLimit = 3;
    [SerializeField] int weaponBonusesLimit = 5;
    [SerializeField] GameObject healthBonusProto;
    [SerializeField] GameObject weaponBonusProto;
    public List<GameObject> bonuses;

    public void ReturnToPool(Projectile projectile)
    {/*
        switch (projectile.GetProjectileType())
        {
            case GameParams.ProjectileType.Basic:
            case GameParams.ProjectileType.Triple:
            case GameParams.ProjectileType.Twinned:
            baseProjectiles.Add(projectile);
                break;
            case GameParams.ProjectileType.Laser:
                laserProjectiles.Add(projectile);
                break;
            case GameParams.ProjectileType.Missile:
            case GameParams.ProjectileType.HomingMissile:
                missileProjectiles.Add(projectile);
                break;
        }*/
        projectile.transform.parent = this.transform;
        projectile.Reset();
        projectile.transform.localPosition = Vector3.zero;
    }
    int idx1 = 0;
    int idx2 = 0;
    int idx3 = 0;
    public Projectile GetProjectile(GameParams.ProjectileType type)
    {
        Projectile result = null;
        switch (type)
        {
            case GameParams.ProjectileType.Basic:
            case GameParams.ProjectileType.Twinned:
            case GameParams.ProjectileType.Triple:
                result = baseProjectiles[idx1];
                idx1 = (idx1 + 1) % baseProjectiles.Count;
                return result;
            case GameParams.ProjectileType.HomingMissile:
            case GameParams.ProjectileType.Missile:
                result = missileProjectiles[idx2];
                idx2 = (idx2 + 1) % missileProjectiles.Count;
                return result;
            case GameParams.ProjectileType.Laser:
                result = laserProjectiles[idx3];
                idx3 = (idx3 + 1) % laserProjectiles.Count;
                return result;
        }
        return result;
    }
    int idx4 = 0;
    public GameObject GetBonus()
    {
        var result = bonuses[idx4];
        idx4 = (idx4 + 1) % bonuses.Count;
        result.GetComponent<BonusBase>().enable = true;
        return result;
    }

    public void ReturnToPool(BonusBase bonus)
    {
        bonus.transform.parent = this.transform;
        bonus.enable = false;
        bonus.transform.localPosition = Vector3.zero;
    }

    private void Start()
    {
        _instance = this;
        
        this.transform.position = new Vector3(100, 100, 100);
        var proto = GameParams.GetProjectileInfo(GameParams.ProjectileType.Basic).projectileProto;
        for (int i = 0; i < baseProjectilesLimit; i++)
        {
            var pr = Instantiate(proto, this.transform);
            pr.transform.localPosition = Vector3.zero;
            baseProjectiles.Add(pr);
        }
        proto = GameParams.GetProjectileInfo(GameParams.ProjectileType.Laser).projectileProto;

        for (int i = 0; i < laserProjectilesLimit; i++)
        {
            var pr = Instantiate(proto, this.transform);
            pr.transform.localPosition = Vector3.zero;
            laserProjectiles.Add(pr);
        }

        proto = GameParams.GetProjectileInfo(GameParams.ProjectileType.Missile).projectileProto;
        for (int i = 0; i < missileProjectilesLimit; i++)
        {
            var pr = Instantiate(proto, this.transform);
            pr.transform.localPosition = Vector3.zero;
            missileProjectiles.Add(pr);
        }

        for (int i = 0; i < healthBonusesLimit; i++)
        {
            var pr = Instantiate(healthBonusProto, this.transform);
            pr.transform.localPosition = Vector3.zero;
            bonuses.Add(pr);
        }

        for (int i = 0; i < weaponBonusesLimit; i++)
        {
            var pr = Instantiate(healthBonusProto, this.transform);
            pr.transform.localPosition = Vector3.zero;
            bonuses.Add(pr);
        }
    }
}

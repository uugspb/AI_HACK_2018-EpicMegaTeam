using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Transform gun;
    [SerializeField] Transform aim;
    [SerializeField] Transform aim2;
    [SerializeField] Transform aim3;
    [SerializeField] Rigidbody spaceShip;
    [SerializeField] public string ownerName;
    [SerializeField] GameParams.ProjectileType initialType;
    [SerializeField] public GameParams.ProjectileType type
    {
        get
        {
            return _type;
        }

        set
        {
            _type = value;
            projectileInfo = GameParams.GetProjectileInfo(type);
        }
    }

    GameParams.ProjectStruct projectileInfo;

    GameParams.ProjectileType _type;
    bool _mayShot;

    public GameParams.ProjectileType GetWeaponType()
    {
        return type;
    }
    
    private void Start()
    {
        _mayShot = true;
        type = initialType;
        projectileInfo = GameParams.GetProjectileInfo(type);
    }
    
    public void Shot()
    {
        if (_mayShot)
        {
            switch (type)
            {
                case GameParams.ProjectileType.Basic:
                default:
                    SpawnProjectile(aim);
                break;

                case GameParams.ProjectileType.Twinned:
                    SpawnProjectile(aim, new Vector3(0, 0, 1));
                    SpawnProjectile(aim, new Vector3(0, 0, -1));
                break;

                case GameParams.ProjectileType.Triple:
                    SpawnProjectile(aim);
                    SpawnProjectile(aim2);
                    SpawnProjectile(aim3);
                    break;
            }
            StartCoroutine(ReloadShots());
        }
    }
    public void SpawnProjectile(Transform aim)
    {
        SpawnProjectile(aim, Vector3.zero);
    }

    public void SpawnProjectile(Transform aim, Vector3 offset)
    {
        var projectile = Projectile.SpawnProjectile(projectileInfo, gun);
        projectile.transform.localPosition = offset;
        projectile.Init((aim.position - gun.position).normalized * projectileInfo.velocity + spaceShip.velocity, projectileInfo.lifeTime, ownerName, type);
    }    

    public float GetReloadingTime()
    {
        return projectileInfo.reload;
    }

    IEnumerator ReloadShots()
    {
        _mayShot = false;
        yield return new WaitForSeconds(GetReloadingTime());
        _mayShot = true;
    }
}

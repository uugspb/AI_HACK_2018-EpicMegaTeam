using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParams : MonoBehaviour {

    public const int SPACESHIP_MAX_HP = 100;
    public const int SPACESHIP_MAX_SPEED = 6;
    public const int SPACESHIP_ACCELERATION = 3;
    public const int SPACESHIP_ROTATION_SPEED = 3;


    public const int PROJECTILE_BASIC_SPEED = 3;


    public enum Type
    {
        Basic,
        Twinned,
        Triple,
        Missile,
        Mine,
        Laser,
        HomingMissile
    }

    [System.Serializable]
    public struct ProjectStruct
    {
        public Type t;
        public Projectile projectileProto;
        public float velocity;
        public float reload;
        public float lifeTime;
    }

    [SerializeField] List<ProjectStruct> _projectiles = new List<ProjectStruct>();

    public static List<ProjectStruct> projectiles = new List<ProjectStruct>();

    private void Awake()
    {
        projectiles = _projectiles;
    }

    public static ProjectStruct GetProjectileInfo(Type type)
    {
        return projectiles.Find(p => p.t == type);
    }
}

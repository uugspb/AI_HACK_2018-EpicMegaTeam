using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParams : MonoBehaviour {

    public const int SPACESHIP_MAX_HP = 20;
    public const int SPACESHIP_MAX_SPEED = 6;
    public const int SPACESHIP_ACCELERATION = 3;
    public const int SPACESHIP_ROTATION_SPEED = 3;
    public const float HOMING_COEFF = 0.08f;
    public const int PROJECTILE_BASIC_SPEED = 3;
    public const int ASTEROIDS_MAX = 25;
    public const int BONUSES_MAX = 8;


    public enum ProjectileType
    {
        Basic = 0,
        Twinned = 1,
        Triple = 2,
        Missile = 3,
        Laser = 4,
        HomingMissile = 5
    }

    public enum BonusType
    {
        Weapon,
        Health,
        Shield
    }

    [System.Serializable]
    public struct ProjectStruct
    {
        public ProjectileType t;
        public Projectile projectileProto;
        public float velocity;
        public float reload;
        public float lifeTime;
        public int damage;
        public Mesh mesh;
        public Material material;
    }

    [SerializeField] List<ProjectStruct> _projectiles = new List<ProjectStruct>();

    public static List<ProjectStruct> projectiles = new List<ProjectStruct>();

    private void Awake()
    {
        projectiles = _projectiles;
    }

    public static ProjectStruct GetProjectileInfo(ProjectileType type)
    {
        return projectiles.Find(p => p.t == type);
    }
}

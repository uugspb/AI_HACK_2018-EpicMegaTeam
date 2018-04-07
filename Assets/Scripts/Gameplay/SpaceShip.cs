using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour, SpaceShipInterface {

    public void RotateLeft() { _rotateLeftIntention = true; }
    public void RotateRight() { _rotateRightIntention = true; }
    public void Accelerate() { _accelerateIntention = true; }
    public void SlowDown() { _slowDownIntention = true; }
    public void Shot() { _shotIntention = true; }

    Rigidbody rb;
    [SerializeField] Transform gun;
    [SerializeField] Transform aim;
    [SerializeField] Transform center;
    [SerializeField] Weapon weapon;
    [SerializeField] MeshFilter shipMesh;
    [SerializeField] MeshRenderer shipRend;
    [SerializeField] FireEngine engineFire;
    public Projectile pl;

    bool _rotateLeftIntention = false;
    bool _rotateRightIntention = false;
    bool _accelerateIntention = false;
    bool _slowDownIntention = false;
    bool _shotIntention = false;
    int _hp = 100;

    SpaceShipInfo info = new SpaceShipInfo();

    public string Name;
    
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        SetOwnerName(Name);
        OnSpawn();
	}

    public void OnSpawn()
    {
        SetWeapon(GameParams.ProjectileType.Basic);
        //weapon.type = GameParams.ProjectileType.Basic;
        _hp = GameParams.SPACESHIP_MAX_HP;
        GameContext.Instance.ships.Add(info);
    }

    public int Damage(int dmg)
    {
        _hp -= dmg;

        if (_hp > GameParams.SPACESHIP_MAX_HP)
        {
            _hp = GameParams.SPACESHIP_MAX_HP;
        }

        if (_hp <= 0)
        {
            OnDestroy();
            //Destroy(this.gameObject);
        }
        return _hp;
    }

    void OnDestroy()
    {
        var idx = GameContext.Instance.ships.FindIndex(s => s.ownerName == GetOwner());
        if (idx >= 0)
        {
            GameContext.Instance.ships.RemoveAt(idx);
        }
        GameManager.Instance.InitializeRespawn(this);
        StopAllCoroutines();
        this.gameObject.SetActive(false);
    }

    public void SetOwnerName(string name)
    {
        info.ownerName = name;
        weapon.ownerName = name;
    }

    public int GetHP()
    {
        return _hp;
    }
    Vector3 _lastAcceleration;
	void FixedUpdate ()
    {
        bool enableEngine = false;
        _lastAcceleration = Vector3.zero;
        if (_rotateLeftIntention)
        {
            transform.Rotate(new Vector3(0,-1 * GameParams.SPACESHIP_ROTATION_SPEED,0));
        }

        if (_rotateRightIntention)
        {
            transform.Rotate(new Vector3(0, 1 * GameParams.SPACESHIP_ROTATION_SPEED, 0));
        }

        if (_accelerateIntention)
        {
            var vel = (aim.position - center.position).normalized * GameParams.SPACESHIP_ACCELERATION;
            vel = CheckForce(vel);
            rb.AddForce(vel);
            _lastAcceleration = vel;
            enableEngine = true;
            //rb.velocity.Set(vel.x, vel.y, vel.z);
        }

        if (_shotIntention)
        {
            weapon.Shot();
        }
        ResetIntentions();
        engineFire.SetActive(enableEngine);
    }

    void Update()
    {
        UpdateInfo();
    }

    public string GetOwner()
    {
        return Name;
    }

    void UpdateInfo()
    {
        info.position = transform.position;
        info.rotation = transform.rotation;
        info.acceleration = _lastAcceleration;
        info.weapon = weapon.GetWeaponType();
        info.health = _hp;
    }
    

    public Vector3 CheckForce(Vector3 accel)
    {
        var vel = rb.velocity;
        float limit = GameParams.SPACESHIP_MAX_SPEED;
        //accel.x = Mathf.Abs(vel.x)
        accel.x = Mathf.Clamp((vel.x + accel.x), -limit, limit) - vel.x;
        accel.y = Mathf.Clamp((vel.y + accel.y), -limit, limit) - vel.y;
        accel.z = Mathf.Clamp((vel.z + accel.z), -limit, limit) - vel.z;
        rb.velocity = vel;
        return accel;
    }

    void ResetIntentions()
    {
        _rotateLeftIntention = false;
        _rotateRightIntention = false;
        _accelerateIntention = false;
        _slowDownIntention = false;
        _shotIntention = false;
    }
    public enum Color
    {
        RED = 1,
        GREEN = 2,
        BLUE = 3
    }
    [SerializeField] Color color;

    UnityEngine.Color GetColor(Color clr)
    {
        switch (clr)
        {
            case Color.RED:
                return new UnityEngine.Color(232f / 255f, 76f / 255f, 61f / 255f, 1.0f);
                break;
            case Color.GREEN:
                return new UnityEngine.Color(45f / 255f, 204f / 255f, 112f / 255f, 1.0f);
                break;
            case Color.BLUE:
                return new UnityEngine.Color(53f / 255f, 152f / 255f, 219f / 255f, 1.0f);
                break;
            default:
                return UnityEngine.Color.white;
        }
    }


    public void SetWeapon(GameParams.ProjectileType weaponType)
    {
        var info = GameParams.GetProjectileInfo(weaponType);
        weapon.type = weaponType;
        shipMesh.mesh = info.mesh;
        var mat = new Material(info.material);
        mat.color = GetColor(color);
        shipRend.material = mat;
    }
}

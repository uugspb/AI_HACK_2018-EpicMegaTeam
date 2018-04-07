using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipInfo : MonoBehaviour {

    public string ownerName;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 acceleration;
    public GameParams.ProjectileType weapon;
    public int health;
}

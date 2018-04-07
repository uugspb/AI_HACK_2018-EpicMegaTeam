using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour {
    public GameObject spaceShip;
    public SpaceShipInterface target;
	// Use this for initialization
	void Start () {
        target = spaceShip.GetComponent<SpaceShip>();
        if (target == null)
        {
            Debug.LogError(this.gameObject.name + ": Could not found SpaceShipInterface");
            Application.Quit();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        target.Shot();
        target.RotateLeft();
    }
}

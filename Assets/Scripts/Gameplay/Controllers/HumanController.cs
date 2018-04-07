using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : MonoBehaviour {
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
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            target.RotateLeft();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            target.RotateRight();
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            target.Accelerate();
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            target.SlowDown();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            target.Shot();
        }
    }
}

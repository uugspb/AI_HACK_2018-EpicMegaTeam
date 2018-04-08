using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyer : MonoBehaviour {
    public GameObject spaceShip;
    SpaceShip ship;
    GameObject enemy = null;
    public SpaceShipInterface target;
	// Use this for initialization
	void Start ()
    {
        ship = spaceShip.GetComponent<SpaceShip>();
        target = spaceShip.GetComponent<SpaceShip>();
        if (target == null)
        {
            Debug.LogError(this.gameObject.name + ": Could not found SpaceShipInterface");
            Application.Quit();
        }
	}
    bool right = false;
    private void OnEnable()
    {
        right = Random.Range(0, 100) % 2 == 1 ? true : false;
    }

    bool TargetInCone(Vector3 target, int angle)
    {
        Vector3 veca = ship.info.forward - ship.info.position;
        Vector3 vecb = target - ship.info.position;
        return Mathf.Abs(Vector3.Angle(veca, vecb)) < angle;
    }
    int idx = 0;

    int ChooseEnemyIndex()
    {
        for (int i = 0; i < GameContext.Instance.ships.Count; i++)
        {
            if (GameContext.Instance.ships[i].ownerName != ship.GetOwner())
            {
                return i;
            }
        }
        return 0;
    }
    // Update is called once per frame
    void Update ()
    {
        var enemyPos = GameContext.Instance.ships[ChooseEnemyIndex()].position;
        if (!TargetInCone(enemyPos,20))
        {
            if (idx % 5 == 0)
            {
                if (right)
                {
                    target.RotateRight();
                }
                else
                {
                    target.RotateLeft();
                }
            }
        }
        idx++;
        if (idx % 15 == 0)
        {
            target.Shot();
        }
        if (true)
        {
            target.Accelerate();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {
    [SerializeField] Transform target;
	
	// Update is called once per frame
	void Update () {
        if (target != null)
        {
            var pos = target.position;
            pos.y = this.transform.position.y;
            this.transform.position = pos;
        }
	}
}

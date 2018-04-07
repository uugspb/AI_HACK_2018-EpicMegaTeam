using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {
    [SerializeField] Transform target;
	
	// Update is called once per frame
	void Update () {
        if (target != null)
        {
            var result = target.position;
            result.y = this.transform.position.y;
            this.transform.position = this.transform.position + (result - this.transform.position) * 0.02f;
        }
	}
}

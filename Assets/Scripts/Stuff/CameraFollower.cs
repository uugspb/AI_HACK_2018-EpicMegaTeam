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
            iTween.MoveUpdate(this.gameObject, result, 0.1f);
        }
	}
}

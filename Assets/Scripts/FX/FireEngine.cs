using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEngine : MonoBehaviour {

    [SerializeField] List<ParticleSystem> emitters;
	public void SetActive(bool state)
    {
        if (state)
        {
            foreach (var em in emitters)
            {
                em.Play();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour {
    [SerializeField] string x;
    [SerializeField] TMPro.TextMeshProUGUI tex;
	
	// Update is called once per frame
	void Update () {
        tex.text = ScoreCounter.score.Find(d => d.player == x).score.ToString();
	}
}

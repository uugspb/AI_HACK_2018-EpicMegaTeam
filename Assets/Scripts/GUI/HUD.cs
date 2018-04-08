using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public static HUD Instance;// CombineInstance HUD;
    [SerializeField] TMPro.TextMeshProUGUI health;
    [SerializeField] TMPro.TextMeshProUGUI ammo;
    [SerializeField] TMPro.TextMeshProUGUI score;
    public Image weaponIcon;
    // Use this for initialization
    void Start () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        health.text = GameContext.Instance.ships.Find(s => s.ownerName == "Player").health.ToString();
        score.text = ScoreCounter.score.Find(s => s.player == "Player").score.ToString();
        ammo.text = "INF";
	}
}

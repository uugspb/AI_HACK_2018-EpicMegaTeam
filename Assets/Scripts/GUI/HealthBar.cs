using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    [SerializeField] Image healthBarProto;

    List<Image> bars = new List<Image>();
    // Update is called once per frame
    void Update ()
    {
        var cam = Camera.main;
        CheckBarsAmount();

        for (int i = 0; i < GameContext.Instance.ships.Count; i++)
        {
            Vector3 screenPos = cam.WorldToScreenPoint(GameContext.Instance.ships[i].position + new Vector3(0, 0, 2));
            bars[i].rectTransform.position = screenPos;
            bars[i].transform.localScale = new Vector3((float)(GameContext.Instance.ships[i].health) / (float)GameParams.SPACESHIP_MAX_HP, 1, 0);
        }
    }
    

    public void CheckBarsAmount()
    {
        while (GameContext.Instance.ships.Count > bars.Count)
        {
            var bar = Instantiate(healthBarProto, this.transform);
            bar.gameObject.SetActive(true);
            bars.Add(bar);
        }
        while (bars.Count > GameContext.Instance.ships.Count)
        {
            var bar = bars[bars.Count - 1];
            Destroy(bar);
            bars.RemoveAt(bars.Count - 1);
        }
    }
}

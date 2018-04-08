using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusView : MonoBehaviour {

    [SerializeField] Image bonusProto;

    List<Image> bonuses = new List<Image>();

    // Update is called once per frame
    void Update ()
    {
        var cam = Camera.main;
        CheckBonusesAmount();

        for (int i = 0; i < GameContext.Instance.bonuses.Count; i++)
        {
            Vector3 screenPos = cam.WorldToScreenPoint(GameContext.Instance.ships[i].position + new Vector3(0, 0, 2));
            bonuses[i].rectTransform.position = screenPos;
            bonuses[i].transform.localScale = new Vector3((float)(GameContext.Instance.ships[i].health) / (float)GameParams.SPACESHIP_MAX_HP, 1, 0);
        }
    }

    public void CheckBonusesAmount()
    {
        while (GameContext.Instance.ships.Count > bonuses.Count)
        {
            var bar = Instantiate(bonusProto, this.transform);
            bar.gameObject.SetActive(true);
            bonuses.Add(bar);
        }
        while (bonuses.Count > GameContext.Instance.ships.Count)
        {
            var bar = bonuses[bonuses.Count - 1];
            Destroy(bar);
            bonuses.RemoveAt(bonuses.Count - 1);
        }
    }
}

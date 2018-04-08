using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusView : MonoBehaviour {

    [SerializeField] Image bonusProto;
    [SerializeField] SpriteRenderer hpSprite;

    List<Image> bonuses = new List<Image>();

    // Update is called once per frame
    void Update ()
    {
        var cam = Camera.main;
        CheckBonusesAmount();

        for (int i = 0; i < GameContext.Instance.bonuses.Count; i++)
        {
            Vector3 screenPos = cam.WorldToScreenPoint(GameContext.Instance.bonuses[i].transform.position);
            bonuses[i].rectTransform.position = screenPos;
            try
            {
                var wb = GameContext.Instance.bonuses[i] as WeaponBonus;
                Debug.Log(wb.projectileType);

                var info = GameParams.GetProjectileInfo(wb.projectileType);
                bonuses[i].sprite = info.icon.sprite;
            }
            catch(System.Exception e)
            {
                bonuses[i].sprite = hpSprite.sprite;
            }
           // bonuses[i].transform.localScale = new Vector3((float)(GameContext.Instance.ships[i].health) / (float)GameParams.SPACESHIP_MAX_HP, 1, 0);
        }
    }

    public void CheckBonusesAmount()
    {
        while (GameContext.Instance.bonuses.Count > bonuses.Count)
        {
            var bar = Instantiate(bonusProto, this.transform);
            bar.gameObject.SetActive(true);
            bonuses.Add(bar);
        }
        while (bonuses.Count > GameContext.Instance.bonuses.Count)
        {
            var bar = bonuses[bonuses.Count - 1];
            Destroy(bar);
            bonuses.RemoveAt(bonuses.Count - 1);
        }
    }
}

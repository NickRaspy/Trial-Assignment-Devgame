using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusFactory : MonoBehaviour
{
    [SerializeField] private float bonusLifetime = 5f;
    [SerializeField] private List<BonusSettings> bonuses;
    [SerializeField] private BonusStand bonusStand;
    [SerializeField] private Transform effectStack;
    public void GetNewBonus()
    {
        Vector3 position;
        float x = Random.Range(0f, 1f), y = Random.Range(0f, 1f);
        position = Camera.main.ViewportToWorldPoint(new Vector2(x, y));
        position.y = 0.5f;

        BonusStand newStand = Instantiate(bonusStand, transform);
        newStand.transform.position = position;
        newStand.Bonus.Settings = bonuses[Random.Range(0, bonuses.Count)];
        newStand.SetTextWithImage();
        newStand.Duration = bonusLifetime;

        newStand.Action += () => StartCoroutine(EffectOnUI(newStand.Bonus.Settings.image, newStand.Bonus.Settings.duration));
    }
    IEnumerator EffectOnUI(Sprite image, float duration)
    {
        GameObject effect = new();
        effect.transform.transform.parent = effectStack;
        effect.AddComponent<Image>().sprite = image;
        yield return new WaitForSeconds(duration);
        Destroy(effect);
    }
}

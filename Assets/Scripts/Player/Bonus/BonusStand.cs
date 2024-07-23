using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusStand : MonoBehaviour, ILiftetime
{
    [SerializeField] private TextMesh text;
    [SerializeField] private SpriteRenderer displayImage;
    private Bonus bonus = new();
    public Bonus Bonus { get { return bonus; } set { bonus = value; } }
    public float Duration { get; set; }
    public Action Action { get; set; }
    private void Start()
    {
        StartCoroutine(Lifetime());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bonus.StartBonus();
            Action.Invoke();
            Destroy(gameObject);
        }
    }
    public void SetTextWithImage()
    {
        text.text = bonus.Settings.name;
        displayImage.sprite = bonus.Settings.image;
    }
    public IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(Duration);
        Destroy(gameObject);
    }
}

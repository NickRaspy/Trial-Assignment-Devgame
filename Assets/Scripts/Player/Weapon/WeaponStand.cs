using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStand : MonoBehaviour, ILiftetime
{
    public float Duration { get; set; }

    [SerializeField] private TextMesh weaponNameHolder;
    [SerializeField] private Transform weaponHolder;
    private Weapon weapon;
    public Weapon Weapon 
    { 
        get { return weapon; } 
        set 
        { 
            weapon = value; 
            Weapon.transform.parent = weaponHolder;
            Weapon.transform.localPosition = Vector3.zero;
        } 
    }
    void Start()
    {
        StartCoroutine(Lifetime());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerBehavior>().Weapon = Weapon;
            Destroy(gameObject);
        }
    }
    public void SetNameHolderText(string name)
    {
        weaponNameHolder.text = name;
    }
    public IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(Duration);
        Destroy(gameObject);
    }
}

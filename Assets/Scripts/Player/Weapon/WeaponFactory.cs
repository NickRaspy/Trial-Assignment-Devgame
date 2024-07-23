using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory : MonoBehaviour
{
    [SerializeField] private float weaponLifetime = 5f;
    public List<Weapon> weapons;
    public WeaponStand weaponStand;
    public void GetNewWeapon()
    {
        List<Weapon> currentWeapons = new();
        Weapon playersWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>().Weapon;
        weapons.ForEach(a => currentWeapons.Add(a)); if(playersWeapon != null) currentWeapons.Remove(currentWeapons.Find(x => x.name == playersWeapon.name));

        Vector3 position;
        float x = Random.Range(0f, 1f), y = Random.Range(0f, 1f);
        position = Camera.main.ViewportToWorldPoint(new Vector2(x, y));
        position.y = 0.5f;

        WeaponStand newWeaponStand = Instantiate(weaponStand, position, Quaternion.identity);
        newWeaponStand.Duration = weaponLifetime;
        newWeaponStand.transform.parent = transform;
        newWeaponStand.Weapon = Instantiate(currentWeapons[Random.Range(0, currentWeapons.Count)]);
        newWeaponStand.Weapon.name = newWeaponStand.Weapon.name.Replace("(Clone)", "").Trim();
        newWeaponStand.SetNameHolderText(newWeaponStand.Weapon.name);
    }
}

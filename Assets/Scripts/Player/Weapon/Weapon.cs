using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponSettings settings;
    [SerializeField] private Transform bulletOut;

    private IWeapon weapon;
    private bool canShoot = true;
    private void Start()
    {
        weapon = settings.weaponType switch
        {
            WeaponSettings.Type.Shotgun => new Shotgun(),
            WeaponSettings.Type.Launcher => new Launcher(),
            _ => new StandartWeapon()
        };
    }
    public void Shoot()
    {
        if (!canShoot) return;
        weapon.Shoot(bulletOut, settings.bullet, settings.bulletDamage);
        StartCoroutine(ShootDelay());
    }
    IEnumerator ShootDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(2f / settings.speed);
        canShoot = true;
    }
}
#region WEAPONS
public class StandartWeapon : IWeapon
{
    public void Shoot(Transform bulletOut, Bullet bullet, int damage)
    {
        Object.Instantiate(bullet, bulletOut.position, Quaternion.LookRotation(bulletOut.forward)).Damage = damage;
    }
}
public class Shotgun : IWeapon
{
    public void Shoot(Transform bulletOut, Bullet bullet, int damage)
    {
        float y = -5f;
        for(int i = 0; i < 5; i++)
        {
            Bullet newBullet = Object.Instantiate(bullet, bulletOut.position, Quaternion.LookRotation(bulletOut.forward));
            newBullet.Damage = damage;
            newBullet.transform.Rotate(0f, y, 0f);
            y += 2f;
        }
    }
}
public class Launcher : IWeapon
{
    public void Shoot(Transform bulletOut, Bullet bullet, int damage)
    {
        Vector3 destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        destination.y = bulletOut.position.y;
        Explosive newExplosive = Object.Instantiate((Explosive)bullet, bulletOut.position, Quaternion.LookRotation(bulletOut.forward));
        newExplosive.Destination = destination;
        newExplosive.Damage = damage;
    }
}
#endregion 
#region INTERFACE
public interface IWeapon
{
    public void Shoot(Transform bulletOut, Bullet bullet, int damage);
}
#endregion
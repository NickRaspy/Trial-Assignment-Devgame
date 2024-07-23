using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Weapon Settings", menuName = "ScriptableObjects/Weapon Settings", order = 1)]
public class WeaponSettings : ScriptableObject
{
    public Type weaponType;
    public Bullet bullet;
    public int bulletDamage;
    public float speed;

    [Serializable]
    public enum Type
    {
        Standart, Shotgun, Launcher
    }
}

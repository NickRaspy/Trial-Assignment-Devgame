using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Bonus Settings", menuName = "ScriptableObjects/Bonus Settings", order = 1)]
public class BonusSettings : ScriptableObject
{
    public string displayName;
    public BonusType type;
    public Sprite image;
    public float duration;
}
public enum BonusType
{
    Speed, Invincible
}

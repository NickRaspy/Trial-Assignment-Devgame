using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Enemy Characteristics List", menuName = "ScriptableObjects/Enemy Characteristics List", order = 1)]
public class EnemyCharacteristicsList : ScriptableObject
{
    public List<Characteristics> characteristics;
}

[Serializable]
public struct Characteristics
{
    public float speed;
    public int health;
    public int points;
    public float spawnChance;
    public Color color;
}

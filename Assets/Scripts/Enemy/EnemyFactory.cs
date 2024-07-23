using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private EnemyCharacteristicsList enemyCharacteristicsList;
    public void GetNewEnemy()
    {
        Vector3 position = new();
        float x = Random.Range(0f, 1f), y = Random.Range(0f, 1f);
        position = Camera.main.ViewportToWorldPoint( Random.Range(0,2) switch
                {
                    1 => new Vector3(Mathf.Round(x) == 0 ? Mathf.Round(x) - 0.1f : Mathf.Round(x) + 0.1f, y),
                    _ => new Vector3(x, Mathf.Round(y) == 0 ? Mathf.Round(y) - 0.1f : Mathf.Round(y) + 0.1f)
                });
        position.y = 1;
        List<float> chances = new(); enemyCharacteristicsList.characteristics.ForEach(a => chances.Add(a.spawnChance));
        int r = RandomChance.Pick(chances);
        Enemy newEnemy =Instantiate(enemyPrefab, position, Quaternion.identity);
        newEnemy.Characteristics = enemyCharacteristicsList.characteristics[r];
        newEnemy.transform.parent = transform;
    }
}

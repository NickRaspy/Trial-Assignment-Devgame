using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZoneFactory : MonoBehaviour
{
    [SerializeField] private float spawnOffset = 6f;
    [SerializeField] private float borderOffset = 3f; 
    public List<ZoneAmount> zones;
    public MeshRenderer borderReference;
    public void GetNewZones()
    {
        List<Zone> newZones = new();
        zones.ForEach(z =>
        {
            for (int i = 0; i < z.amount; i++)
            {
                newZones.Add(z.zone);
            }
        });
        float rotator = 0f;
        Bounds bounds = borderReference.bounds;
        int zoneAmount = newZones.Count;
        transform.Rotate(0f, Random.Range(0f, 360f), 0f);
        while (newZones.Count > 0)
        {
            int r = Random.Range(0, newZones.Count);
            Zone newZone = Instantiate(newZones[r], transform);
            newZone.transform.Rotate(0f, rotator, 0f);
            newZone.transform.Translate(Vector3.forward * Random.Range(spawnOffset, bounds.max.x > bounds.max.z ? bounds.max.x - borderOffset : bounds.max.z - borderOffset));
            newZone.transform.eulerAngles = Vector3.zero;
            newZones.RemoveAt(r);
            rotator += 360f / zoneAmount;
        }
        foreach (Transform t in transform)
        {
            t.position = new(
                Mathf.Abs(t.position.x) > bounds.max.x - borderOffset ? (t.position.x >= 0f ? t.position.x - borderOffset * 2f : t.position.x + borderOffset * 2) : t.position.x,
                0f,
                Mathf.Abs(t.position.z) > bounds.max.z - borderOffset ? (t.position.z >= 0f ? t.position.z - borderOffset * 2f : t.position.z + borderOffset * 2) : t.position.z
                );
        }
    }
    [Serializable]
    public struct ZoneAmount
    {
        public Zone zone;
        public float amount;
    }
}

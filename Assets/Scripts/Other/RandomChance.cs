using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class RandomChance
{
    public static int Pick(float[] chances)
    {
        float sum = chances.Sum();
        if (sum != 1f) for(int i = 0; i < chances.Length; i++) chances[i] /= sum;
        float comp = 0, r = Random.Range(0f,1f);
        for(int i = 0; i < chances.Length; i++)
        {
            if (r > comp && r <= comp + chances[i]) return i;
            else comp += chances[i];
        }
        return 0;
    }
    public static int Pick(List<float> chances)
    {
        float sum = chances.Sum();
        if (sum != 1f) chances.ForEach(c => c /= sum);
        float comp = 0, r = Random.Range(0f, 1f);
        for (int i = 0; i < chances.Count; i++)
        {
            if (r > comp && r <= comp + chances[i]) return i;
            else comp += chances[i];
        }
        return 0;
    }
}

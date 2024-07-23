using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILiftetime
{
    public float Duration { get; set; }
    public IEnumerator Lifetime();
}

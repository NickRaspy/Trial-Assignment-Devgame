using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public int Damage { get; set; }
    private Action action;
    void Start()
    {
        LifeAction();
    }
    void Update()
    {
        action?.Invoke();
    }
    public virtual void LifeAction()
    {
        action += () => Move();
        StartCoroutine(LiveUntil());
    } 
    void Move()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.forward, Space.Self);
    }
    IEnumerator LiveUntil()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}

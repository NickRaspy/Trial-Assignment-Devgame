using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : Bullet
{
    public Vector3 Destination { get; set; }
    public override void LifeAction()
    {
        StartCoroutine(ReachPoint());
    }
    IEnumerator ReachPoint()
    {
        Debug.Log(Destination);
        while(Vector3.Distance(transform.position, Destination) > 1f) 
        {
            Debug.Log(Vector3.Distance(transform.position, Destination));
            transform.position = Vector3.Lerp(transform.position, Destination, Time.deltaTime * speed);
            yield return new WaitForEndOfFrame();
        }
        CreateExplosion();
        Collider[] victims = Physics.OverlapSphere(transform.position, 2f);
        foreach (Collider c in victims)
            if (c.CompareTag("Enemy")) c.GetComponent<Enemy>().CurrentHealth -= Damage;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
    void CreateExplosion()
    {
        GameObject explosion = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        explosion.GetComponent<Collider>().isTrigger = true;
        explosion.GetComponent<MeshRenderer>().material.color = Color.red + Color.yellow;
        explosion.transform.parent = transform;
        explosion.transform.localPosition = Vector3.zero;
        explosion.transform.localScale = Vector3.one * 15f;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 2f);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private GameObject canvas;

    public Characteristics Characteristics { get; set; }

    private int currentHealth;
    public int CurrentHealth
    {
        get { return currentHealth; }
        set 
        { 
            currentHealth = value; 
            healthBar.value = currentHealth;
            if(currentHealth <= 0) Death(); 
        }
    }

    private Vector3 target;
    private Quaternion canvasOriginRotation;
    private void Start()
    {
        canvasOriginRotation = canvas.transform.localRotation;
        healthBar.maxValue = Characteristics.health;
        CurrentHealth = Characteristics.health;
        GetComponent<MeshRenderer>().material.color = Characteristics.color;
    }
    private void Update()
    {
        Move();
        canvas.transform.rotation = canvasOriginRotation;
    }
    void Death()
    {
        GameManager.instance.Score += Characteristics.points;
        Destroy(gameObject);
    }

    public void Move()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null) return;
        target = GameObject.FindGameObjectWithTag("Player").transform.position;
        target.y = transform.position.y;
        transform.Translate(Characteristics.speed * Time.deltaTime * (target - transform.position).normalized, Space.World);
        transform.LookAt(target);
    }
    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "Bullet":
                CurrentHealth -= other.GetComponent<Bullet>().Damage;
                Destroy(other.gameObject);
                break;
            case "Player":
                other.GetComponent<PlayerBehavior>().MustDie = true;
                if (!other.GetComponent<PlayerBehavior>().IsInvincible) other.GetComponent<PlayerBehavior>().Death();
                break;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) other.GetComponent<PlayerBehavior>().MustDie = false;
    }
}


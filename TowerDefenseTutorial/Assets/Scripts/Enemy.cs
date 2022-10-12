using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;
    
    public float startHealth = 100;
    private float health;
    public int worth = 50;

    [Header("Unity Staff")] 
    public Image healthBar;

    private void Start()
    {
        health = startHealth;
        speed = startSpeed;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health/startHealth;
        if (health<=0)
        {
            Die();
        }
    }

    public void Slow(float pct)
    {
        speed = startSpeed*(1f - pct);
    }
    void Die()
    {
        PlayerStats.Money += worth;
        WaweSpanner.EnemiesAlive--;
        Destroy(gameObject);
    }
    
}

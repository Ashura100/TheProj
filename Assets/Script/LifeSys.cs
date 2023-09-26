using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSys : MonoBehaviour, IAlife
{
    public delegate void OnDie();
    public OnDie onDieDel;
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthbar;

    int IAlife.maxHealth
    {
        get
        {
            return this.maxHealth;
        }
    }

    public int health
    {
        get
        {
            return this.currentHealth;
        }
    }

    public bool isAlife
    {
        get
        {
            return health > 0;
        }
    }

    void Start()
    {
        healthbar.Init(this);
        currentHealth = maxHealth;
        healthbar.UpdateHealth();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthbar.UpdateHealth();
        Die();
    }

    void Die()
    {
        if (currentHealth <= 0)
        {
            if (onDieDel != null) onDieDel();
        }
    }
}

public interface IAlife
{
    bool isAlife
    {
        get;
    }
    int maxHealth
    {
        get;
    }

    int health
    {
        get;
    }
}

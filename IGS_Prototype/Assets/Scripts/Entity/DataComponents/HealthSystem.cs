using System;
using UnityEngine;

public class HealthSystem
{
    private float currentHealth;
    public Action onHealthDrained;
    public HealthData configData;

    public HealthSystem(HealthData configData)
    {
        this.configData = configData;
        currentHealth = configData.maxHealth;
    }
    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            onHealthDrained?.Invoke();
        }
        else if (currentHealth >= configData.maxHealth)
            currentHealth = configData.maxHealth;

        Debug.Log(currentHealth);
    }
    
    public void Heal(float heal) => TakeDamage(-heal);
    public void HealMax() => Heal(configData.maxHealth);
}
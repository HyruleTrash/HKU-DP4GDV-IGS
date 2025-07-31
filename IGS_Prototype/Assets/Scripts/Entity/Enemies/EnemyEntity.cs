using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : IEntity
{
    public GameObject Body { get; set; }
    public bool Active { get; set; }
    private HealthSystem healthSystem;
    public HurtTriggerEntity[] hurtTriggers;
    private EntityManager entityManagerReference;

    public EnemyEntity(HealthData healthData)
    {
        healthSystem = new HealthSystem(healthData);
        healthSystem.onHealthDrained = DoDie;
        entityManagerReference = Game.instance.GetEntityManager();
    }
    
    public void OnEnableObject()
    {
        healthSystem.HealMax();
        Body.SetActive(true);
    }

    public void OnDisableObject()
    {
        Body.SetActive(false);
        foreach (HurtTriggerEntity hurtTrigger in hurtTriggers)
        {
            entityManagerReference.entityPool.DeactivateObject(hurtTrigger);
        }
    }

    public void DoDie()
    {
        entityManagerReference.entityPool.DeactivateObject(this);
    }

    public void CustomUpdate() { }
    public void CustomUpdateAtFixedRate() { }

    public void TakeDamage(float damage)
    {
        healthSystem.TakeDamage(damage);
    }
}
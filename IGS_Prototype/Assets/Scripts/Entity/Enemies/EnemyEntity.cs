using System;
using System.Collections.Generic;
using LucasCustomClasses;
using UnityEngine;

public class EnemyEntity : IEntity
{
    public GameObject Body { get; set; }
    public bool Active { get; set; }
    private HealthSystem healthSystem;
    public HurtTriggerEntity[] hurtTriggers;
    private EntityManager entityManagerReference;
    private Timer invisibilityTimer;
    private bool invisibility;

    public EnemyEntity(HealthData healthData, float hitInvinsibilityTime)
    {
        healthSystem = new HealthSystem(healthData);
        healthSystem.onHealthDrained = DoDie;
        entityManagerReference = Game.instance.GetEntityManager();
        invisibilityTimer = new Timer(hitInvinsibilityTime, () => invisibility = false);
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

    public void CustomUpdate()
    {
        invisibilityTimer.Update(Time.deltaTime);
    }
    
    public void CustomUpdateAtFixedRate() { }

    public void TakeDamage(float damage)
    {
        if (invisibility)
            return;
        PlayerInterfaceConsole.Instance.AddToConsole($"{this.GetType()}{Body.GetInstanceID()}: Took <color=red>{damage}</color> damage");
        healthSystem.TakeDamage(damage);
        invisibility = true;
        invisibilityTimer.Reset();
    }
}
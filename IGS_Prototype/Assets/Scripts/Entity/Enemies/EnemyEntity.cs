using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : IEntity, IDamagable
{
    public GameObject Body { get; set; }
    public List<DamageType> Weaknesses { get; set; }
    public List<DamageType> Affinities { get; set; }
    public Action OnDamaged { get; set; }
    public bool Active { get; set; }
    private HealthSystem healthSystem;

    public EnemyEntity(HealthData healthData)
    {
        healthSystem = new HealthSystem(healthData);
        healthSystem.onHealthDrained = DoDie;
    }
    
    public void OnEnableObject()
    {
        healthSystem.HealMax();
        Body.SetActive(true);
    }

    public void OnDisableObject()
    {
        Body.SetActive(false);
    }

    public void DoDie()
    {
        Game.instance.GetEntityManager().entityPool.DeactivateObject(this);
    }

    public void CustomUpdate()
    {
        // throw new System.NotImplementedException();
    }

    public void CustomUpdateAtFixedRate()
    {
        // throw new System.NotImplementedException();
    }

    public void TakeDamage(IDamager other)
    {
        OnDamaged?.Invoke();
        healthSystem.TakeDamage(other.RetrieveDamage(this));
    }
}
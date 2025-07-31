using System;
using System.Collections.Generic;
using UnityEngine;

public class HurtTriggerEntity : TriggerEntity, IDamagable
{
    public List<DamageType> Weaknesses { get; set; }
    public List<DamageType> Affinities { get; set; }
    public Action<IDamager> OnDamaged { get; set; }

    public void Initialize(Action<float> onDamaged, List<DamageType> weaknesses, List<DamageType> affinities, float radius)
    {
        this.OnDamaged = other =>
        {
            onDamaged.Invoke(other.RetrieveDamage(this));
        };
        this.Weaknesses = weaknesses;
        this.Affinities = affinities;
        this.triggerRadius = radius;
    }
    
    public void TakeDamage(IDamager other)
    {
        OnDamaged?.Invoke(other);
    }
}
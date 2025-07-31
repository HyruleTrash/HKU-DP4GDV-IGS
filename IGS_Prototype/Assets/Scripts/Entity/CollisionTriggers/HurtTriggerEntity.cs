using System;
using System.Collections.Generic;
using UnityEngine;

public class HurtTriggerEntity : TriggerEntity, IDamagable
{
    public List<DamageType> Weaknesses { get; set; }
    public List<DamageType> Affinities { get; set; }
    public Action<IDamager> OnDamaged { get; set; }

    public override void OnEnableObject()
    {
        base.OnEnableObject();
        Body.SetActive(true);
    }

    public override void OnDisableObject()
    {
        Body.SetActive(false);
    }

    public void Initialize(Action<float> onDamaged, List<DamageType> weaknesses, List<DamageType> affinities, float radius)
    {
        this.OnDamaged = other =>
        {
            onDamaged.Invoke(other.RetrieveDamage(this));
        };
        this.Weaknesses = weaknesses;
        this.Affinities = affinities;
        this.triggerRadius = radius;
        if (Body)
            this.collider.radius = this.triggerRadius;
    }
    
    public void TakeDamage(IDamager other)
    {
        OnDamaged?.Invoke(other);
    }
}
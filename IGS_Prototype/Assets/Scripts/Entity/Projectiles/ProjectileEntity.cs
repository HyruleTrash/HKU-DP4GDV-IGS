using System;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEntity : TriggerEntity, IDamager
{
    private List<DamageTypeDecorator> damageTypes = new ();
    public Rigidbody rb;
    public float baseDamage;
    
    public override void OnEnableObject()
    {
        Body.SetActive(true);
        layerMasks = new[] { typeof(IDamagable) };
        onTrigger = OnTrigger;
    }

    private void OnTrigger(IEntity other)
    {
        if (other is IDamagable damagable)
        {
            damagable.TakeDamage(this);
        }
    }

    public override void OnDisableObject()
    {
        Body.SetActive(false);
        rb.linearVelocity = Vector3.zero;
    }
    
    public bool HasAffinity(DamageType damageType)
    {
        foreach (DamageTypeDecorator damageTypeDecorator in damageTypes)
        {
            if (damageTypeDecorator.affinity == damageType)
                return true;
        }
        return false;
    }

    public void AddDamageTypeDecorator(DamageType type)
    {
        damageTypes.Add(new DamageTypeDecorator(type));
    }

    public void ClearDamageTypes()
    {
        damageTypes.Clear();
    }

    public float RetrieveDamage(IDamagable other)
    {
        float damage = baseDamage;
        foreach (DamageTypeDecorator decorator in damageTypes)
        {
            damage = decorator.CalculateDamage(other, damage);
            if (damage <= 0)
                break;
        }
        DoDie();
        return damage;
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using LucasCustomClasses;
using UnityEngine;

public class ProjectileEntity : TriggerEntity, IDamager
{
    private List<DamageTypeDecorator> damageTypes = new ();
    public Rigidbody rb;
    public float baseDamage;
    private StringBuilder decoratorAffinitiesAsStrings;
    private bool canDamage = true;
    public Timer despawnTimer;
    
    public override void OnEnableObject()
    {
        base.OnEnableObject();
        Body.SetActive(true);
        layerMasks = new[] { typeof(IDamagable) };
        onTrigger = OnTrigger;

        if (despawnTimer != null)
        {
            despawnTimer.running = false;
            despawnTimer.onEnd = DoDie;
        }
        
        decoratorAffinitiesAsStrings = new();
        foreach (DamageTypeDecorator decorator in damageTypes)
            decoratorAffinitiesAsStrings.Append($"<color={DamageTypeLookup.colorTable[decorator.affinity].ToHex()}>{decorator.affinity.ToString()}</color> ");
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

    public override void CustomUpdate()
    {
        base.CustomUpdate();
        despawnTimer?.Update(Time.deltaTime);
    }

    public DamageData RetrieveDamage(IDamagable other)
    {
        if (!canDamage)
            return new DamageData(0, "");
        StringBuilder damageText = new();
        float damage = baseDamage;
        
        foreach (DamageTypeDecorator decorator in damageTypes)
        {
            damage = decorator.CalculateDamage(other, damage);
            if (damage <= 0)
                break;
        }
        damageText.Append($"Projectile hit with: {decoratorAffinitiesAsStrings} type damage. ");
        damageText.Append($"Against entity with: {other.GetWeaknessesAndAffinities()}\n");

        despawnTimer.running = true;
        canDamage = false;
        return new DamageData(damage, damageText.ToString());
    }
}
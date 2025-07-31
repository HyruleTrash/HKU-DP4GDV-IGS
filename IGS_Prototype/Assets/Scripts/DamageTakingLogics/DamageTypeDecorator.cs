using System.Collections.Generic;
using UnityEngine;

public class DamageTypeDecorator
{
    public DamageType affinity;

    public DamageTypeDecorator(DamageType type)
    {
        affinity = type;
    }
    
    public float CalculateDamage(IDamagable other, float damage)
    {
        damage = ApplyMultiplier(damage, other.Affinities, DamageTypeLookup.affinityTable);
        damage = ApplyMultiplier(damage, other.Weaknesses, DamageTypeLookup.weaknessTable);
        return damage;
    }

    private float ApplyMultiplier(float damage, List<DamageType> otherEntityTypesList, Dictionary<DamageType,DamageTypeLookup.DamageTypeRecord[]> LookupReference)
    {
        foreach (var type in otherEntityTypesList)
        {
            if (!LookupReference.TryGetValue(type, out var value))
                continue;
            foreach (var record in value)
            {
                if (record.damageType != affinity)
                    continue;
                damage *= record.multiplier;
                break;
            }
        }
        return damage;
    }
}
using System;
using System.Collections.Generic;
using System.Text;
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

    public void Initialize(Action<DamageData> onDamaged, List<DamageType> weaknesses, List<DamageType> affinities, float radius)
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

    public string GetWeaknessesAndAffinities()
    {
        StringBuilder text = new();
        void ListToString(List<DamageType> list, StringBuilder sb)
        {
            for (var index = 0; index < list.Count; index++)
            {
                var damageType = list[index];
                sb.Append($"<color={DamageTypeLookup.colorTable[damageType].ToHex()}>{damageType.ToString()}</color>");
                if (index < list.Count - 1)
                {
                    sb.Append(", ");
                }
            }
        }
        
        text.Append("Weaknesses: ");
        ListToString(Weaknesses, text);

        text.Append(" and affinities: ");
        ListToString(Affinities, text);

        return text.ToString();
    }
}
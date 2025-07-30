
using System;
using System.Collections.Generic;

public interface IDamagable
{
    public List<DamageType> Weaknesses { get; set; }
    public List<DamageType> Affinities { get; set; }
    public Action OnDamaged { get; set; }
    public void TakeDamage(IDamager other);
}
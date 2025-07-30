
using System.Collections.Generic;

public interface IDamagable
{
    List<DamageType> Weaknesses { get; set; }
    List<DamageType> Affinities { get; set; }
    
    public void TakeDamage(IDamager other);
}
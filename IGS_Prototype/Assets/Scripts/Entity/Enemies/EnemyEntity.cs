using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : IEntity, IDamagable
{
    public GameObject Body { get; set; }
    public List<DamageType> Weaknesses { get; set; }
    public List<DamageType> Affinities { get; set; }
    public bool Active { get; set; }
    
    public void OnEnableObject()
    {
        Body.SetActive(true);
    }

    public void OnDisableObject()
    {
        Body.SetActive(false);
    }

    public void DoDie()
    {
        throw new System.NotImplementedException();
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
        throw new System.NotImplementedException();
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEntity : TriggerEntity, IDamager
{
    private List<DamageTypeDecorator> damageTypes;
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
        if (other is IDamagable)
        {
            Debug.Log($"Hit! {other}"); // TODO: currently not hitting anything
        }
    }

    public override void OnDisableObject()
    {
        Body.SetActive(false);
        rb.linearVelocity = Vector3.zero;
    }

    public float RetrieveDamage()
    {
        throw new NotImplementedException();
    }
}
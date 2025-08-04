
using LucasCustomClasses;
using UnityEngine;

public class EnemyEntity : IEntity
{
    public GameObject Body { get; set; }
    public bool Active { get; set; }
    private HealthSystem healthSystem;
    public HurtTriggerEntity[] hurtTriggers;
    private EntityManager entityManagerReference;
    private Timer invincibilityTimer;
    private bool invincibility;

    public EnemyEntity(HealthData healthData, float hitInvincibilityTime)
    {
        healthSystem = new HealthSystem(healthData);
        healthSystem.onHealthDrained = DoDie;
        entityManagerReference = Game.instance.GetEntityManager();
        invincibilityTimer = new Timer(hitInvincibilityTime, () => invincibility = false);
    }
    
    public void OnEnableObject()
    {
        healthSystem.HealMax();
        Body.SetActive(true);
    }

    public void OnDisableObject()
    {
        Body.SetActive(false);
        foreach (HurtTriggerEntity hurtTrigger in hurtTriggers)
        {
            entityManagerReference.entityPool.DeactivateObject(hurtTrigger);
        }
    }

    public void DoDie()
    {
        entityManagerReference.entityPool.DeactivateObject(this);
    }

    public void CustomUpdate()
    {
        invincibilityTimer.Update(Time.deltaTime);
    }
    
    public void CustomUpdateAtFixedRate() { }

    public void TakeDamage(DamageData damageData)
    {
        if (invincibility)
            return;
        if (damageData.damage != 0)
            PlayerInterfaceConsole.Instance.AddToConsole($"{damageData.damageText}{this.GetType()}{Body.GetInstanceID()}: Took <color=red>{damageData.damage}</color> damage");
        healthSystem.TakeDamage(damageData.damage);
        invincibility = true;
        invincibilityTimer.Reset();
    }
}
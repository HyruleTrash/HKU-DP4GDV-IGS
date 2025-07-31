using UnityEngine;

public class LevelEndEntity : TriggerEntity
{
    public LevelEndData data;
    private EntityManager entityManagerReference;

    public LevelEndEntity(LevelEndData data)
    {
        this.data = data;
        entityManagerReference = Game.instance.GetEntityManager();
        onTrigger = OnTrigger;
        layerMasks = new[] { typeof(PlayerEntity) };
        triggerRadius = data.triggerRadius;
    }
    
    public override void OnEnableObject()
    {
        base.OnEnableObject();
        Body.SetActive(true);
    }

    public override void OnDisableObject()
    {
        Body.SetActive(false);
    }

    private void OnTrigger(IEntity other)
    {
        if (other is PlayerEntity player)
            Game.instance.NextLevel();
    }
}
using UnityEngine;

public class LevelEndEntity : IEntity
{
    public GameObject body;
    public LevelEndData data;
    private EntityManager entityManagerReference;
    public bool active { get; set; }
    private bool shouldTrigger;

    public LevelEndEntity(LevelEndData data)
    {
        this.data = data;
        entityManagerReference = Game.instance.GetEntityManager();
    }
    
    public void OnEnableObject()
    {
        body.SetActive(true);
        shouldTrigger = false;
    }

    public void OnDisableObject()
    {
        body.SetActive(false);
    }

    public void DoDie()
    {
        entityManagerReference.entityPool.DeactivateObject(this);
    }

    public void CustomUpdate()
    {
        if (shouldTrigger)
        {
            Game.instance.NextLevel();
        }
    }

    public void CustomUpdateAtFixedRate()
    {
        if (!active)
            return;
        if (!entityManagerReference.entityPool.GetActiveObject(typeof(PlayerEntity), out var result))
            return;
        PlayerEntity player = (PlayerEntity)result;

        float distance = Vector3.Distance(player.body.transform.position, body.transform.position);
        if (player.active && distance < data.triggerRadius)
        {
            shouldTrigger = true;
        }
    }
}
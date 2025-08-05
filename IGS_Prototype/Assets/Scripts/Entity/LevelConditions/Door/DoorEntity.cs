using UnityEngine;

public class DoorEntity : IEntity
{
    public GameObject Body { get; set; }
    public bool Active { get; set; }

    public void Open()
    {
        DoDie();
    }
    
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
        Game.instance.GetEntityManager().entityPool.DeactivateObject(this);
    }

    public void CustomUpdate() { }
    public void CustomUpdateAtFixedRate() { }
}
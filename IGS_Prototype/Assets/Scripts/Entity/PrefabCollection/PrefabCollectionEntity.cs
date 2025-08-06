
using UnityEngine;

public class PrefabCollectionEntity : IEntity
{
    public bool Active { get; set; }
    public GameObject Body { get; set; }
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
        var gameReference = Game.instance;
        gameReference.GetEntityManager().entityPool.DestroyObject(this);
    }

    public void CustomUpdate() { }
    public void CustomUpdateAtFixedRate() { }
}
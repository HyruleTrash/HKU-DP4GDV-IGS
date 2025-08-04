using UnityEngine;

public class PlayerEntity : IEntity
{
    public GameObject Body {get; set;}
    private PlayerData playerData;
    public GunHandler gunHandler;
    public bool Active { get; set; }

    public PlayerEntity(PlayerData playerData)
    {
        this.playerData = playerData;
    }
    
    public void OnEnableObject()
    {
        Body.SetActive(true);
        playerData.camera.Active = true;
        playerData.movementController.Active = true;
    }

    public void OnDisableObject()
    {
        Body.SetActive(false);
        playerData.camera.Active = false;
        playerData.movementController.Active = false;
    }

    public void DoDie()
    {
        Game gameReference = Game.instance;
        gameReference.GetEntityManager().entityPool.DeactivateObject(this);
        gameReference.TriggerDeathScreen();
    }

    public void CustomUpdate()
    {
        playerData.camera.CustomUpdate(Body);
        gunHandler.CustomUpdate();
    }

    public void CustomUpdateAtFixedRate()
    {
        playerData.movementController.CustomUpdateAtFixedRate(Body);
    }

    public Transform GetCameraTransform()
    {
        Transform transform = playerData.camera.GetTransform();
        if (!transform)
            return null;
        return transform;
    }
    
    public Vector3 GetBulletDirection()
    {
        Transform transform = playerData.camera.GetTransform();
        if (!transform)
            return Vector3.zero;
        return transform.forward;
    }

    public void Destroy()
    {
        GameObject.Destroy(Body);
        gunHandler.Destroy();
    }
}
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
        // TODO: show death screen logic
        Game.instance.GetEntityManager().entityPool.DeactivateObject(this);
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

    public Vector3 GetBulletOrigin()
    {
        Transform transform = playerData.camera.GetTransform();
        if (transform == null)
            return Vector3.zero;
        return transform.position + transform.forward + (-transform.up * 0.5f); // Used a magic number here, since this is supposedly temporary TODO: make this more elegant
    }
    
    public Vector3 GetBulletDirection()
    {
        Transform transform = playerData.camera.GetTransform();
        if (transform == null)
            return Vector3.zero;
        return transform.forward;
    }
}
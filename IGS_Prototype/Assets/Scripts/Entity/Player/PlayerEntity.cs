using UnityEngine;

public class PlayerEntity : IEntity
{
    private PlayerData playerData;
    public GameObject body;
    public bool active { get; set; }

    public PlayerEntity(PlayerData playerData)
    {
        this.playerData = playerData;
    }
    
    public void OnEnableObject()
    {
        body.SetActive(true);
        playerData.camera.Active = true;
        playerData.movementController.Active = true;
    }

    public void OnDisableObject()
    {
        body.SetActive(false);
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
        playerData.camera.CustomUpdate(body);
    }

    public void CustomUpdateAtFixedRate()
    {
        playerData.movementController.CustomUpdateAtFixedRate(body);
    }
}
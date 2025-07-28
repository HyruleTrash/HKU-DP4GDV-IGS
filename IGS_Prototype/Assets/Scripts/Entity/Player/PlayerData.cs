using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "EntityData/PlayerData")]
public class PlayerData : LevelDataEntity
{
    public FirstPersonCamera camera;
    public FirstPersonMovement movementController;
    public GunInventory gunInventory;
    
    public override void Load()
    {
        PlayerEntity playerEntity = new PlayerEntity(this);
        playerEntity.active = true;
        playerEntity.body = new GameObject("Player");

        camera.Load(playerEntity.body);
        camera.Active = true;
        
        movementController.Load(playerEntity.body);
        
        Game.instance.GetEntityManager().entityPool.AddToPool(playerEntity);
    }
}

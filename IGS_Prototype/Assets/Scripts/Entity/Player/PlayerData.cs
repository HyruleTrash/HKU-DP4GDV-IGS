using System.ComponentModel;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "PlayerData", menuName = "EntityData/PlayerData")]
public class PlayerData : LevelDataEntity
{
    [Header("Player Data")]
    [SerializeField] private GameObject bodyPrefab;
    public FirstPersonCamera camera;
    public FirstPersonMovement movementController;
    [FormerlySerializedAs("gunInventory")] public GunLevelData gunLevelData;
    
    public override IEntity Load()
    {
        EntityManager entityManagerReference = Game.instance.GetEntityManager();
        PlayerEntity playerEntity;

        if (entityManagerReference.entityPool.GetInActiveObject(typeof(PlayerEntity), out var result))
        {
            playerEntity = (PlayerEntity)result;
            playerEntity.Body.transform.position = position;
            entityManagerReference.entityPool.ActivateObject(playerEntity);
        }
        else
        {
            if (!camera || !movementController || !gunLevelData || !bodyPrefab)
                throw new WarningException("The player in this level is missing data and cannot be loaded");
            
            playerEntity = new PlayerEntity(this);
            playerEntity.Active = true;
            playerEntity.Body = Instantiate(bodyPrefab, position, Quaternion.identity);
            
            camera.Load(playerEntity.Body);
            camera.Active = true;
            
            movementController.Load(playerEntity.Body);
            movementController.Active = true;

            playerEntity.gunHandler = new GunHandler(playerEntity);
            gunLevelData.Load(playerEntity.gunHandler);
            
            entityManagerReference.entityPool.AddToPool(playerEntity);
        }
        
        return playerEntity;
    }
}

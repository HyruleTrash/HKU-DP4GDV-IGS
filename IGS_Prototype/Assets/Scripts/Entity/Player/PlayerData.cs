using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "EntityData/PlayerData")]
public class PlayerData : LevelDataEntity
{
    [Header("Player Data")]
    [SerializeField] private GameObject bodyPrefab;
    public FirstPersonCamera camera;
    public FirstPersonMovement movementController;
    public GunInventory gunInventory;
    
    public override void Load()
    {
        EntityManager entityManagerReference = Game.instance.GetEntityManager();
        PlayerEntity playerEntity;

        if (entityManagerReference.entityPool.GetInActiveObject(typeof(PlayerEntity), out var result))
        {
            playerEntity = (PlayerEntity)result;
            playerEntity.body.transform.position = position;
            entityManagerReference.entityPool.ActivateObject(playerEntity);
        }
        else
        {
            playerEntity = new PlayerEntity(this);
            playerEntity.active = true;
            playerEntity.body = Instantiate(bodyPrefab, position, Quaternion.identity);

            camera.Load(playerEntity.body);
            camera.Active = true;
            
            movementController.Load(playerEntity.body);
            movementController.Active = true;
            
            entityManagerReference.entityPool.AddToPool(playerEntity);
        }
    }
}

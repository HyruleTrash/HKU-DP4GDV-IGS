
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "DoorData", menuName = "EntityData/DoorData")]
public class DoorData : LevelDataEntity
{
    [Header("Door data")]
    [SerializeField] private GameObject bodyPrefab;
    [SerializeField] private Quaternion rotation;
    [Header("Key data")]
    [SerializeField] private GameObject keyBodyPrefab;
    [SerializeField] private Vector3 keyPosition;
    [SerializeField] private float keyPickupRadius;
    
    public override IEntity Load()
    {
        EntityManager entityManagerReference = Game.instance.GetEntityManager();
        DoorEntity doorEntity;

        if (entityManagerReference.entityPool.GetInActiveObject(typeof(DoorEntity), out var result))
        {
            doorEntity = (DoorEntity)result;
            doorEntity.Body.transform.position = position;
            doorEntity.Body.transform.rotation = rotation;
            
            entityManagerReference.entityPool.ActivateObject(doorEntity);
        }
        else
        {
            doorEntity = new DoorEntity();
            doorEntity.Active = true;
            doorEntity.Body = Instantiate(bodyPrefab, position, rotation);
            
            entityManagerReference.entityPool.AddToPool(doorEntity);
        }

        LoadKey(doorEntity);
        return doorEntity;
    }

    private void LoadKey(DoorEntity door)
    {
        EntityManager entityManagerReference = Game.instance.GetEntityManager();
        KeyPickupEntity keyPickupEntity;

        if (entityManagerReference.entityPool.GetInActiveObject(typeof(KeyPickupEntity), out var result))
        {
            keyPickupEntity = (KeyPickupEntity)result;
            keyPickupEntity.Body.transform.position = keyPosition;
            
            entityManagerReference.entityPool.ActivateObject(keyPickupEntity);
        }
        else
        {
            keyPickupEntity = new KeyPickupEntity();
            keyPickupEntity.Active = true;
            keyPickupEntity.Body = Instantiate(keyBodyPrefab, keyPosition, Quaternion.identity);
            
            entityManagerReference.entityPool.AddToPool(keyPickupEntity);
        }
        
        keyPickupEntity.doorEntity = door;
        keyPickupEntity.triggerRadius = keyPickupRadius;
        keyPickupEntity.Setup();
    }
}
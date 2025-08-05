public class KeyPickupEntity : TriggerEntity
{
    public DoorEntity doorEntity;

    public void Setup()
    {
        onTrigger = OnTrigger;
        layerMasks = new [] { typeof(PlayerEntity) };
    }
    
    public override void OnEnableObject()
    {
        base.OnEnableObject();
        Body.SetActive(true);
    }
    
    public override void OnDisableObject()
    {
        Body.SetActive(false);
    }

    private void OnTrigger(IEntity obj)
    {
        doorEntity.Open();
        DoDie();
    }
}
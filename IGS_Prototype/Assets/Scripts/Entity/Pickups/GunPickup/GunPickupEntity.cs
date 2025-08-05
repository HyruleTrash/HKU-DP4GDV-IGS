
public class GunPickupEntity : TriggerEntity
{
    public int gunId;

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
        PlayerEntity playerEntity = (PlayerEntity)obj;
        playerEntity.gunHandler.AddGun(GunLookup.Instance.GetBuilder(gunId).Build(), gunId);
        DoDie();
    }
}
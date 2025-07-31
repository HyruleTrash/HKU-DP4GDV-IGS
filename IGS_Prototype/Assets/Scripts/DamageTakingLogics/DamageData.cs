public class DamageData
{
    public float damage;
    public string damageText = "";

    public DamageData(float damage)
    {
        this.damage = damage;
    }

    public DamageData(float damage, string damageText)
    {
        this.damage = damage;
        this.damageText = damageText;
    }
}
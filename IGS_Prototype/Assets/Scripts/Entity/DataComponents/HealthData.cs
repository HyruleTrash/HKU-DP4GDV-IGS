using UnityEngine;

[CreateAssetMenu(fileName = "HealthData", menuName = "EntityData/GenericComponents/HealthData")]
public class HealthData : ScriptableObject
{
    public int maxHealth;

    public HealthSystem Generate()
    {
        return new HealthSystem(this);
    }
}
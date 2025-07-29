using UnityEngine;

public abstract class LevelDataEntity : ScriptableObject
{
    [Header("Entity Data")]
    [SerializeField] protected Vector3 position;

    public abstract void Load();
}

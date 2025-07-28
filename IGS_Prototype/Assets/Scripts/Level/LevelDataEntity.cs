using UnityEngine;

public abstract class LevelDataEntity : ScriptableObject
{
    protected Vector3 position;

    public abstract void Load();
}

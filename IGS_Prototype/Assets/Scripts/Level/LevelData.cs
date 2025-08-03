using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "LevelManagement/LevelData")]
public class LevelData : ScriptableObject
{
    [SerializeField] private List<LevelDataEntity> entities;
    [SerializeField] private float levelTime;

    public void Load()
    {
        foreach (LevelDataEntity e in entities)
        {
            e.Load();
        }
        LevelTimer.Instance.Setup(levelTime);
    }
}

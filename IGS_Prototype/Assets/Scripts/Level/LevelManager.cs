using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelManager", menuName = "LevelManagement/LevelManager")]
public class LevelManager : ScriptableObject
{
    [SerializeField] public int currentLevel = 0;
    [SerializeField] private List<LevelData> levels;

    public void LoadLevel(int level)
    {
        if (level > levels.Count || level < 0)
            return;
        levels[level].Load();
    }

    public void AdvanceLevel()
    {
        currentLevel++;
        if (currentLevel < levels.Count)
        {
            LoadLevel(currentLevel);
            return;
        }
        
        currentLevel = 0;
        Game.instance.TriggerWinScreen();
    }
}

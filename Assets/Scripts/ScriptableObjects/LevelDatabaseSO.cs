using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level Database SO")]
public class LevelDatabaseSO : ScriptableObject {
    public List<LevelConfigSO> allLevels;

    public LevelConfigSO GetLevelConfig(int levelNum) {
        return allLevels.Find(l => l.levelNumber == levelNum);
    }

    public int GetLevelsCount() {
        return allLevels.Count;
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

// Level Data Model
[System.Serializable]
public class LevelSaveData {
    public int levelNumber;
    public bool isUnlocked;
    public int starsCount; // 0 to 3
    public int highestScore;
    public int bestStarsCount;
}

// Game Data Model
[System.Serializable]
public class GameSaveData {
    public List<LevelSaveData> levelDataList = new List<LevelSaveData>();
}

public static class SaveManager {


    private const string SAVE_KEY = "SpaceLander_SaveKey";


    public static void SaveGame(GameSaveData gameSaveData) {
        string json = JsonUtility.ToJson(gameSaveData);
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();
    }

    public static GameSaveData LoadGame() {
        if (PlayerPrefs.HasKey(SAVE_KEY)) {
            string json = PlayerPrefs.GetString(SAVE_KEY);
            return JsonUtility.FromJson<GameSaveData>(json);
        }


        // create new save data
        GameSaveData newGameSaveData = new GameSaveData();

        for (int i = 1; i <= 10; i++) {
            newGameSaveData.levelDataList.Add(new LevelSaveData {
                levelNumber = i,
                isUnlocked = i == 1,
                starsCount = 0,
                bestStarsCount = 0,
                highestScore = 0

            });
        }

        return newGameSaveData;
    }

    public static void UpdateGameProgress(int levelNumber, int score, int stars) {
        GameSaveData gameSaveData = LoadGame();

        // update current level stats
        LevelSaveData currentLevel = gameSaveData.levelDataList.Find(l => l.levelNumber == levelNumber);
        if (currentLevel != null) {
            if (score > currentLevel.highestScore) {
                currentLevel.highestScore = score;
            }
            if (stars > currentLevel.bestStarsCount) {
                currentLevel.bestStarsCount = stars;
            }
        }


        // unlock next level
        LevelSaveData nextLevel = gameSaveData.levelDataList.Find(l => l.levelNumber == levelNumber + 1);
        if (nextLevel != null) {
            nextLevel.isUnlocked = true;
        }


        SaveGame(gameSaveData);

    }

}
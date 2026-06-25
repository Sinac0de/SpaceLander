using UnityEngine;


[CreateAssetMenu(menuName = "LevelConfigSO")]
public class LevelConfigSO : ScriptableObject {
    public int levelNumber;
    public GameLevel levelPrefab;

    [Header("Star Score Thresholds")]
    public int oneStarScore;
    public int twoStarScore;
    public int threeStarScore;


}

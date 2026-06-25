using System;
using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static int totalScore = 0;
    public static int LevelNumber = 1;

    public static void ResetStaticData() {
        totalScore = 0;
        LevelNumber = 1;
    }

    public static GameManager Instance { get; private set; }

    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnPaused;

    [Header("Level Configurations")]
    [SerializeField] private LevelDatabaseSO levelDatabase; 
    [SerializeField] private CinemachineCamera cinemachineCamera;

    private LevelConfigSO currentLevelConfig;
    private int score;
    private float timer;
    private bool isTimerActive;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        Lander.Instance.OnCoinPickup += Lander_OnCoinPickup;
        Lander.Instance.OnLanded += Lander_OnLanded;
        Lander.Instance.OnStateChanged += Lander_OnStateChanged;

        InputManager.Instance.OnPausePerformed += InputManager_OnPausePerformed;

        LoadCurrentLevel();
    }

    private void InputManager_OnPausePerformed(object sender, EventArgs e) {
        PauseUnPauseGame();
    }

    private void Lander_OnStateChanged(object sender, Lander.OnStateChangedEventArgs e) {
        if (e.state == Lander.State.Normal) {
            isTimerActive = true;
        } else if (e.state == Lander.State.GameOver) {
            isTimerActive = false;
        }
    }

    private void Lander_OnCoinPickup(object sender, EventArgs e) {
        score += 100; 
    }

    private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e) {
        if (e.landingType == Lander.LandingType.Successful) {
            
            score += e.score;
            LevelCompletedSuccessfully();
        }
    }

    private void Update() {
        if (isTimerActive) {
            timer += Time.deltaTime;
        }
    }

    private void LoadCurrentLevel() {
        currentLevelConfig = levelDatabase.GetLevelConfig(LevelNumber);

        if (currentLevelConfig != null && currentLevelConfig.levelPrefab != null) {
            Instantiate(currentLevelConfig.levelPrefab, Vector3.zero, Quaternion.identity);

        } else {
            Debug.LogError("Level Config or Prefab is missing for Level: " + LevelNumber);
        }
    }

    public void LevelCompletedSuccessfully() {
        int earnedStars = CalculateStars(score);

        SaveManager.UpdateGameProgress(LevelNumber, earnedStars, score);

        totalScore += score;
    }

    private int CalculateStars(int currentScore) {
        if (currentLevelConfig == null) return 0;

        if (currentScore >= currentLevelConfig.threeStarScore) return 3;
        if (currentScore >= currentLevelConfig.twoStarScore) return 2;
        if (currentScore >= currentLevelConfig.oneStarScore) return 1;

        return 0; 
    }

    public GameLevel GetGameLevel() {
        LevelConfigSO config = levelDatabase.GetLevelConfig(LevelNumber);
        return config != null ? config.levelPrefab : null;
    }

    public int GetScore() {
        return score;
    }

    public int GetTotalScore() {
        return totalScore;
    }

    public float GetTimer() {
        return timer;
    }

    public void LoadNextLevel() {
        LevelNumber++;

        if (GetGameLevel() != null) {
            SceneLoader.LoadScene(SceneLoader.Scene.GameScene);
        } else {
            SceneLoader.LoadScene(SceneLoader.Scene.GameOverScene);
        }
    }

    public void RetryLevel() {
        SceneLoader.LoadScene(SceneLoader.Scene.GameScene);
    }

    public int GetLevelNumber() {
        return LevelNumber;
    }

    private void PauseUnPauseGame() {
        if (Time.timeScale == 0f) {
            UnPauseGame();
        } else if (Time.timeScale == 1f) {
            PauseGame();
        }
    }

    public void PauseGame() {
        Time.timeScale = 0f;
        OnGamePaused?.Invoke(this, EventArgs.Empty);
    }

    public void UnPauseGame() {
        Time.timeScale = 1f;
        OnGameUnPaused?.Invoke(this, EventArgs.Empty);
    }
}

using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;


public class GameManager : MonoBehaviour {

    private static int totalScore = 0;
    private static int LevelNumber = 1;

    public static void ResetStaticData() {
        totalScore = 0;
        LevelNumber = 1;
    }


    public static GameManager Instance { get; private set; }

    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnPaused;


    [SerializeField] private List<GameLevel> GameLevelsList;
    [SerializeField] private CinemachineCamera cinemachineCamera;

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

    private void InputManager_OnPausePerformed(object sender, System.EventArgs e) {
        PuaseUnPauseGame();
    }

    private void Lander_OnStateChanged(object sender, Lander.OnStateChangedEventArgs e) {
        isTimerActive = e.state == Lander.State.Normal;

        if (e.state == Lander.State.Normal) {
            cinemachineCamera.Target.TrackingTarget = Lander.Instance.transform;
            CinemachineCameraZoom2D.Instance.SetNormalOrthographicSize();
        }
    }

    private void Update() {
        if (isTimerActive) {
            timer += Time.deltaTime;
        }
    }

    private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e) {
        AddScore(e.score);
    }

    private void Lander_OnCoinPickup(object sender, System.EventArgs e) {
        AddScore(500);
    }


    private void AddScore(int scoreAmount) {
        score += scoreAmount;
    }

    private void LoadCurrentLevel() {
        GameLevel spawnedLevel = GetGameLevel();

        Lander.Instance.transform.position = spawnedLevel.GetLanderSpawnPosition();

        cinemachineCamera.Target.TrackingTarget = spawnedLevel.GetInitialCameraTransform();
        CinemachineCameraZoom2D.Instance.SetOrthographicSize(spawnedLevel.GetZoomedOutOrthographicSize());
    }

    public GameLevel GetGameLevel() {
        foreach (GameLevel gameLevel in GameLevelsList) {
            if (gameLevel.GetLevelNumber() == LevelNumber) {
                GameLevel spawnedLevel = Instantiate(gameLevel, Vector3.zero, Quaternion.identity);
                return spawnedLevel;
            }
        }
        return null;
    }


    public int GetScore() {
        return score;
    }

    public float GetTimer() {
        return timer;
    }

    public void LoadNextLevel() {
        LevelNumber++;
        totalScore += score;
        if (GetGameLevel()) {
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

    private void PuaseUnPauseGame() {
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

    public int GetTotalScore() {
        return totalScore;
    }


}

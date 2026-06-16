
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    private static int LevelNumber = 1;
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

        LoadCurrentLevel();
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
        foreach (GameLevel gameLevel in GameLevelsList) {
            if (gameLevel.GetLevelNumber() == LevelNumber) {
                GameLevel spawnedLevel = Instantiate(gameLevel, Vector3.zero, Quaternion.identity);
                Lander.Instance.transform.position = spawnedLevel.GetLanderSpawnPosition();

                cinemachineCamera.Target.TrackingTarget = spawnedLevel.GetInitialCameraTransform();
                CinemachineCameraZoom2D.Instance.SetOrthographicSize(spawnedLevel.GetZoomedOutOrthographicSize());
            }
        }
    }


    public int GetScore() {
        return score;
    }

    public float GetTimer() {
        return timer;
    }

    public void LoadNextLevel() {
        LevelNumber++;
        SceneManager.LoadScene(0);
    }

    public void RetryLevel() {
        SceneManager.LoadScene(0);
    }


}

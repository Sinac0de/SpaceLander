using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button musicVolumeButton;
    [SerializeField] private Button soundEffectsVolumeButton;
    [SerializeField] private TextMeshProUGUI musicVolumeTextMesh;
    [SerializeField] private TextMeshProUGUI soundEffectsVolumeTextMesh;

    private void Awake() {
        resumeButton.onClick.AddListener(() => {
            GameManager.Instance.UnPauseGame();
        });

        mainMenuButton.onClick.AddListener(() => {
            Time.timeScale = 1f;
            SceneLoader.LoadScene(SceneLoader.Scene.MainMenuScene);
        });

        musicVolumeButton.onClick.AddListener(() => {
            MusicManager.Instance.ChangeMusicVolume();
            musicVolumeTextMesh.text = "Music: " + MusicManager.Instance.GetMusicVolume();
        });

        soundEffectsVolumeButton.onClick.AddListener(() => {
            SoundManager.Instance.ChangeSoundVolume();
            soundEffectsVolumeTextMesh.text = "Sound: " + SoundManager.Instance.GetSoundVolume();
        });

    }

    private void Start() {
        Hide();

        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnPaused += GameManager_OnGameUnPaused;
        soundEffectsVolumeTextMesh.text = "Sound: " + SoundManager.Instance.GetSoundVolume(); 
    }

    private void GameManager_OnGameUnPaused(object sender, EventArgs e) {
        Hide();
    }

    private void GameManager_OnGamePaused(object sender, EventArgs e) {
        Show();
    }

    private void Show() {
        gameObject.SetActive(true);
        resumeButton.Select();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

}

using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public static UIManager Instance { get; private set; }

    [SerializeField] private CanvasGroup blackScreen;
    [SerializeField] private float transitionDuration;
    private List<BasePanel> registeredPanels = new List<BasePanel>();


    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // make sure black screen is initially hidden
            if (blackScreen != null) {
                blackScreen.alpha = 0;
                blackScreen.blocksRaycasts = false;
                blackScreen.gameObject.SetActive(false);
            }
        } else {
            Destroy(gameObject);
        }
    }

    private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    // GeneralBlackScreenTransition
    public void ShowBlackScreen(bool show) {
        blackScreen.gameObject.SetActive(true);
        blackScreen.DOFade(show ? 1 : 0, 0.5f).OnComplete(() => {
            if (!show) blackScreen.gameObject.SetActive(false);
        });
    }


    // Transition between Scenes
    public void LoadSceneWithTransition(SceneLoader.Scene scene) {
        blackScreen.gameObject.SetActive(true);
        blackScreen.blocksRaycasts = true;

        blackScreen.DOFade(1, transitionDuration).OnComplete(() => {
            SceneLoader.LoadScene(scene);
        });
    }

    // auto Fade-out on Scene Loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        blackScreen.DOFade(0, transitionDuration).OnComplete(() => {
            blackScreen.gameObject.SetActive(false);
            blackScreen.blocksRaycasts = false;
        });
    }

    // Transition between UI Panels
    public void OpenPanelWithTransition(BasePanel panelToOpen) {
        blackScreen.gameObject.SetActive(true);
        blackScreen.blocksRaycasts = true;

        // 1. Screen Gets Black
        blackScreen.DOFade(1, transitionDuration).OnComplete(() => {
            // 2. Close all the panels
            foreach (var p in registeredPanels) {
                if (p != null) {
                    p.gameObject.SetActive(false);
                }
            }

            // 3. Open the new Panel
            panelToOpen.Open();

            // 4. Show the panel
            blackScreen.DOFade(0, transitionDuration).OnComplete(() => {
                blackScreen.gameObject.SetActive(false);
                blackScreen.blocksRaycasts = false;
            });
        });
    }


    // Add Panel to the panels list on the scene
    public void RegisterPanel(BasePanel panel) {
        if (!registeredPanels.Contains(panel))
            registeredPanels.Add(panel);
    }
}
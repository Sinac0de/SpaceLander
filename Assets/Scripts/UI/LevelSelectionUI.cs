using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionUI : MonoBehaviour {
    [SerializeField] private LevelDatabaseSO levelDatabase;
    [SerializeField] private GameObject levelButtonPrefab;
    [SerializeField] private Transform gridContainer;
    [SerializeField] private Button backToMainMenuButton;
    [SerializeField] private MainMenuUI mainMenuPanel;

    private void Start() {
        GenerateLevelButtons();

        Hide();

        backToMainMenuButton.onClick.AddListener(() => {
            Hide();
            mainMenuPanel.Show();
        });
    }

    private void GenerateLevelButtons() {
        GameSaveData saveData = SaveManager.LoadGame();

        foreach (LevelConfigSO levelConfig in levelDatabase.allLevels) {
            GameObject btnObj = Instantiate(levelButtonPrefab, gridContainer);
            LevelButtonUI buttonUI = btnObj.GetComponent<LevelButtonUI>();

            LevelSaveData saveStatus = saveData.levelDataList.Find(l => l.levelNumber == levelConfig.levelNumber);

            bool isUnlocked = saveStatus != null && saveStatus.isUnlocked;
            int bestStars = saveStatus != null ? saveStatus.bestStarsCount : 0;

            // Button display setup
            buttonUI.Setup(levelConfig.levelNumber, isUnlocked, bestStars);

            // Button click setup
            if (isUnlocked) {
                int levelToLoad = levelConfig.levelNumber;
                buttonUI.button.onClick.AddListener(() => {
                    Hide();
                    GameManager.LevelNumber = levelToLoad;
                    SceneLoader.LoadScene(SceneLoader.Scene.GameScene);
                });
            }
        }
    }


    public void Show() {
        gameObject.SetActive(true);
        backToMainMenuButton.Select();
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
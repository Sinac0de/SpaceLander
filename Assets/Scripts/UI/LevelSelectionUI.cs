using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionUI : BasePanel
{
    [SerializeField] private LevelDatabaseSO levelDatabase;
    [SerializeField] private GameObject levelButtonPrefab;
    [SerializeField] private Transform gridContainer;
    [SerializeField] private Button backToMainMenuButton;
    [SerializeField] private MainMenuUI mainMenuPanel;

    protected override void Awake()
    {
        base.Awake();
        GenerateLevelButtons();

        backToMainMenuButton.onClick.AddListener(() => {
            UIManager.Instance.OpenPanelWithTransition(mainMenuPanel);
        });
    }

    private void GenerateLevelButtons()
    {
        GameSaveData saveData = SaveManager.LoadGame();

        foreach (LevelConfigSO levelConfig in levelDatabase.allLevels)
        {
            GameObject btnObj = Instantiate(levelButtonPrefab, gridContainer);
            LevelButtonUI buttonUI = btnObj.GetComponent<LevelButtonUI>();

            LevelSaveData saveStatus = saveData.levelDataList.Find(l => l.levelNumber == levelConfig.levelNumber);
            bool isUnlocked = saveStatus != null && saveStatus.isUnlocked;
            int bestStars = saveStatus != null ? saveStatus.bestStarsCount : 0;

            buttonUI.Setup(levelConfig.levelNumber, isUnlocked, bestStars);

            if (isUnlocked)
            {
                int levelToLoad = levelConfig.levelNumber;
                buttonUI.button.onClick.AddListener(() => {
                    GameManager.LevelNumber = levelToLoad;
                    UIManager.Instance.LoadSceneWithTransition(SceneLoader.Scene.GameScene);
                });
            }
        }
    }

    public override void Open()
    {
        base.Open();
        backToMainMenuButton.Select();
    }
}
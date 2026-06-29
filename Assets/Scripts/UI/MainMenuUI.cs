using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : BasePanel
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button quitGameButton;
    [SerializeField] private LevelSelectionUI levelSelectionPanel;

    protected override void Awake()
    {
        base.Awake(); // Initial Setup from BasePanel

        startGameButton.onClick.AddListener(() => {
            
            UIManager.Instance.OpenPanelWithTransition(levelSelectionPanel);
        });

        quitGameButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }

    public override void Open()
    {
        base.Open();
        startGameButton.Select();
    }
}
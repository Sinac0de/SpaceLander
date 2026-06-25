using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button quitGameButton;
    [SerializeField] private LevelSelectionUI levelSelectionPanel;


    private void Awake() {
        startGameButton.Select();

        startGameButton.onClick.AddListener(() => {
            levelSelectionPanel.Show();
        });

        quitGameButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }

    public void Show() {
        gameObject.SetActive(true);
        startGameButton.Select();
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

}

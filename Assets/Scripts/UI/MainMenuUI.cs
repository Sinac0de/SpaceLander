using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button quitGameButton;

    private void Awake() {
        startGameButton.Select();

        startGameButton.onClick.AddListener(() => {
            GameManager.ResetStaticData();
            SceneLoader.LoadScene(SceneLoader.Scene.GameScene);
        });

        quitGameButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }

}

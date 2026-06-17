using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour {
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private TextMeshProUGUI totalScoreTextMesh;

    private void Awake() {
        mainMenuButton.Select();

        mainMenuButton.onClick.AddListener(() => {
            SceneLoader.LoadScene(SceneLoader.Scene.MainMenuScene);
        });
    }

    private void Start() {
        totalScoreTextMesh.text = "Total Score:" + "\n" + GameManager.Instance.GetTotalScore();
    }
}

using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LandedUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI statsTextMesh;
    [SerializeField] private TextMeshProUGUI bannerTextMesh;
    [SerializeField] private Button nextButton;
    [SerializeField] private TextMeshProUGUI nextButtonTextMesh;

    private Action OnNextButtonClicked;


    private void Awake() {
        nextButton.onClick.AddListener(() => {
            OnNextButtonClicked();
        });
    }

    private void Start() {
        Lander.Instance.OnLanded += Lander_OnLanded;
        Hide();
    }

    private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e) {
        if (e.landingType == Lander.LandingType.Successful) {
            bannerTextMesh.text = "Successful Landing!";
            nextButtonTextMesh.text = "Continue";
            OnNextButtonClicked = GameManager.Instance.LoadNextLevel;
        } else {
            bannerTextMesh.text = "<color=#ff0000>CRASH!</color>";
            nextButtonTextMesh.text = "Restart";
            OnNextButtonClicked = GameManager.Instance.RetryLevel;
        }

        statsTextMesh.text = Mathf.Round(e.landingSpeed * 2f) + "\n" +
            Mathf.Round(e.landingAngle * 100f) + "\n" +
            "x" + e.multiplier + "\n" +
            e.score;

        Show();
    }

    private void Show() {
        gameObject.SetActive(true);
        nextButton.Select();
    }



    private void Hide() {
        gameObject.SetActive(false);
    }
}

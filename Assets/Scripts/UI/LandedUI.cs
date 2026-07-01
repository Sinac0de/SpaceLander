using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;

public class LandedUI : BasePanel {
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI statsTextMesh;
    [SerializeField] private TextMeshProUGUI bannerTextMesh;
    [SerializeField] private TextMeshProUGUI nextOrRestartButtonText;
    [SerializeField] private Button nextOrRestartButton;
    [SerializeField] private Button goToMainMenuButton;
    [SerializeField] private Transform starsContainer;
    [SerializeField] private Image[] starIconsFilled;

    private Action onNextButtonClicked;
    private Sequence dotWeenSequence;

    protected override void Awake() {
        base.Awake();

        // Setup persistent listeners once
        nextOrRestartButton.onClick.AddListener(() => onNextButtonClicked?.Invoke());
        goToMainMenuButton.onClick.AddListener(() =>
            UIManager.Instance.LoadSceneWithTransition(SceneLoader.Scene.MainMenuScene));
    }

    private void Start() {
        Lander.Instance.OnLanded += Lander_OnLanded;
    }

    private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e) {
        Setup(e);
    }

    public override void Open() {
        base.Open();
        AnimateResults();
    }

    public override void Close() {
        // Kill sequence to prevent memory leaks and unexpected behavior
        dotWeenSequence?.Kill();
        base.Close();
    }

    private void Setup(Lander.OnLandedEventArgs e) {
        int collectedStars = 0;

        if (e.landingType == Lander.LandingType.Successful) {
            starsContainer.gameObject.SetActive(true);
            bannerTextMesh.gameObject.SetActive(false);
            onNextButtonClicked = GameManager.Instance.LoadNextLevel;
            nextOrRestartButtonText.text = "Continue";
            collectedStars = GameManager.Instance.CalculateStars(e.score);
        } else {
            starsContainer.gameObject.SetActive(false);
            bannerTextMesh.gameObject.SetActive(true);
            bannerTextMesh.text = "Exploded!";
            bannerTextMesh.color = Color.red;
            onNextButtonClicked = GameManager.Instance.RetryLevel;
            nextOrRestartButtonText.text = "Restart";
        }

        statsTextMesh.text = Mathf.Round(e.landingSpeed * 2f) + "\n" +
            Mathf.Round(e.landingAngle * 100f) + "\n" +
            "x" + e.multiplier + "\n" +
            e.score;


        Debug.Log("Collected: " + collectedStars);

        // Prepare star icons state
        for (int i = 0; i < starIconsFilled.Length; i++)
            starIconsFilled[i].gameObject.SetActive(i < collectedStars);

        Open();
    }

    private void AnimateResults() {
        dotWeenSequence = DOTween.Sequence();

        // Banner punch animation
        dotWeenSequence.Append(bannerTextMesh.transform.DOScale(1.2f, 0.3f).SetEase(Ease.OutBack))
                       .Append(bannerTextMesh.transform.DOScale(1f, 0.2f));

        // Staggered star animation
        for (int i = 0; i < starIconsFilled.Length; i++) {
            if (!starIconsFilled[i].gameObject.activeSelf) continue;

            starIconsFilled[i].transform.localScale = Vector3.zero;
            dotWeenSequence.Append(starIconsFilled[i].transform.DOScale(1, 0.5f).SetEase(Ease.OutElastic));
            dotWeenSequence.AppendInterval(0.1f);
        }
    }
}
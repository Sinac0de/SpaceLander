using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LandedUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI statsTextMesh;
    [SerializeField] private TextMeshProUGUI bannerTextMesh;


    private void Start() {
        Lander.Instance.OnLanded += Lander_OnLanded;
        Hide();
    }

    private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e) {
        if (e.landingType == Lander.LandingType.Successful) {
            bannerTextMesh.text = "Successful Landing!";
        } else {
            bannerTextMesh.text = "<color=#ff0000>CRASH!</color>";
        }

        statsTextMesh.text = Mathf.Round(e.landingSpeed * 2f) + "\n" +
            Mathf.Round(e.landingAngle * 100f) + "\n" +
            "x" + e.multiplier + "\n" +
            e.score;

        Show();
    }

    private void Show() {
        gameObject.SetActive(true);
    }



    private void Hide() {
        gameObject.SetActive(false);
    }
}

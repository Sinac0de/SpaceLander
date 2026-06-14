using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI statsTextMesh;
    [SerializeField] private GameObject leftArrow;
    [SerializeField] private GameObject rightArrow;
    [SerializeField] private GameObject upArrow;
    [SerializeField] private GameObject downArrow;
    [SerializeField] private Image fuelBarImage;


    private void Update() {
        statsTextMesh.text = GameManager.Instance.GetScore() + "\n" +
                             Mathf.Round(GameManager.Instance.GetTimer()) + "\n" +
                             Mathf.Abs(Mathf.Round(Lander.Instance.GetSpeedX() * 10f)) + "\n" +
                             Mathf.Abs(Mathf.Round(Lander.Instance.GetSpeedY() * 10f)) + "\n";

        leftArrow.SetActive(Lander.Instance.GetSpeedX() < 0);
        rightArrow.SetActive(Lander.Instance.GetSpeedX() > 0);
        upArrow.SetActive(Lander.Instance.GetSpeedY() > 0);
        downArrow.SetActive(Lander.Instance.GetSpeedY() < 0);

        fuelBarImage.fillAmount = Lander.Instance.GetFuelAmountNormalized();
    }

}

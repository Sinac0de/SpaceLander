using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButtonUI : MonoBehaviour {
    public Button button;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject lockIcon;
    [SerializeField] private GameObject[] starIcons;

    public void Setup(int levelNum, bool isUnlocked, int starsCount) {
        levelText.text = levelNum.ToString();
        button.interactable = isUnlocked;

        if (lockIcon != null) lockIcon.SetActive(!isUnlocked);

        // hide stars at first
        foreach (var star in starIcons) {
            if (star != null) star.SetActive(false);
        }

        // show stars based on player score for this level
        if (isUnlocked) {
            for (int i = 0; i < starsCount; i++) {
                if (i < starIcons.Length && starIcons[i] != null) {
                    starIcons[i].SetActive(true);
                }
            }
        }
    }
}
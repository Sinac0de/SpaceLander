using UnityEngine;

[CreateAssetMenu(menuName = "SpaceLander/Key Data")]
public class KeyDataSO : ScriptableObject {
    public string keyName;
    public Sprite  sprite;
    public Sprite HUDSprite;
    public Color color = Color.white;
}

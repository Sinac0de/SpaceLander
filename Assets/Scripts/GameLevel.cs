using UnityEngine;

public class GameLevel : MonoBehaviour
{
    [SerializeField] private int levelNumber;
    [SerializeField] private Transform landerSpawnPosition;
    [SerializeField] private Transform initialCameraTransform;
    [SerializeField] private float zoomedOutOrthographicSize;


    public int GetLevelNumber() {
        return levelNumber;
    }

    public Vector3 GetLanderSpawnPosition() {
        return landerSpawnPosition.position;
    }


    public Transform GetInitialCameraTransform() {
        return initialCameraTransform;
    }

    public float GetZoomedOutOrthographicSize() {
        return zoomedOutOrthographicSize;
    }
}

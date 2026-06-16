using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CinemachineCameraZoom2D : MonoBehaviour
{
    private const float NORMAL_ORTHOGRAPHIC_SIZE = 10f;

    public static CinemachineCameraZoom2D Instance { get; private set; }

    [SerializeField] private float zoomSpeed;
    [SerializeField] private CinemachineCamera cinemachineCamera;

    private float orthographicSize;

    private void Awake() {
        Instance = this;
    }


    private void Update() {
        cinemachineCamera.Lens.OrthographicSize = Mathf.Lerp(cinemachineCamera.Lens.OrthographicSize, orthographicSize, Time.deltaTime * zoomSpeed);
    }

    public void SetOrthographicSize(float orthographicSize) {
        this.orthographicSize = orthographicSize;
    }

    public void SetNormalOrthographicSize() {
        this.orthographicSize = NORMAL_ORTHOGRAPHIC_SIZE;
    }

    public float GetOrthographicSize() {
        return orthographicSize;
    }

}

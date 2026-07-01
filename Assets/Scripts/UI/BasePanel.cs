using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class BasePanel : MonoBehaviour {
    protected CanvasGroup canvasGroup;
    [SerializeField] protected bool startHidden = true; //to hide the panels in start

    protected virtual void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();
        // make sure the panel is initially disabled
        if (startHidden) {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
        }

    }

    protected virtual void OnEnable() {
        if (UIManager.Instance != null) {
            UIManager.Instance.RegisterPanel(this);
        }
    }

    protected virtual void OnDisable() { }

    public virtual void Open() {
        gameObject.SetActive(true);
        canvasGroup.blocksRaycasts = true;
        // Tweening
        canvasGroup.DOFade(1, 0.1f);
        transform.DOScale(1, 0.1f).SetEase(Ease.OutBack);
    }

    public virtual void Close() {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.DOFade(0, 0.1f);
        transform.DOScale(0.9f, 0.1f).OnComplete(() => gameObject.SetActive(false));
    }
}
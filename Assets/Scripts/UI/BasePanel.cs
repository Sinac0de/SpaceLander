using UnityEngine;

public abstract class BasePanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    public virtual void Open()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    public virtual void Close()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
}

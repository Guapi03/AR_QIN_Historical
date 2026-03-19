using UnityEngine;
using Vuforia;

public class ARContentTracker : MonoBehaviour
{
    public ARContentData contentData;

    private ObserverBehaviour observer;

    void Start()
    {
        observer = GetComponent<ObserverBehaviour>();

        if (observer != null)
        {
            observer.OnTargetStatusChanged += OnTargetStatusChanged;
        }
    }

    void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        if (status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED)
        {
            UIManager.Instance.SetCurrentContent(contentData);
        }
        else
        {
            UIManager.Instance.ClearContent();
        }
    }
}
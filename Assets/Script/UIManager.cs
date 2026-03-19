using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI")]
    public GameObject infoPanel;
    public Image descriptionImage;

    private ARContentData currentContent;
    private AudioSource audioSource;

    void Awake()
    {
        Instance = this;

        audioSource = GetComponent<AudioSource>();

        if (infoPanel != null)
            infoPanel.SetActive(false);
    }

    // 扫描到 ImageTarget
    public void SetCurrentContent(ARContentData data)
    {
        currentContent = data;
    }

    // target lost
    public void ClearContent()
    {
        currentContent = null;

        if (infoPanel != null)
            infoPanel.SetActive(false);

        if (audioSource != null && audioSource.isPlaying)
            audioSource.Stop();
    }

    // Toggle Info Panel
    public void ToggleInfoPanel()
    {
        if (currentContent == null) return;

        bool isActive = infoPanel.activeSelf;

        infoPanel.SetActive(!isActive);

        if (!isActive)
        {
            descriptionImage.sprite = currentContent.descriptionImage;
        }
    }

    // Toggle Audio
    public void ToggleAudio()
    {
        if (currentContent == null) return;

        if (currentContent.audioClip == null) return;

        if (!audioSource.isPlaying)
        {
            audioSource.clip = currentContent.audioClip;
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
}
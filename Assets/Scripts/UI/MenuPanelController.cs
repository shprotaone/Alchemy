using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanelController : MonoBehaviour
{
    [SerializeField] private InGameTimeController _ingameTimeController;
    [SerializeField] private RectTransform _panel;

    [SerializeField] private RectTransform _closePosition;
    [SerializeField] private RectTransform _openPosition;

    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _openButton;

    [SerializeField] private float _openDuration;
    private void Start()
    {
        _openButton.onClick.AddListener(Open);
        _closeButton.onClick.AddListener(Close);
    }

    private void Open()
    {
        _panel.gameObject.SetActive(true);
        _panel.DOAnchorPos(_openPosition.anchoredPosition, _openDuration, false)
                 .OnComplete(()=>_ingameTimeController.PauseGame());
    }

    private void Close()
    {
        _ingameTimeController.ResumeGame();
        _panel.DOAnchorPos(_closePosition.anchoredPosition, _openDuration, false)
                 .OnComplete(() => _panel.gameObject.SetActive(false));
    }

    private void OnDestroy()
    {
        _openButton?.onClick.RemoveListener(Open);
        _closeButton?.onClick.RemoveListener(Close);
    }
}

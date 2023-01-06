using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanelController : MonoBehaviour
{
    public event Action<bool> OnInterract;

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

    public void Open()
    {
        OnInterract?.Invoke(false);
        _panel.gameObject.SetActive(true);
        _panel.DOAnchorPos(_openPosition.anchoredPosition, _openDuration, false);
    }

    public void Close()
    {
        OnInterract?.Invoke(true);
        _ingameTimeController.ResumeGame();
        _panel.DOAnchorPos(_closePosition.anchoredPosition, _openDuration, false).OnComplete(() => _panel.gameObject.SetActive(false));
    }
}

using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class VisitorCountSystemView : MonoBehaviour
{
    [SerializeField] private Transform _hidePosition;
    [SerializeField] private Transform _showPosition;
    [SerializeField] private TMP_Text _countText;
    [SerializeField] private TMP_Text _startCountText;
    [SerializeField] private float _moveTime;

    private RectTransform _position;

    private void Awake()
    {
        _position = GetComponent<RectTransform>();
        _position.anchoredPosition =_showPosition.position;

        LevelInitializator.OnLevelStarted += Show;
        LevelInitializator.OnLevelEnded += Hide;
    }

    private void OnDisable()
    {
        LevelInitializator.OnLevelStarted -= Show;
        LevelInitializator.OnLevelEnded -= Hide;
    }

    private void Hide()
    {
        _position.DOMove(_hidePosition.position, _moveTime);
    }

    private void Show()
    {
        _position.DOMove(_showPosition.position, _moveTime);
    }

    public void SetStartCountText(int count)
    {
        _startCountText.text = count.ToString();
    }

    public void RefreshText(int count)
    {
        _countText.text = count.ToString();
    }
}

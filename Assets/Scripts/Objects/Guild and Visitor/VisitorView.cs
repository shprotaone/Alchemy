using DG.Tweening;
using TMPro;
using UnityEngine;

public class VisitorView : MonoBehaviour
{
    private const float duration = 0.5f;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private SpriteRenderer _visitorImage;

    public void Rising()
    {
        this.gameObject.SetActive(true);
        _timerText.enabled = false;

        DOTween.ToAlpha(() => _visitorImage.color, x => _visitorImage.color = x, 1, duration);
    }

    public void Fading()
    {
        StopAllCoroutines();

        _timerText.gameObject.SetActive(false);

        DOTween.ToAlpha(() => _visitorImage.color, x => _visitorImage.color = x, 0, duration).OnComplete(() => this.gameObject.SetActive(false));
    }

    public void UpdateTimerText(int currentTime)
    {
        _timerText.text = currentTime.ToString();
    }

    public void BrightVisitor(string sortingLayerName)
    {
        _visitorImage.sortingLayerName = sortingLayerName;
        this.GetComponentInChildren<Canvas>().sortingLayerName = sortingLayerName;
    }
}

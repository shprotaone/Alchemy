using DG.Tweening;
using TMPro;
using UnityEngine;

public class VisitorView : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private SpriteRenderer _visitorImage;

    public void Rising()
    {
        this.gameObject.SetActive(true);

        DOTween.ToAlpha(() => _visitorImage.color, x => _visitorImage.color = x, 1, 1);
    }

    public void Fading()
    {
        StopAllCoroutines();

        _timerText.gameObject.SetActive(false);

        DOTween.ToAlpha(() => _visitorImage.color, x => _visitorImage.color = x, 0, 1).OnComplete(() => this.gameObject.SetActive(false));
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

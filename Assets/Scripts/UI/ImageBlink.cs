using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageBlink : MonoBehaviour
{
    [SerializeField] private Image _image;

    private Tween _blinkTween;

    public void StartBlink()
    {
        gameObject.SetActive(true);
        _blinkTween = _image.DOFade(0, 1).SetLoops(-1, LoopType.Yoyo);
    }

    public void DisableBlink()
    {
        _blinkTween.Kill();
        _image.gameObject.SetActive(false);
    }
}

using DG.Tweening;
using TMPro;
using UnityEngine;

public class VisitorView : MonoBehaviour
{
    private const float risingDuration = 1.5f;
    private const float fadingDuration = 1.5f;

    private readonly Vector3 _startScale = new Vector3(0.5f, 0.5f, 0.5f);
    private readonly Vector3 _startPos = new Vector3(5,4,0);
    private readonly Vector3 _endPos = new Vector3(6, -4, 0);
    private readonly Vector3 _tradePos = new Vector3(-3, -1.6f, 0);
    private readonly Vector3 _rotate = new Vector3(0, 0, 12);

    [SerializeField] private SpriteRenderer _visitorImage;
    [SerializeField] private PotionTaskView _taskView;

    public void Rising()
    {  
        this.gameObject.SetActive(true);

        _visitorImage.transform.DOScale(1, risingDuration);
        _visitorImage.transform.DOMove(_tradePos, risingDuration).OnComplete(_taskView.Rising);
        _visitorImage.transform.DOLocalRotate(_rotate, risingDuration / 6)
            .SetLoops(6, LoopType.Yoyo)
            .SetEase(Ease.Linear).OnComplete(() => _visitorImage.transform.rotation = Quaternion.Euler(Vector3.zero));
            

        DOTween.ToAlpha(() => _visitorImage.color, x => _visitorImage.color = x, 1, risingDuration);
    }

    public void Fading()
    {
        StopAllCoroutines();

        _visitorImage.transform.DOScale(0.5f, fadingDuration);
        _visitorImage.transform.DOMove(_endPos, fadingDuration)
            .OnComplete(() => {
                _visitorImage.transform.position = -_startPos;
                _taskView.Fading();
            });

        _visitorImage.transform.DOLocalRotate(new Vector3(0, 0, 12), fadingDuration / 4)
            .SetLoops(4, LoopType.Yoyo)
            .SetEase(Ease.Linear);

        DOTween.ToAlpha(() => _visitorImage.color, x => _visitorImage.color = x, 0, fadingDuration)
            .OnComplete(() =>
            {              
                this.gameObject.SetActive(false);
                RefreshView();
            });
    }

    public void RefreshView()
    {
        _visitorImage.color = new Color(1,1,1,1);
        _visitorImage.transform.localScale = _startScale;      
        _visitorImage.transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    public void BrightVisitor(string sortingLayerName)
    {
        _visitorImage.sortingLayerName = sortingLayerName;
        this.GetComponentInChildren<Canvas>().sortingLayerName = sortingLayerName;
    }
}

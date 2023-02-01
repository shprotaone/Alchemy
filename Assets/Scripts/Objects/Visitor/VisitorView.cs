using DG.Tweening;
using TMPro;
using UnityEngine;

public class VisitorView : MonoBehaviour
{
    private const float RisingDuration = 0.5f;
    private const float FadingDuration = 2f;

    private Vector3 _startScale = new Vector3(0.5f, 0.5f, 0.5f);
    private Vector3 _downPos = new Vector3(5,4,0);
    private Vector3 _forwardPos = new Vector3(6, -4, 0);
    private Vector3 _upPose = new Vector3(-3, -1.6f, 0);
    private Vector3 _rotate = new Vector3(0, 0, 12);

    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private SpriteRenderer _visitorImage;
    [SerializeField] private PotionTaskView _taskView;

    public void Rising()
    {  
        this.gameObject.SetActive(true);
        _timerText.enabled = false;

        _visitorImage.transform.DOScale(1, 2);
        _visitorImage.transform.DOMove(_upPose, 2).OnComplete(_taskView.RisingTask);
        _visitorImage.transform.DOLocalRotate(_rotate, 0.3f)
            .SetLoops(6, LoopType.Yoyo)
            .SetEase(Ease.Linear);
            

        DOTween.ToAlpha(() => _visitorImage.color, x => _visitorImage.color = x, 1, RisingDuration);
    }

    public void Fading()
    {
        StopAllCoroutines();

        _timerText.gameObject.SetActive(false);

        _visitorImage.transform.DOScale(0.5f, 2);
        _visitorImage.transform.DOMove(_forwardPos, FadingDuration).OnComplete(() => _visitorImage.transform.position = -_downPos);
        _visitorImage.transform.DOLocalRotate(new Vector3(0, 0, 12), 0.3f)
            .SetLoops(4, LoopType.Yoyo);

        DOTween.ToAlpha(() => _visitorImage.color, x => _visitorImage.color = x, 0, 3)
            .OnComplete(() =>
            {              
                this.gameObject.SetActive(false);
                
            });
    }

    public void RefreshView()
    {
        _visitorImage.color = new Color(1,1,1,1);
        _visitorImage.transform.localScale = _startScale;      
        _visitorImage.transform.localEulerAngles = Vector3.zero;        
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

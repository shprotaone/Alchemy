using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuideController : MonoBehaviour
{
    [SerializeField] private DraggableObjectController _dragController;
    [SerializeField] private Button _nextSlide;
    [SerializeField] private Button _previousSlide;
    [SerializeField] private ScrollRect _contentTransform;
    [SerializeField] private HorizontalLayoutGroup _layoutGroup;
    [SerializeField] private TMP_Text _guideText;
    [SerializeField] private List<RectTransform> _contents;
    [SerializeField] private Languages _text;

    private int _currentContentIndex = 0;
    private void Start()
    {
        _contentTransform.content.anchoredPosition = SetContentPos(0);
        _nextSlide.onClick.AddListener(NextSlide);
        _previousSlide.onClick.AddListener(PrevSlide);
    }

    private void PrevSlide()
    {      
        if(_currentContentIndex > 0)
        {
            _currentContentIndex--;
            Movement();
        }            
    }

    private Vector2 SetContentPos(float rightPos)
    {
        return new Vector2(rightPos + _layoutGroup.padding.left, _contentTransform.content.anchoredPosition.y);
    }

    private void NextSlide()
    {     
        if(_currentContentIndex < _contents.Count-1)
        {
            _currentContentIndex++;            
            Movement();
        }      
    }

    private void Movement()
    {
        Vector2 nextPos = SetContentPos(-_contents[_currentContentIndex].anchoredPosition.x);
        _guideText.DOFade(0, 0.25f);
        _contentTransform.content.DOAnchorPos(nextPos, 0.5f).OnComplete(() =>
        {
            _guideText.DOFade(1, 0.25f);
            _guideText.text = _text._guideTextRU[_currentContentIndex];
        });        
    }

    private void OnDisable()
    {
        _currentContentIndex = 0;
        _dragController?.SetInterract(true);
    }
    private void OnEnable()
    {
        Movement();
        _guideText.text = _text._guideTextRU[_currentContentIndex];
        _dragController?.SetInterract(false);
    }
}

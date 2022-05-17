using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class DragEndDrop : MonoBehaviour,IBeginDragHandler,IEndDragHandler,IDragHandler
{
    private float _moveSpeed = 2;

    private Vector2 _startPosition;
    private Vector2 _endPosition;
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {        
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        _endPosition = _rectTransform.anchoredPosition;
        DestroyObj();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPosition = this.transform.position;
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {        
        _rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("End " + _startPosition);
        CheckObjectType(eventData);
        _canvasGroup.blocksRaycasts = true;
    }

    private void CheckObjectType(PointerEventData eventData)
    {
        if (eventData.pointerEnter.GetComponentInParent<MixingSystemv2>())
        {
            print("Ingredient");
            eventData.pointerEnter.GetComponentInParent<MixingSystemv2>().CheckIngredientIn(this.gameObject);
        }
        else if (eventData.pointerEnter.GetComponent<Bottle>())
        {
            print("Bottle");
        }
        else if (this.gameObject.GetComponent<Ingredient>())
        {           
            _rectTransform.DOAnchorPos(_startPosition, _moveSpeed, false);            //возврат на место
        }
    }

    private void DestroyObj()
    {
        float distance = Vector2.Distance(_startPosition, _endPosition);

        if( distance < 0.01 && distance != 0)
        {
            Destroy(this.gameObject);
        }
    }
}

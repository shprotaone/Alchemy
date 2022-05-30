using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections;

public class DragEndDrop : MonoBehaviour,IBeginDragHandler,IEndDragHandler,IDragHandler
{
    private float _moveSpeed = 3;

    private Vector2 _startPosition;
    private Vector2 _completeBottlePosition;
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {        
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();

        DOTween.SetTweensCapacity(1250, 50);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPosition = _rectTransform.anchoredPosition;
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {        
        _rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        CheckObjectType(eventData);
        _canvasGroup.blocksRaycasts = true;
    }

    private void CheckObjectType(PointerEventData eventData)
    {
        if (eventData.pointerEnter.GetComponentInParent<MixingSystemv2>())
        {
            MixingSystemv2 _mixingSystem = eventData.pointerEnter.GetComponentInParent<MixingSystemv2>();

            bool complete = _mixingSystem.BottleFilled;
            
            if (!complete)
            {
                StartCoroutine(ReplaceBottle(_startPosition));
            }
            else
            {
                _completeBottlePosition = _mixingSystem.CompleteTable;
                StartCoroutine(ReplaceBottle(_completeBottlePosition));
            }           
        }
        else if (this.gameObject.GetComponent<Bottle>())
        {
            StartCoroutine(ReplaceBottle(_startPosition));
        }
        else if (eventData.pointerEnter.GetComponentInChildren<Task>())
        {
            Task task = eventData.pointerEnter.GetComponentInChildren<Task>();
            task.ChekResult(this.GetComponent<Bottle>().PotionInBottle);
            
        }
        else if (this.gameObject.GetComponent<Ingredient>())
        {
            Ingredient ingredient = GetComponent<Ingredient>();
                        
            StartCoroutine(ReplaceIngredient(ingredient));            
        }
    }
    /// <summary>
    /// Возврат ингредиентов
    /// </summary>
    /// <param name="ingredient"></param>
    /// <returns></returns>
    private IEnumerator ReplaceIngredient(Ingredient ingredient)
    {
        float distance = Vector2.Distance(this._rectTransform.anchoredPosition, _startPosition);

        while (distance > 10)
        {
            _rectTransform.DOAnchorPos(_startPosition, _moveSpeed, false);

            yield return new WaitForEndOfFrame();
            distance = Vector2.Distance(this._rectTransform.anchoredPosition, _startPosition);
        }

        ingredient.Slot.IncreaseAmount(1);
        Destroy(this.gameObject);

        yield return null;
    }
    /// <summary>
    /// Возврат бутылок
    /// </summary>
    /// <param name="endPosition"></param>
    /// <returns></returns>
    private IEnumerator ReplaceBottle(Vector2 endPosition)      //добавить присвоение родителя к столу
    {
        float distance = Vector2.Distance(_rectTransform.anchoredPosition, endPosition);

        while (distance > 10)
        {
            _rectTransform.DOAnchorPos(endPosition, _moveSpeed, false);

            yield return new WaitForEndOfFrame();
            distance = Vector2.Distance(_rectTransform.anchoredPosition, endPosition);
        }

        yield return null;
    }
}

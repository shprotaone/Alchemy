using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections;

public class DragEndDropRect : MonoBehaviour,IBeginDragHandler,IEndDragHandler,IDragHandler
{
    private Vector3 _startPosition;
    private RectTransform _emptyBottlePosition;
    private RectTransform _completeBottlePosition;
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    //private Bottle _bottle;
    private Ingredient _ingredient;

    private void Awake()
    {        
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();

        DOTween.SetTweensCapacity(1250, 50);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        CheckObject();
        _startPosition = this.transform.position;
        print("From Drag Pos" + _startPosition);
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
                //ReplaceBottle(_bottle,TableManager.instance.EmptyPotionTable);
            }
            else
            {
                //ReplaceBottle(_bottle, TableManager.instance.FullPotionTable);
            }           
        }
        else if (eventData.pointerEnter.GetComponentInChildren<Task>())
        {
            Task task = eventData.pointerEnter.GetComponentInChildren<Task>();
            //task.ChekResult(_bottle.PotionInBottle);

            //_bottle.ResetBottle();
            //ReplaceBottle(_bottle, TableManager.instance.EmptyPotionTable); ;
            
        }
        else if (this.gameObject.GetComponent<Bottle>())
        {
            //ReplaceBottle(_bottle, TableManager.instance.EmptyPotionTable);
        }
        else if (this.gameObject.GetComponent<Ingredient>())
        {
            Ingredient ingredient = GetComponent<Ingredient>();

            ReplaceIngredient(ingredient);          
        }        
    }

    /// <summary>
    /// Возврат ингредиентов
    /// </summary>
    /// <param name="ingredient"></param>
    /// <returns></returns>
    private void ReplaceIngredient(Ingredient ingredient)
    {               
        ingredient.Slot.IncreaseAmount();
        ingredient.Movement();
    }

    /// <summary>
    /// Возврат бутылок
    /// </summary>
    /// <param name="endPosition"></param>
    /// <returns></returns>
    private void ReplaceBottle(Bottle bottle, Table table)      
    {
        //bottle.Movement(table,_startPosition);
    }

    private void CheckObject()
    {
        if (this.gameObject.GetComponent<Bottle>())
        {
            //_bottle = GetComponent<Bottle>();
        }
        else if (this.gameObject.GetComponent<Ingredient>())
        {
            _ingredient = GetComponent<Ingredient>();
        }
    }
}

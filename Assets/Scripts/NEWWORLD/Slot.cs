using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Slot : MonoBehaviour,IBeginDragHandler,IDragHandler
{
    private Image _slotImage;
    private InitShelfs _initShelfs;
    private IngredientData _ingredientData;
    private TMP_Text _amountText;
    private int _amount;

    private void Start()
    {
        _initShelfs = GetComponentInParent<InitShelfs>();
    }

    public void FillSlot(IngredientData ingredientData)
    {
        _slotImage = GetComponent<Image>();
        _amountText = GetComponentInChildren<TMP_Text>();

        _ingredientData = ingredientData;
        _slotImage.sprite = ingredientData.mainSprite;       
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GameObject ingredientGO = Instantiate(_initShelfs.CurrentPrefab, this.transform);
        ingredientGO.GetComponent<Ingredient>().IngredientData = _ingredientData;
        eventData.pointerDrag = ingredientGO;
        eventData.pointerEnter = null;
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }
}


using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Slot : MonoBehaviour, IBeginDragHandler,IDragHandler
{
    private Image _slotImage;
    private Inventory _initShelfs;
    private IngredientData _ingredientData;
    private TMP_Text _amountText;
    private int _amount;

    private void Start()
    {
        _initShelfs = GetComponentInParent<Inventory>();
    }

    public void FillSlot(IngredientData ingredientData,int value)
    {
        _slotImage = GetComponent<Image>();
        _amountText = GetComponentInChildren<TMP_Text>();
        _amount = value;
        RefreshAmount();

        _ingredientData = ingredientData;
        _slotImage.sprite = ingredientData.mainSprite;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GameObject ingredientGO = Instantiate(_initShelfs.CurrentPrefab, this.transform);
        Ingredient ingredient = ingredientGO.GetComponent<Ingredient>();

        ingredient.IngredientData = _ingredientData;
        ingredient.SetSlot(this);

        DecreaseAmount(1);
        eventData.pointerDrag = ingredientGO;
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void IncreaseAmount(int value)
    {
        _amount += value;
        RefreshAmount();
    }

    public void DecreaseAmount(int value)
    {
        _amount -= value;
        RefreshAmount();
    }

    private void RefreshAmount()
    {
        _amountText.text = _amount.ToString();
    }
}


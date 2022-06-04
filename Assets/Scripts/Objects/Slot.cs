using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Slot : MonoBehaviour, IBeginDragHandler,IDragHandler
{
    private Image _slotImage;
    private Inventory _inventory;
    private IngredientData _ingredientData;
    private TMP_Text _amountText;
    private int _amount;

    private void Start()
    {
        _inventory = GetComponentInParent<Inventory>();
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
        GameObject ingredientGO = Instantiate(_inventory.CurrentPrefab, this.transform);
        Ingredient ingredient = ingredientGO.GetComponent<Ingredient>();

        ingredient.SetIngredientData(_ingredientData);
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
        _inventory.DecreaseIngredient(_ingredientData, value);
        RefreshAmount();
    }

    private void RefreshAmount()
    {
        _amountText.text = _amount.ToString();
    }
}


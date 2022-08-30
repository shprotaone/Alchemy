using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    private Color _hideColor = new Color(0, 0, 0, 0.2f);
    private Color _activeColor = Color.white;

    [SerializeField] private TMP_Text _cost;
    [SerializeField] private TMP_Text _inInventoryValue;
    [SerializeField] private Image _slotImage;    
    [SerializeField] private Button _buyButton;

    private Inventory _inventory;
    private ShopSystem _shopSystem;
    private IngredientData _ingredient;    
    
    public IngredientData IngredienData => _ingredient;
    private void OnEnable()
    {
        _shopSystem = GetComponentInParent<ShopSystem>();

        if(_ingredient == null)
        {
            _shopSystem.FillShop();
        }

        _inventory = _shopSystem.Inventory;
        _buyButton = GetComponentInChildren<Button>();        

        ShowValueInInventory();
    }

    public void FillSlot(IngredientData ingredientData)
    {
        _ingredient = ingredientData;
        _slotImage.sprite = ingredientData.mainSprite;        
        _cost.text = ingredientData.cost.ToString();                   
    }

    public void Buy()
    {
        if (_shopSystem.Transaction(_ingredient.cost))
        {
            _inventory.AddIngredient(_ingredient);
            ShowValueInInventory();
        }        
    }

    private void ShowValueInInventory()
    {
        _inInventoryValue.text = _inventory.ShowIngredientValue(_ingredient).ToString();
    }

    public void HideSlot(bool flag)
    {
        Image[] images = GetComponentsInChildren<Image>();
        Color currentColor;

        if (flag)
        {
            currentColor = _hideColor;
            _buyButton.interactable = false;
        }
        else
        {
            currentColor = _activeColor;
            _buyButton.interactable = true;
        }

        foreach (var item in images)
        {
            item.color = currentColor;
        }

        _cost.color = currentColor;
    }
}

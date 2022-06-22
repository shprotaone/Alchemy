using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    [SerializeField] private TMP_Text _cost;
    [SerializeField] private TMP_Text _inInventoryValue;
    [SerializeField] private Image _slotImage;

    private Inventory _inventory;
    private ShopSystem _shopSystem;
    private IngredientData _ingredient;    
    private Button _buyButton;

    private void Start()
    {
        _shopSystem = GetComponentInParent<ShopSystem>();
        _inventory = _shopSystem.Inventory;
        _buyButton = GetComponentInChildren<Button>();
        _buyButton.onClick.AddListener(Buy);

        ShowValueInInventory();
    }

    public void FillSlot(IngredientData ingredientData)
    {
        _ingredient = ingredientData;
        _slotImage.sprite = ingredientData.mainSprite;        
        _cost.text = ingredientData.cost.ToString();                   
    }

    private void Buy()
    {
        _inventory.AddIngredient(_ingredient);
        _shopSystem.Transaction(_ingredient.cost);
        ShowValueInInventory();
    }

    private void ShowValueInInventory()
    {
        _inInventoryValue.text = _inventory.ShowIngredientValue(_ingredient).ToString();
    }
}

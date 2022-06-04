using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    private Inventory _inventory;
    private ShopSystem _shopSystem;
    private IngredientData _ingredient;
    private Image _slotImage;
    private TMP_Text _cost;
    private Button _buyButton;

    private void Start()
    {
        _shopSystem = GetComponentInParent<ShopSystem>();
        _inventory = _shopSystem.Inventory;
        _buyButton = GetComponentInChildren<Button>();
        _buyButton.onClick.AddListener(Buy);
    }

    public void FillSlot(IngredientData ingredientData)
    {
        _ingredient = ingredientData;
        _slotImage = GetComponentInChildren<Image>();
        _slotImage.sprite = ingredientData.mainSprite;

        _cost = GetComponentInChildren<TMP_Text>();
        _cost.text = ingredientData.cost.ToString();        
    }

    private void Buy()
    {
        _inventory.AddIngredient(_ingredient);
        _shopSystem.Transaction(_ingredient.cost);
    }
}

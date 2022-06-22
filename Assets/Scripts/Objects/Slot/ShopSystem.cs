using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopSystem : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Money _currentMoney;
    [SerializeField] private TMP_Text _moneyText;

    private ShopSlot[] _shopSlots;
    private IngredientData[] _ingredientData;

    public Inventory Inventory => _inventory;
    private void Start()
    {
        _shopSlots = GetComponentsInChildren<ShopSlot>();
        _ingredientData = _inventory.Ingredients;
        _moneyText.text = _currentMoney.CurrentMoney.ToString();

        FillShop();
    }

    public void Transaction(int value)
    {
        _currentMoney.Decrease(value);
        _moneyText.text = _currentMoney.CurrentMoney.ToString();
    }

    private void FillShop()
    {
        for (int i = 0; i < _shopSlots.Length; i++)
        {
            _shopSlots[i].FillSlot(_ingredientData[i]);
        }
    }
}

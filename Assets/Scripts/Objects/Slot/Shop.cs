using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System;

public class Shop : MonoBehaviour
{
    private const int MaxBottle = 4;
    private const int BottleCost = 500;
    private const int FuelCost = 150;
    private const int ClaudronUpgradeCost = 3000;

    [SerializeField] private BottleStorage _bottleStorage;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Firewood _firewoodSystem;
    [SerializeField] private ClaudronQualityManager _claudronQualityManager;

    [SerializeField] private Money _currentMoney;
    [SerializeField] private ShopShelf _commonShelf;
    [SerializeField] private ShopShelf _rareShelf;

    [SerializeField] private Button _addBottleButton;
    [SerializeField] private Button _addFuelButton;
    [SerializeField] private Button _upgradeClaudronButton;

    private IngredientData[] _ingredientData;

    public bool BlockRareSlots { get; set; }
    public Inventory Inventory => _inventory;

    private void Start()
    {
        UIController.OnShopSlotDisabled += HideForTutorial;

        _addBottleButton.onClick.AddListener(BuyBottle);
        _addFuelButton.onClick.AddListener(BuyFuel);
        _upgradeClaudronButton.onClick.AddListener(UpgradeClaudron);
    }

    private void InitShopSystem()
    {        
        _ingredientData = _inventory.Ingredients;
    }

    public bool Transaction(int value)
    {
        bool isCanBuy = _currentMoney.Decrease(value);

        if (isCanBuy) return true;
        else return false;        
    }

     public void BuyBottle()
    {
        if (_inventory.BottleCount < MaxBottle)
        {
            if (Transaction(BottleCost))
            {
                _bottleStorage.AddBottle(10);
            }
        }
        else
        {
            print("Bottle shelf is full");
        }        
    }

    public void BuyFuel()
    {
        if (Transaction(FuelCost))
        {
            _firewoodSystem.AddFireWood();
        }
    }

    public void UpgradeClaudron()
    {
        if (Transaction(ClaudronUpgradeCost))
        {
            _claudronQualityManager.UpgradeClaudron();
        }

    }

    public void FillShop()
    {
        InitShopSystem();

        IngredientData[] ingredientArray = new IngredientData[4];

        Array.Copy(_ingredientData, 0, ingredientArray, 0, 4);
        _commonShelf.FillSlots(ingredientArray);
       
        if (!BlockRareSlots)
        {
            Array.Copy(_ingredientData, 4, ingredientArray, 0, 4);
            _rareShelf.FillSlots(ingredientArray);
        }
    }

    public void HideForTutorial(bool flag)
    {
        if (flag)
        {
            _commonShelf.HideSlotToIndex(1, true);
            _commonShelf.HideSlotToIndex(3, true);
            _rareShelf.HideSlots(true);
        }
        else
        {
            _commonShelf.HideSlots(false);
        }

        HideAdditionalButton(true);
    }

    private void HideAdditionalButton(bool flag)
    {
        List<Button> buttons = new List<Button>();

        buttons.Add(_addBottleButton);
        buttons.Add(_addFuelButton);
        buttons.Add(_upgradeClaudronButton);

        foreach (var item in buttons)
        {
            item.interactable = !flag;
        }
    }

    private void OnDisable()
    {
        UIController.OnShopSlotDisabled -= HideForTutorial;
    }
}

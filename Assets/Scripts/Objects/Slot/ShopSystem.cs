using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    private const int MaxBottle = 4;
    private const int BottleCost = 500;
    private const int FuelCost = 150;
    private const int ClaudronUpgradeCost = 3000;

    [SerializeField] private Inventory _inventory;
    [SerializeField] private Firewood _firewoodSystem;
    [SerializeField] private ClaudronQualityManager _claudronQualityManager;

    [SerializeField] private Money _currentMoney;
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private ShopSlot[] _shopSlots;
    [SerializeField] private Button _backButton;

    private IngredientData[] _ingredientData;

    public Inventory Inventory => _inventory;

    private void Start()
    {
        TutorialSystem.OnShopSlotDisabled += TutorialMarkSlot;
    }

    private void InitShopSystem()
    {        
        _ingredientData = _inventory.Ingredients;
        RefreshMoneyCount();
    }

    public bool Transaction(int value)
    {
        if(_currentMoney.Decrease(value))
        {
            _moneyText.text = _currentMoney.CurrentMoney.ToString();
            return true;
        }
        else
        {
            Debug.LogWarning("NotHaveMoney");
            return false;
        }
        
    }

     public void BuyBottle()
    {
        if (_inventory.BottleCount < MaxBottle)
        {
            if (Transaction(BottleCost))
            {
                _inventory.AddBottle(1);
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

        for (int i = 0; i < _shopSlots.Length; i++)
        {
            _shopSlots[i].FillSlot(_ingredientData[i]);            
        }
    }

    public void TutorialMarkSlot(bool flag)
    {
        foreach (var item in _shopSlots)
        {
            item.HideSlot(flag);
        }

        if (flag)
        {
            _shopSlots[0].HideSlot(false);
            _shopSlots[2].HideSlot(false);
        }
    }

    public void RefreshMoneyCount()
    {
        _moneyText.text = _currentMoney.CurrentMoney.ToString();
    }

    public void NextStepTutorial()
    {
        _backButton.gameObject.GetComponent<NextCountHandler>().DisableClickHerePrefab();
    }

    private void OnDisable()
    {
        TutorialSystem.OnShopSlotDisabled -= TutorialMarkSlot;
    }
}

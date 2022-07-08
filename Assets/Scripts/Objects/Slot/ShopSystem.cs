using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
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
        _moneyText.text = _currentMoney.CurrentMoney.ToString();        
    }

    public void Transaction(int value)
    {
        _currentMoney.Decrease(value);
        _moneyText.text = _currentMoney.CurrentMoney.ToString();
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

    public void NextStepTutorial()
    {
        _backButton.gameObject.GetComponent<NextCountHandler>().DisableClickHerePrefab();
    }

    private void OnEnable()
    {
        TutorialSystem.OnShopSlotDisabled -= TutorialMarkSlot;
    }
}

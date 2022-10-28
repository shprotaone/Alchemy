using UnityEngine;
using TMPro;
using UnityEngine.Events;

/// <summary>
/// Отвечает за монеты
/// </summary>
public class Money : MonoBehaviour
{
    public static UnityEvent OnMoneyChanged = new UnityEvent();

    public static UnityEvent<int> OnDecreaseMoney = new UnityEvent<int>();
    public static UnityEvent<int> OnIncreaseMoney =  new UnityEvent<int>();

    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private TMP_Text _moneyTextInShop;

    private int _money;
    private int _moneyMinRange;
    public int CurrentMoney => _money;

    private void Start()
    {
        OnMoneyChanged.AddListener(RefreshMoneyText);
        OnDecreaseMoney.AddListener(CheckDecrease);
        OnIncreaseMoney.AddListener(Increase);
    }

    public void SetStartMoney(int money,int moneyMinRange)
    {
        _money = money;
        _moneyMinRange = moneyMinRange;
        OnMoneyChanged?.Invoke();
    }

    public void CheckDecrease(int value)    //Сомнительно
    {
        Decrease(value);
    }

    public bool Decrease(int value)
    {
        if (_moneyMinRange<value)
        {
            _money -= value;           
            OnMoneyChanged?.Invoke();
            return true;
        }
        else
        {
            Debug.LogWarning("NotHaveMoney");
            return false;
        }       
    }

    public void Increase(int value)
    {
        _money += value;
        OnMoneyChanged?.Invoke();
    }

    private void RefreshMoneyText()
    {
        _moneyText.text = _money.ToString();
        _moneyTextInShop.text = _money.ToString();
    }
}

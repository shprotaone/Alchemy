using System;
using UnityEngine;

/// <summary>
/// Отвечает за монеты
/// </summary>
public class Money
{
    public static event Action<int> OnChangeMoney;
    public static event Action OnChangeMoneyAction;

    private int _money;
    private int _moneyMinRange;
    public int CurrentMoney => _money;
    public bool CanBuy { get; private set; }

    public Money(MoneyView view,int startMoney, int moneyMinRange)
    {
        _money = startMoney;
        _moneyMinRange = moneyMinRange;
        view.Init();
        OnChangeMoney?.Invoke(_money);
    }

    public void Decrease(int value)
    {
        CanBuy = (_money - value) > _moneyMinRange;

        if (_moneyMinRange < value && CanBuy)
        {
            _money -= value;
            OnChangeMoney?.Invoke(_money);
            OnChangeMoneyAction?.Invoke();
        }
        else
        {
            _money = 0;
            OnChangeMoney?.Invoke(_money);
            Debug.LogWarning("NotHaveMoney");
        }       
    }

    public void Increase(int value)
    {
        _money += value;
        OnChangeMoney?.Invoke(_money);
        OnChangeMoneyAction?.Invoke();
    }

    internal void SetMoney(int moneyInPrevSession)
    {
        _money = moneyInPrevSession;
        OnChangeMoneyAction?.Invoke();
    }
}

using System;
using UnityEngine;

/// <summary>
/// Отвечает за монеты
/// </summary>
public class Money
{
    public static event Action<int> OnChangeMoney;
    public static event Action OnChangeMoneyAction;

    private MoneyView _view;
    private int _money;
    private int _moneyMinRange;
    private int _moneyTask;
    public int CurrentMoney => _money;
    public bool CanBuy { get; private set; }

    public Money(MoneyView view,int startMoney, int moneyTask, int moneyMinRange)
    {
        _money = startMoney;
        _moneyMinRange = moneyMinRange;
        _view = view;
        _moneyTask = moneyTask;

        _view.Init(startMoney, moneyTask);
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
        _view.Init(_money, _moneyTask);
        OnChangeMoneyAction?.Invoke();
        OnChangeMoney(_money);
    }
}

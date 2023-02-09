using System;
using UnityEngine;

/// <summary>
/// Отвечает за монеты
/// </summary>
public class Money
{
    public static event Action<int> OnChangeValueMoney;

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
        _view.RefreshMoneyView(_money);
    }

    public void Decrease(int value)
    {
        CanBuy = (_money - value) > _moneyMinRange;

        if (_moneyMinRange < value && CanBuy)
        {
            _money -= value;
            OnChangeValueMoney?.Invoke(-value);
        }
        else
        {
            _money = 0;
            OnChangeValueMoney?.Invoke(_money);
            Debug.LogWarning("NotHaveMoney");
        }       

        _view.RefreshMoneyView(_money);
    }

    public void Increase(int value)
    {
        _money += value;
        _view.RefreshMoneyView(_money);
        OnChangeValueMoney?.Invoke(value);
    }

    public void ResetMoneyValue(int moneyInPrevSession)
    {
        _money = moneyInPrevSession;
        _view.Init(_money, _moneyTask);
        _view.RefreshMoneyView(_money);
        //OnChangeValueMoney?.Invoke(_money);
    }
}

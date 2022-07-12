using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Money : MonoBehaviour
{
    public static Action OnMoneyChanged;

    [SerializeField] private TMP_Text _moneyText;
    private int _money;

    public int CurrentMoney => _money;

    private void Start()
    {
        OnMoneyChanged += RefreshMoneyText;
    }
    public void SetStartMoney(int money)
    {
        _money = money;
        Increase(_money);
    }

    public void Decrease(int value)
    {
        _money -= value;
        OnMoneyChanged?.Invoke();
    }

    public void Increase(int value)
    {
        _money += value;
        OnMoneyChanged?.Invoke();
    }

    private void RefreshMoneyText()
    {
        _moneyText.text = _money.ToString();
    }

    private void OnDisable()
    {
        OnMoneyChanged -= RefreshMoneyText;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Money : MonoBehaviour
{
    private const int startMoney = 500;

    [SerializeField] private TMP_Text _moneyText;
    private int _money;

    public int CurrentMoney => _money;
    private void Start()
    {
        _money = startMoney;
        RefreshMoneyText();
    }

    public void Decrease(int value)
    {
        _money -= value;
        RefreshMoneyText();
    }

    public void Increase(int value)
    {
        _money += value;
        RefreshMoneyText();
    }

    private void RefreshMoneyText()
    {
        _moneyText.text = _money.ToString();
    }
}

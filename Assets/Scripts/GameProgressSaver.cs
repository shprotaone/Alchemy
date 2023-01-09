using System;
using UnityEngine;

public class GameProgressSaver
{
    private int _moneyInPrevSession = 0;
    public int MoneyInPrevSession => _moneyInPrevSession;

    public void SetMoneyInSession(int value)
    {
        _moneyInPrevSession = value;
    }
}

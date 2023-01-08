using System;
using UnityEngine;

public class GameProgressSaver : MonoBehaviour
{
    
    private int _moneyInSession = 0;

    public void SetMoneyInSession(int value)
    {
        _moneyInSession += value;
    }
}

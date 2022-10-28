using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RentCalculator : MonoBehaviour
{
    [SerializeField] private Money _money;
    [SerializeField] private GameTimer _timer;

    private int _rentCost;
    private int _rentTime;

    public void InitRentSystem(int rentCost,int rentTime)
    {
        _rentCost = rentCost;
        _rentTime = rentTime;
        GameTimer.OnSecondChange += Rent;
    }

    public void Rent(float time)
    {
        if(time % _rentTime == 0)
        _money.Decrease(_rentCost);
    }

    private void OnDisable()
    {
        GameTimer.OnSecondChange -= Rent;
    }
}

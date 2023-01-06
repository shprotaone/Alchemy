using UnityEngine;

public class RentCalculator : MonoBehaviour
{
    [SerializeField] private Money _money;
    [SerializeField] private GameTimer _timer;

    private int _rentCost;
    private int _rentTime;

    public void InitRentSystem(int rentCost,int rentTime, bool rentActive)
    {
        if (rentActive)
        {
            _rentCost = rentCost;
            _rentTime = rentTime;
            _timer.LocalTimer.OnTimerUpdate += () => Rent(_timer.LocalTimer.CurrentTime);
        }      
    }

    public void Rent(int time)
    {
        if(time % _rentTime == 0)
        _money.Decrease(_rentCost);
    }

    private void OnDisable()
    {      
        //_timer.LocalTimer.OnTimerUpdate -= () => Rent(_timer.LocalTimer.CurrentTime);
    }
}

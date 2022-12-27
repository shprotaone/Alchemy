using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContrabandBottle : Bottle
{
    private Money _money;
    private LocalTimer _timer;
    private int _contrabandTime;

    private void StartTimer(Money money)
    {
        _money = money;
        _timer = new LocalTimer(_contrabandTime, true);
        StartCoroutine(_timer.StartTimer());

        _timer.OnTimerUpdate += UpdateTimer;
    }

    private void UpdateTimer()
    {
        //_timerText.text = _timer.CurrentTime.ToString();

        if (_timer.CurrentTime < 1)
        {
            _timer.StoppedTimer();
            _money.Decrease(3000);
            Destroy(this.gameObject);
        }
    }

    private void ResetTimer()
    {
        if (_timer != null)
        {
            StopCoroutine(_timer.StartTimer());
            _timer.OnTimerUpdate -= UpdateTimer;
        }
    }       
}

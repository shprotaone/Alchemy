using System;
using System.Collections;
using UnityEngine;

public class LocalTimer
{
    public event Action OnTimerUpdate;
    public event Action<bool> OnTimerEnded;

    private int _stockTime;
    private bool _stopped = false; // возможно лишн€€ проверка

    public int CurrentTime { get; private set; }
    public bool Started { get; private set; }
    public bool Finished { get; private set; }

    public LocalTimer(int stockTime, bool started)
    {
        _stockTime = stockTime;
        Started = started;
        CurrentTime = 0;
    }

    public IEnumerator StartTimer()
    {
        CurrentTime = _stockTime;
        Started = true;

        while (CurrentTime > 0)
        {
            if (!_stopped)
            {
                yield return new WaitForSeconds(1);
                CurrentTime--;
                OnTimerUpdate?.Invoke();               
            }
            else
            {
                Finished = true;                
                yield break;
            }
        }

        OnTimerEnded?.Invoke(true);
        Started = false;
    }

    public void StoppedTimer()
    {
        _stopped = true;
    }
}

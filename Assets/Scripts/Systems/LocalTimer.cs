using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalTimer
{
    public delegate void TimerUpdate();
    public event TimerUpdate OnTimerUpdate;

    private int _stockTime;
    private int _currentTime;

    private bool _started = false;
    private bool _stopped = false;
    private bool _finished = false;

    public int CurrentTime => _currentTime;

    public bool Started => _started;
    public bool Finished => _finished;

    public LocalTimer(int stockTime, bool started)
    {
        _stockTime = stockTime;
        _started = started;
        _currentTime = 0;
    }

    public IEnumerator Timer()
    {
        _currentTime = _stockTime;

        while (_currentTime > 0)
        {
            if (!_stopped)
            {
                yield return new WaitForSeconds(1);
                _currentTime--;
                OnTimerUpdate?.Invoke();               
            }
            else
            {
                _finished = true;                
                yield break;
            }
        }
    }

    public void StoppedTimer()
    {
        _stopped = true;
    }
}

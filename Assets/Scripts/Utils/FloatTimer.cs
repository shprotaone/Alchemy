using UnityEngine;

public class FloatTimer : MonoBehaviour
{
    [SerializeField] private float _timerRemaining;
    [SerializeField] private bool _timerIsRunning;

    public bool TimerIsRunning => _timerIsRunning;
    public void InitTimer(float delay)
    {
        _timerIsRunning = true;
        _timerRemaining = delay;
    }

    void Update()
    {
        if (_timerIsRunning)
        {
            if (_timerRemaining > 0)
            {
                _timerRemaining -= Time.deltaTime;
            }
            else
            {
                _timerRemaining = 0;
                _timerIsRunning = false;
            }
        }
    }
}

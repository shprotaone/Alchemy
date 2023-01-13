using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private GameTimeView _view;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private RentCalculator _rentShop; 
    [SerializeField] private Money _money;

    private LocalTimer _localTimer;

    public LocalTimer LocalTimer => _localTimer;

    /// <summary>
    /// Задает таймер
    /// </summary>
    /// <param name="seconds">общее время в секундах</param>
    /// <param name="flag">включать или нет?</param>
    public void InitTimer(int seconds, bool flag)
    {
        if (flag)
        {         
            _localTimer = new LocalTimer(seconds, flag);

            _localTimer.OnTimerUpdate += () => _view.UpdateTimeText(_localTimer.CurrentTime);

            StartCoroutine(_localTimer.StartTimer());
        }
        else
        {
            _view.gameObject.SetActive(false);
        }
    } 

    private void OnDisable()
    {
        if(_localTimer != null)
        StopCoroutine(_localTimer.StartTimer());
    }
}

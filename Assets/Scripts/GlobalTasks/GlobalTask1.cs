using UnityEngine;
using TMPro;
using System;

public class GlobalTask1 : MonoBehaviour
{
    public static Action OnLevelComplete;

    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Money _money;
    [SerializeField] private GuildSystem _guildSystem;
    [SerializeField] private TMP_Text _taskText;

    private int _taskValue;
    private int _minMoneyValue;
    private string _moneyTaskText = "Вам нужно набрать ";

    private void Start()
    {
        Money.OnMoneyChanged += CheckLevelComplete;
        Money.OnMoneyChanged += CheckLevelDefeat;
    }

    public void SetTaskValue(int value,int minValue)
    {
        _taskValue = value;
        _minMoneyValue = minValue;
        SetLevelTask();
    }

    public void DisableTask()
    {
        _taskText.text = "";
    }

    private void SetLevelTask()
    {
        if (_taskValue != 0)
        {
            _taskText.text = _moneyTaskText + _taskValue;
        }
    }

    private void CheckLevelComplete()
    {
        if (_taskValue <= _money.CurrentMoney)
        {
            _gameManager.CompleteLevel();
            OnLevelComplete?.Invoke();
        }
    }

    private void CheckLevelDefeat()
    {
        if(_minMoneyValue >= _money.CurrentMoney)
        {
            _gameManager.DefeatLevel();
        }
    }

    private void OnDisable()
    {
        Money.OnMoneyChanged -= CheckLevelComplete;
        Money.OnMoneyChanged -= CheckLevelDefeat;
    }
}

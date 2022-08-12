using UnityEngine;
using TMPro;
using System;

public class GlobalTaskController : MonoBehaviour
{
    public static Action OnLevelComplete;

    [SerializeField] private GlobalTaskViewer _globalTaskViewer;
    [SerializeField] private GameObject _endGamePanel;    
    [SerializeField] private Money _money;
    [SerializeField] private GuildSystem _guildSystem;
    [SerializeField] private TMP_Text _taskText;

    private int _taskValue;
    private string _moneyTaskText = "Вам нужно набрать ";

    private void Start()
    {
        Money.OnMoneyChanged += CheckLevelComplete;
    }

    public void SetTaskValue(int value)
    {
        _taskValue = value;
        SetLevelTask();
    }

    public void DisableTask()
    {
        _taskText.text = "";
    }

    public void CallStartGlobalTaskViewer(string text)
    {
        _globalTaskViewer.gameObject.SetActive(true);
        _globalTaskViewer.SetGlobalTaskText(text);
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
        if (_taskValue < _money.CurrentMoney)
        {
            _endGamePanel.SetActive(true);
            OnLevelComplete?.Invoke();
        }
    }

    private void OnDisable()
    {
        Money.OnMoneyChanged -= CheckLevelComplete;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GlobalTaskController : MonoBehaviour
{  
    [SerializeField] private LevelInitializator _levelInitializator;
    [SerializeField] private Money _money;
    [SerializeField] private GuildSystem _guildSystem;
    [SerializeField] private TMP_Text _taskText;

    private int _taskValue;
    private string _moneyTaskText = "Вам нужно набрать ";

    private void Start()
    {
        TutorialSystem.OnEndedTutorial += SetLevelTask;
        Money.OnMoneyChanged += CheckLevelComplete;
    }

    private void SetLevelTask(bool flag)
    {
        if (flag)
        {
            if (_levelInitializator.LevelTask.MoneyTask != 0)
            {
                _taskValue = _levelInitializator.LevelTask.MoneyTask;
                _taskText.text = _moneyTaskText + _taskValue;
            }
        }       
    }

    private void CheckLevelComplete()
    {
        if (_taskValue < _money.CurrentMoney)
        {
            print("Level Complete");
        }
    }

    private void OnDisable()
    {
        TutorialSystem.OnEndedTutorial -= SetLevelTask;
        Money.OnMoneyChanged -= CheckLevelComplete;
    }
}

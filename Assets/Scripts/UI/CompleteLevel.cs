using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CompleteLevel : MonoBehaviour,IMenu
{
    [SerializeField] private LevelInitializator _init;
    [SerializeField] private MenuPanelController _menuPanelController;
    [SerializeField] private GameProgress _gameProgress;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private TMP_Text _coinResult;

    private Money _money;
    private MoneyTask _moneyTask;


    private void Start()
    {
        _nextLevelButton?.onClick.AddListener(NextLevel);
        _restartButton?.onClick.AddListener(Restart);
    }

    public void Init(Money money,MoneyTask moneyTask)
    {
        _money = money;
        _moneyTask = moneyTask;
    }

    private void CheckResult()
    {
        _coinResult.text = _money.CurrentMoney.ToString();

        if(_moneyTask.TaskMoney <= _money.CurrentMoney)
        {
            _nextLevelButton.interactable = true;
        }
        else
        {
            _nextLevelButton.interactable = false;
        }

        _init.DisableLevel();
    }

    public void NextLevel()
    {
        _init.SetPreset(_gameProgress.GetNextLevel(),false);      
        //TODO затемнение? 
    }

    public void Restart()
    {
        _init.SetPreset(_gameProgress.CurrentLevel,true);
    }

    public void Disable()
    {       
        _menuPanelController.Close();
    }

    internal void Activated()
    {
        _menuPanelController.Open();
        CheckResult();
        
    }
}

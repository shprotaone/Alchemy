using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CompleteLevel : MonoBehaviour,IMenu
{
    [SerializeField] private LevelInitializator _init;
    [SerializeField] private MenuPanelController _menuPanelController;
    [SerializeField] private GameProgress _gameProgress;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private TMP_Text _coinResult;

    private Money _money;
    private MoneyTask _moneyTask;


    private void OnEnable()
    {
        _mainMenuButton?.onClick.AddListener(MainMenu);
        _restartButton.gameObject.SetActive(false);
        _nextLevelButton.gameObject.SetActive(false);
    }

    public void Init(Money money,MoneyTask moneyTask)
    {
        _money = money;
        _moneyTask = moneyTask;
    }

    private void CheckResult()
    {
        _coinResult.text = _money.CurrentMoney.ToString();

        if(_moneyTask.TaskMoney < _money.CurrentMoney)
        {
            _nextLevelButton.gameObject.SetActive(true);
            _nextLevelButton?.onClick.AddListener(NextLevel);
        }
        else
        {
            _restartButton.gameObject.SetActive(true);
            _restartButton?.onClick.AddListener(Restart);          
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
        _init.SetPreset(_gameProgress.LoadLevelFromIndex(0), false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
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

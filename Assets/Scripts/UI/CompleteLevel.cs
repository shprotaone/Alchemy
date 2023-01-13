using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CompleteLevel : MonoBehaviour,IMenu
{   
    private const string defeatText = "ƒневна€ цель не достигнута";

    [SerializeField] private LevelInitializator _init;
    [SerializeField] private CompleteLevelPanelController _controller;
    [SerializeField] private LevelSelector _gameProgress;
    [SerializeField] private GameObject _backGroundPanel;
    [SerializeField] private ParticleSystem _completeParticle;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private TMP_Text _coinResult;

    private Money _money;
    private MoneyTask _moneyTask;
    private string _resultText;

    private int _currentDay;

    private void Start()
    {
        _nextLevelButton?.onClick.AddListener(NextLevel);
        _restartButton?.onClick.AddListener(Restart);
        _mainMenuButton?.onClick.AddListener(MainMenuLoad);
    }

    public void Init(Money money,MoneyTask moneyTask)
    {
        _money = money;
        _moneyTask = moneyTask;
        _currentDay = (int)_gameProgress.CurrentLevel.levelNumber;       
    }

    private void CheckResult()
    {       
        _coinResult.text = _money.CurrentMoney.ToString();

        if(_moneyTask.TaskMoney < _money.CurrentMoney)
        {
            _nextLevelButton.gameObject.SetActive(true);
            _restartButton.gameObject.SetActive(false);
            
            _resultText = "ƒень " + _currentDay + " пройден";
            _currentDay++;
            _completeParticle.gameObject.SetActive(true);
            _completeParticle.Play();
        }
        else
        {
            _restartButton.gameObject.SetActive(true);
            _nextLevelButton.gameObject.SetActive(false);

            _currentDay = 1;
            _resultText = defeatText;
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
        _currentDay = (int)LevelNumber.Level1;
        _init.SetPreset(_gameProgress.LoadLevelFromIndex(LevelNumber.Level1), true);
    }

    public void MainMenuLoad()
    {
        SceneManager.LoadScene(MainMenu.mainMenuSceneName);
    }

    public void Disable()
    {
        _controller.Disable();
        _completeParticle.Stop();
        _backGroundPanel.SetActive(false);
    }

    public void Activated()
    {
        _backGroundPanel.SetActive(true);
        CheckResult();
        _controller.Enable(_currentDay);
        _controller.SetText(_resultText);
    }
}

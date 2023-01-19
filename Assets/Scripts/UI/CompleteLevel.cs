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
    [SerializeField] private GameObject _backGroundPanel;
    [SerializeField] private ParticleSystem _completeParticle;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private TMP_Text _coinResult;

    private Money _money;
    private MoneyTask _moneyTask;
    private LevelSelector _levelSelector;
    private string _resultText;

    private void Start()
    {
        _nextLevelButton?.onClick.AddListener(NextLevel);
        _restartButton?.onClick.AddListener(Restart);
        _mainMenuButton?.onClick.AddListener(MainMenuLoad);
    }

    public void Init(Money money,MoneyTask moneyTask,LevelSelector levelSelector)
    {
        _levelSelector = levelSelector;
        _money = money;
        _moneyTask = moneyTask;      
    }

    private void CheckResult()
    {       
        _coinResult.text = _money.CurrentMoney.ToString();

        if(_moneyTask.TaskMoney < _money.CurrentMoney)
        {
            _nextLevelButton.gameObject.SetActive(true);
            _restartButton.gameObject.SetActive(false);
            
            _resultText = "ƒень " + (int)_levelSelector.CurrentLevel.levelNumber + " пройден";
            _completeParticle.gameObject.SetActive(true);
            _completeParticle.Play();
        }
        else
        {
            _restartButton.gameObject.SetActive(true);
            _nextLevelButton.gameObject.SetActive(false);
            _resultText = defeatText;
        }

        _init.DisableLevel();
    }

    public void NextLevel()
    {
        _init.LoadNextLevel(_levelSelector.GetNextLevel());      
        //TODO затемнение? 
    }

    public void Restart()
    {
        _init.RestartGame();
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
        _controller.Enable();
        _controller.SetText(_resultText);
    }
}

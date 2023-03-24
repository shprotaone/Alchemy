using System;
using System.Collections;
using FMODUnity;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class CompleteLevel : MonoBehaviour,IMenu
{   
    private const string defeatText = "������� ���� �� ����������";

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
    private AudioManager _audioManager;
    private LevelSelector _levelSelector;
    private string _resultText;
    public bool IsLevelComplete { get; private set; }

    private void Start()
    {
        _nextLevelButton?.onClick.AddListener(NextLevel);
        _restartButton?.onClick.AddListener(Restart);
        _mainMenuButton?.onClick.AddListener(MainMenuLoad);
    }

    public void Init(Money money,MoneyTask moneyTask,LevelSelector levelSelector,AudioManager audioManager)
    {
        _audioManager = audioManager;
        _levelSelector = levelSelector;
        _money = money;
        _moneyTask = moneyTask;      
    }

    private void CheckResult()
    {       
        _coinResult.text = _money.CurrentMoney.ToString();

        if (_moneyTask.TaskMoney < _money.CurrentMoney)
        {
            _nextLevelButton.gameObject.SetActive(true);
            _restartButton.gameObject.SetActive(false);
            
            _resultText = "���� " + (int)_levelSelector.CurrentLevel.levelNumber + " �������";
            _completeParticle.gameObject.SetActive(true);

            _completeParticle.Play();
            _audioManager.PlaySFX(_audioManager.Data.WinWindowSound);

            StartCoroutine(ADDelay());
            IsLevelComplete = true;
        }
        else
        {
            _restartButton.gameObject.SetActive(true);
            _nextLevelButton.gameObject.SetActive(false);
            _resultText = defeatText;
            _audioManager.PlaySFX(_audioManager.Data.LoseWindowSound);
            IsLevelComplete = false;
        }

        _init.DisableLevel();
    }

    public void NextLevel()
    {
        _init.LoadNextLevel(_levelSelector.GetNextLevel());      
        //TODO ����������? 
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

    private IEnumerator ADDelay()
    {
        yield return new WaitForSeconds(1);
        YandexGame.FullscreenShow();
    }
}

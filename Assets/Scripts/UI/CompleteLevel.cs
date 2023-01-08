using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CompleteLevel : MonoBehaviour,IMenu
{
    [SerializeField] private LevelInitializator _init;
    [SerializeField] private GameProgress _gameProgress;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private TMP_Text _coinResult;

    private Money _money;
    private MoneyTask _moneyTask;
    public void Init(Money money,MoneyTask moneyTask)
    {
        _nextLevelButton?.onClick.AddListener(NextLevel);
        _restartButton?.onClick.AddListener(Restart);

        _money = money;
        _moneyTask = moneyTask;
    }

    private void OnEnable()
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
    }

    public void NextLevel()
    {
        _init.SetPreset(_gameProgress.GetNextLevel());      
        //TODO затемнение? 
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}

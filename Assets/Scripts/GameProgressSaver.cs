using System;
using UnityEngine;

public class GameProgressSaver
{
    public event Action OnSaveProgress;
    public readonly string firstPlayName = "FirstPlay";
    private int _moneyInPrevSession = 0;

    public int MoneyInPrevSession => _moneyInPrevSession;
    public int IsFirstGame { get; set; }
    public GameProgressSaver()
    {
        CheckFirstPlay();
    }

    private void CheckFirstPlay()
    {
        IsFirstGame = PlayerPrefs.GetInt(firstPlayName, 0) ;
    }

    public void SetMoneyInSession(int value)
    {
        _moneyInPrevSession = value;
    }

    public void SaveProgress()
    {
        PlayerPrefs.SetInt(firstPlayName, IsFirstGame);
        OnSaveProgress?.Invoke();
        PlayerPrefs.Save();
    }

    ~GameProgressSaver()
    {
        SaveProgress();
    }
}

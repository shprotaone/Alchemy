using System;
using UnityEngine;
using YG;

public class GameProgressSaver
{
    public event Action OnSaveProgress;

    public readonly string musicName = "isMusicOn";
    public readonly string sfxName = "Effects";
    public readonly string firstPlayName = "FirstPlay";
    public readonly string saveRecord = "Record";

    private bool _music = true;
    private bool _sfx = true;
    private bool _isFirstGame = true;
    private int _currentRecord;

    public bool IsFirstGame => _isFirstGame;
    public bool Music => _music;
    public bool SFX => _sfx;

    public GameProgressSaver()
    {
        CheckFirstPlay();
        LoadSettings();
        LoadRecord();
    }

    private void CheckFirstPlay()
    {
        _isFirstGame = GetFirstGame();
    }

    private void LoadSettings()
    {
        _music = Convert.ToBoolean(PlayerPrefs.GetInt(musicName,1));
        _sfx = Convert.ToBoolean(PlayerPrefs.GetInt(sfxName,1));
    }

    public void LoadRecord()
    {
        if(YandexGame.SDKEnabled == true)
        {
            _currentRecord = YandexGame.savesData.moneyRecord;
        }
        
        Debug.Log("Текущий рекорд " + _currentRecord);
    }

    public void SaveRecord(int money)
    {
        if (_currentRecord < money)
        {
            _currentRecord = money;

            if(YandexGame.SDKEnabled == true)
            {
                YandexGame.savesData.moneyRecord = _currentRecord;
                YandexGame.NewLeaderboardScores("CoinLeaderBoard", _currentRecord);
                YandexGame.SaveProgress();
                
            }       
        }
    }

    public void SaveProgress()
    {
        PlayerPrefs.SetInt(firstPlayName, IsFirstGame.GetHashCode());
        OnSaveProgress?.Invoke();
        PlayerPrefs.Save();
    }

    public void SaveSoundsSettings(bool music, bool sfx)
    {
        _music = music;
        _sfx = sfx;

        PlayerPrefs.SetInt(musicName, _music.GetHashCode());
        PlayerPrefs.SetInt(sfxName, _sfx.GetHashCode());
    }

    public void FirstGameComplete() 
    { 
        _isFirstGame = false;
        SaveProgress();
    }

    private bool GetFirstGame()
    {
        int result = PlayerPrefs.GetInt(firstPlayName, 1);

        if (result == 1) return true;
        else return false;
    }
}

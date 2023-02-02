using System;
using UnityEngine;

public class GameProgressSaver
{
    public event Action OnSaveProgress;

    public readonly string musicName = "Music";
    public readonly string sfxName = "Effects";
    public readonly string firstPlayName = "FirstPlay";

    private bool _music = true;
    private bool _sfx = true;
    private bool _isFirstGame = true;

    public bool IsFirstGame => _isFirstGame;
    public bool Music => _music;
    public bool SFX => _sfx;

    public GameProgressSaver()
    {
        CheckFirstPlay();
        ReadSaveSettings();
    }

    private void CheckFirstPlay()
    {
        _isFirstGame = GetFirstGame();
    }

    private void ReadSaveSettings()
    {
        if (_isFirstGame)
        {
            _music = true;
            _sfx = true;
            SaveSoundsSettings(_music, _sfx);
        }
        else
        {
            _music = Convert.ToBoolean(PlayerPrefs.GetInt(musicName));
            _sfx = Convert.ToBoolean(PlayerPrefs.GetInt(sfxName));
        }

        SaveProgress();
    }

    public void SaveRecord(int money)
    {
        int currentRecord = PlayerPrefs.GetInt(RecordLoader.RecordName, 0);

        if (currentRecord < money)
        {
            PlayerPrefs.SetInt(RecordLoader.RecordName, money);
            PlayerPrefs.Save();
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
        PlayerPrefs.SetInt(musicName, music.GetHashCode());
        PlayerPrefs.SetInt(sfxName, sfx.GetHashCode());
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

    internal void SaveAchievments()
    {
        
    }

    ~GameProgressSaver()
    {
        SaveProgress();
    }
}

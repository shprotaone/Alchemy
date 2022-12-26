using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{    
    private const string musicName = "Music";
    private const string sfxName = "Effects";
    
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private SettingsView _settingDisplay;
    [SerializeField] private GameProgress _gameProgress;

    private bool _music;
    private bool _sfx;

    public bool SFX { get { return _sfx; } }
    public bool Music { get { return _music; } }

    private void Start()
    {      
        if(_gameProgress.FirstPlay == 0)
        {
            _music = true;
            _sfx = true;
            SaveSettings();            
        }
        else
        {
            _music = Convert.ToBoolean(PlayerPrefs.GetInt(musicName));
            _sfx = Convert.ToBoolean(PlayerPrefs.GetInt(sfxName));                 
        }      

        LoadSettings();
        _settingDisplay.LoadCurrentSettings();
    }

    /// <summary>
    /// Загрузка текущих настроек
    /// </summary>
    /// <param name="music"></param>
    /// <param name="sounds"></param>
    public void LoadSettings()
    {
        SetSound(musicName, _music);
        SetSound(sfxName, _sfx);
    }

    private void SetSound(string soundLayer, bool value)
    {
        if (value)
        {
            _audioMixer.SetFloat(soundLayer, 0);
        }
        else
        {
            _audioMixer.SetFloat(soundLayer, -80);
        }
    }

    public void SwitchMusic()
    {
        if (_music)
        {
            _music = false;
        }
        else
        {
            _music = true;
        }

        SaveSettings();
        _settingDisplay.ChangeSpriteMusic();
        SetSound(musicName, _music);
    }

    public void SwitchSFX()
    {
        if (_sfx)
        {
            _sfx = false;
        }
        else
        {
            _sfx = true;
        }

        SaveSettings();
        _settingDisplay.ChangeSpriteSounds();
        SetSound(sfxName, _sfx);
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetInt(musicName, _music.GetHashCode());
        PlayerPrefs.SetInt(sfxName, _sfx.GetHashCode());
    }
}

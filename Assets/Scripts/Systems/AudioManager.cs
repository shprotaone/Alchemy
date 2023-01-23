using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public event Action OnSoundSettingsChanged;

    [SerializeField] private SoundsData _data;
    [SerializeField] private AudioSource _mainMusicSource;
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private SettingsView _settingDisplay;
    
    private GameProgressSaver _gameProgress;

    public bool Music { get; private set; }
    public bool SFX { get; private set;
    }
    public AudioSource MainMusicSoruce => _mainMusicSource;
    public SoundsData Data => _data;

    public void Init(GameProgressSaver gameProgress)
    {
        _gameProgress = gameProgress;
        Music = _gameProgress.Music;
        SFX = _gameProgress.SFX;

        _settingDisplay.Init();
        OnSoundSettingsChanged?.Invoke();

        LoadSettings();        
        _mainMusicSource?.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        _sfxSource.PlayOneShot(clip);
    }

    public void ChangeMainMusic(AudioClip clip)
    {
        _mainMusicSource.clip = clip;
        _mainMusicSource.Play();
    }

    public AudioClip GetRandomSound(AudioClip[] clips)
    {
        return clips[UnityEngine.Random.Range(0, clips.Length)];
    }  

    /// <summary>
    /// Загрузка текущих настроек
    /// </summary>
    /// <param name="music"></param>
    /// <param name="sounds"></param>
    public void LoadSettings()
    {
        SetSound(_gameProgress.musicName, Music);
        SetSound(_gameProgress.sfxName, SFX);
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
        if (Music)
        {
            Music = false;
        }
        else
        {
            Music = true;
        }

        _gameProgress.SaveSoundsSettings(Music, SFX);
        SetSound(_gameProgress.musicName, Music);
        OnSoundSettingsChanged?.Invoke();       
    }

    public void SwitchSFX()
    {
        if (SFX)
        {
            SFX = false;
        }
        else
        {
            SFX = true;
        }
        _gameProgress.SaveSoundsSettings(Music, SFX);
        SetSound(_gameProgress.sfxName, SFX);
        OnSoundSettingsChanged?.Invoke();      
    }
}

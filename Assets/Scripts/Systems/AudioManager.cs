using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{    
    private const string musicName = "Music";
    private const string sfxName = "Effects";

    [SerializeField] private SoundsData _data;
    [SerializeField] private AudioSource _mainMusicSource;
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private SettingsView _settingDisplay;
    
    private GameProgressSaver _gameProgress;

    private bool _music;
    private bool _sfx;

    public AudioSource MainMusicSoruce => _mainMusicSource;
    public SoundsData Data => _data; 
    public bool SFX { get { return _sfx; } }
    public bool Music { get { return _music; } }

    private void Start()
    {
        _gameProgress = new GameProgressSaver();
        _gameProgress.OnSaveProgress += SaveSettings;
        ReadSaveSettings();

        LoadSettings();
        _settingDisplay.LoadCurrentSettings();
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

    private void ReadSaveSettings()
    {
        if (_gameProgress.IsFirstGame == 0)
        {
            _music = true;
            _sfx = true;
            _gameProgress.IsFirstGame = 1;
            SaveSettings();
        }
        else
        {
            _music = Convert.ToBoolean(PlayerPrefs.GetInt(musicName));
            _sfx = Convert.ToBoolean(PlayerPrefs.GetInt(sfxName));
        }
    }

    /// <summary>
    /// �������� ������� ��������
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

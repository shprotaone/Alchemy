using DG.Tweening;
using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.Audio;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class AudioManager : MonoBehaviour
{
    public event Action OnSoundSettingsChanged;

    [SerializeField] private SoundsData _data;
    [SerializeField] private SettingsView _settingDisplay;
    
    private GameProgressSaver _gameProgress;
    private EventInstance _mainMusicInstance;
    private EventInstance _sfxInstance;

    public bool IsMusicOn { get; private set; }
    public bool IsSoundEffectOn { get; private set; }
    public SoundsData Data => _data;

    public void Init(GameProgressSaver gameProgress)
    {
        _gameProgress = gameProgress;
        IsMusicOn = _gameProgress.Music;
        IsSoundEffectOn = _gameProgress.SFX;
        OnSoundSettingsChanged?.Invoke();

        ChangeMainMusic(_data.ClaudronRoomTheme);
    }

    /// <summary>
    /// ѕроигрывание звуков из разных мест
    /// </summary>
    /// <param name="clip"></param>
    public void PlaySFX(EventReference clip)
    {
        if (IsSoundEffectOn)
        {
            RuntimeManager.PlayOneShot(clip);
        }
    }

    public void PlayInstanceSfx(EventReference clip)
    {
        if (IsSoundEffectOn)
        {
            _sfxInstance = RuntimeManager.CreateInstance(clip);
            _sfxInstance.start();
        }
    }

    public void StopInstanceSfx()
    {
        _sfxInstance.stop(STOP_MODE.IMMEDIATE);
    }

    /// <summary>
    /// »зменение музыки в зависимости от стадии игры
    /// </summary>
    /// <param name="clip"></param>
    public void ChangeMainMusic(EventReference clip)
    {
        _mainMusicInstance.stop(STOP_MODE.ALLOWFADEOUT);
        _mainMusicInstance = RuntimeManager.CreateInstance(clip);

        if (IsMusicOn)
        {
            _mainMusicInstance.start();
        }
    }

    public void PlaySteps(GuildsType guild)
    {
        EventReference temp;
        if (guild == GuildsType.Saint)
        {
            temp = _data.StepsSaint;
        }
        else if(guild == GuildsType.Bandit)
        {
            temp = _data.StepsBandit;
        }
        else if (guild == GuildsType.Knight)
        {
            temp = _data.StepsKnight;
        }
        else
        {
            temp = _data.StepsWizzard;
        }

        PlaySFX(temp);
    }

    public void PlaySleepSound(bool value)
    {
        PlaySFX(_data.VoiceWaiting);
    }

    public void SwitchMusic()
    {
        if (IsMusicOn)
        {
            IsMusicOn = false;
            _mainMusicInstance.stop(STOP_MODE.ALLOWFADEOUT);
        }
        else
        {
            IsMusicOn = true;
            _mainMusicInstance.start();
        }

        _gameProgress.SaveSoundsSettings(IsMusicOn, IsSoundEffectOn);
        OnSoundSettingsChanged?.Invoke();       
    }

    public void SwitchSFX()
    {
        if (IsSoundEffectOn)
        {
            IsSoundEffectOn = false;
        }
        else
        {
            IsSoundEffectOn = true;
        }

        _gameProgress.SaveSoundsSettings(IsMusicOn, IsSoundEffectOn);
        OnSoundSettingsChanged?.Invoke();      
    }

    private void OnDisable()
    {
        _mainMusicInstance.stop(STOP_MODE.IMMEDIATE);
    }
}

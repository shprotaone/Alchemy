using FMOD;
using FMOD.Studio;
using FMODUnity;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public event Action OnSoundSettingsChanged;

    [SerializeField] private SoundsData _data;
    [SerializeField] private SettingsView _settingDisplay;
    
    private GameProgressSaver _gameProgress;
    private EventInstance _mainMusicInstance;
    private EventInstance _sfxInstance;

    private EventReference _mainMusicReference;
    private EventReference _sfxReference;
    private ChannelGroup _channelGroup;

    public bool IsMusicOn { get; private set; }
    public bool IsSoundEffectOn { get; private set; }
    public SoundsData Data => _data;

    public void Init(GameProgressSaver gameProgress)
    {             
        _mainMusicInstance.getChannelGroup(out _channelGroup);

        ResumeSoundCore();
        EnableSounds();

        _gameProgress = gameProgress;
        IsMusicOn = _gameProgress.Music;
        IsSoundEffectOn = _gameProgress.SFX;      
        OnSoundSettingsChanged?.Invoke();
    }

    /// <summary>
    /// Проигрывание звуков из разных мест
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
            _sfxReference = clip;
            _sfxInstance = RuntimeManager.CreateInstance(_sfxReference);
            _sfxInstance.start();
        }
    }

    public void StopInstanceSfx()
    {
        _sfxInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    /// <summary>
    /// Изменение музыки в зависимости от стадии игры
    /// </summary>
    /// <param name="clip"></param>
    public void ChangeMainMusic(EventReference clip)
    {
        _mainMusicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        _mainMusicInstance.release();
        _mainMusicReference = clip;
        _mainMusicInstance = RuntimeManager.CreateInstance(_mainMusicReference);

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
            _mainMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
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

    public void DisableSounds()
    {
        RuntimeManager.MuteAllEvents(true);
        _mainMusicInstance.setPaused(true);
        _sfxInstance.setPaused(true);
    }

    public void EnableSounds()
    {
        RuntimeManager.MuteAllEvents(false);
        if (IsMusicOn) _mainMusicInstance.setPaused(false);
        if(IsSoundEffectOn) _sfxInstance.setPaused(false);
    }

    public void SuspendSoundCore()
    {
        var result = FMODUnity.RuntimeManager.CoreSystem.mixerSuspend();
        //RuntimeManager.MuteAllEvents(true);
        //Debug.Log("Не в фокусе");
    }

    public void ResumeSoundCore()
    {
        var result = FMODUnity.RuntimeManager.CoreSystem.mixerResume();
        //Debug.Log("В фокусе");
    }

    private void OnDisable()
    {
        DisableSounds();
        SuspendSoundCore();
        _mainMusicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}

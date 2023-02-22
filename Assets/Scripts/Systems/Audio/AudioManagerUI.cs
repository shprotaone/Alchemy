using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioManager))]
public class AudioManagerUI : MonoBehaviour
{
    [SerializeField] private AudioManager _audioManager;

    public void PlayOk()
    {
        _audioManager.PlaySFX(_audioManager.Data.OkSoundButton);
    }

    public void PlayButton()
    {
        _audioManager.PlaySFX(_audioManager.Data.SoundButton);
    }

    public void PlayCloseButton()
    {
        _audioManager.PlaySFX(_audioManager.Data.CloseButton);
    }
}

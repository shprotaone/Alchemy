using UnityEngine;
using UnityEngine.UI;

public class SettingsView : MonoBehaviour
{
    [SerializeField] private Button _musicButton;
    [SerializeField] private Button _soundsButton;

    [SerializeField] private Sprite _turnOnMusic;
    [SerializeField] private Sprite _turnOffMusic;

    [SerializeField] private Sprite _turnOnSfx;
    [SerializeField] private Sprite _turnOffSfx;

    [SerializeField] private Image _musicImage;
    [SerializeField] private Image _sfxImage;

    [SerializeField] private AudioManager _audioManager;

    private void Start()
    {
        _audioManager.OnSoundSettingsChanged += ChangeMusic;
        _audioManager.OnSoundSettingsChanged += ChangeSFX;

        _musicButton.onClick.AddListener(_audioManager.SwitchMusic);
        _soundsButton.onClick.AddListener(_audioManager.SwitchSFX);
    }
    private void OnEnable()
    {
        ChangeSFX();
        ChangeMusic();
    }

    public void ChangeMusic()
    {
        if (_audioManager.IsMusicOn) _musicImage.sprite = _turnOnMusic;
        else _musicImage.sprite = _turnOffMusic;
    }

    public void ChangeSFX()
    {
        if (_audioManager.IsSoundEffectOn) _sfxImage.sprite = _turnOnSfx;
        else _sfxImage.sprite = _turnOffSfx;
    }

    private void OnDestroy()
    {
        _audioManager.OnSoundSettingsChanged -= ChangeMusic;
        _audioManager.OnSoundSettingsChanged -= ChangeSFX;
    }
}

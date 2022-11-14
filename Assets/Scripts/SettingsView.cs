using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsView : MonoBehaviour
{
    [SerializeField] private Button _musicButton;
    [SerializeField] private Button _soundsButton;

    [SerializeField] private Sprite[] _turnOnMusic;
    [SerializeField] private Sprite[] _turnOffMusic;

    [SerializeField] private Sprite[] _turnOnSfx;
    [SerializeField] private Sprite[] _turnOffSfx;

    [SerializeField] private Image[] _currentImage;

    [SerializeField] private AudioManager _audioManager;

    private void Start()
    {
        _musicButton.onClick.AddListener(_audioManager.SwitchMusic);
        _soundsButton.onClick.AddListener(_audioManager.SwitchSFX);
    }

    public void LoadCurrentSettings()
    {
        Sprite[] switchSprite;

        _currentImage = _musicButton.GetComponentsInChildren<Image>();

        if (_audioManager.Music)
            switchSprite = _turnOnMusic;
        else switchSprite = _turnOffMusic;

        SwitchImage(_audioManager.Music, _currentImage, switchSprite);

        _currentImage = _soundsButton.GetComponentsInChildren<Image>();

        if (_audioManager.SFX)
            switchSprite = _turnOnSfx;
        else switchSprite = _turnOffSfx;

        SwitchImage(_audioManager.SFX, _currentImage, switchSprite);
    }

    public void ChangeSpriteMusic()
    {
        _currentImage = _musicButton.GetComponentsInChildren<Image>();
        Sprite[] switchSprite;

        if (_audioManager.Music)
        {
            switchSprite = _turnOnMusic;
            SwitchImage(_audioManager.Music, _currentImage, switchSprite);
        }
        else
        {
            switchSprite = _turnOffMusic;
            SwitchImage(_audioManager.Music, _currentImage, switchSprite);
        }
    }

    public void ChangeSpriteSounds()
    {
        _currentImage = _soundsButton.GetComponentsInChildren<Image>();

        Sprite[] switchSprite;
        if (_audioManager.SFX)
        {
            switchSprite = _turnOnSfx;
            SwitchImage(_audioManager.SFX, _currentImage, switchSprite);
        }
        else
        {
            switchSprite = _turnOffSfx;
            SwitchImage(_audioManager.SFX, _currentImage, switchSprite);
        }
    }

    private void SwitchImage(bool stats, Image[] currentImage, Sprite[] switchSprite)
    {
        if (stats)
        {
            for (int i = 0; i < _currentImage.Length; i++)
            {
                currentImage[i].sprite = switchSprite[i];
            }
        }
        else
        {
            for (int i = 0; i < _currentImage.Length; i++)
            {
                currentImage[i].sprite = switchSprite[i];
            }
        }
    }
}

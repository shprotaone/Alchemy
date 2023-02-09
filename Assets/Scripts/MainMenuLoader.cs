using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLoader : MonoBehaviour
{
    [SerializeField] private AudioManager _audioManager;
    private GameProgressSaver _gameProgress;

    void Start()
    {
        _gameProgress = new GameProgressSaver();
        _audioManager.Init(_gameProgress);
        _audioManager.ChangeMainMusic(_audioManager.Data.MainMenuMusic);
    }

}

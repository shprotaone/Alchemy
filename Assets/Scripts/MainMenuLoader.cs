using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLoader : MonoBehaviour
{
    [SerializeField] private AudioManager _audioManger;
    private GameProgressSaver _gameProgress;

    void Start()
    {
        _gameProgress = new GameProgressSaver();
        _audioManger.Init(_gameProgress);
    }
}

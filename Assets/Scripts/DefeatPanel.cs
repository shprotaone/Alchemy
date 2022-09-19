using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DefeatPanel : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;

    [SerializeField] private LevelInitializator _levelInitializator;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(Restart);
        _exitButton.onClick.AddListener(Exit);
    }

    private void Restart()
    {
        //_levelInitializator.SetRestartLevel(true);
        SceneManager.LoadScene(1);
    }

    private void Exit()
    {
        //_levelInitializator.SetRestartLevel(false);
        SceneManager.LoadScene(0);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(Restart);
        _exitButton.onClick.RemoveListener(Exit);
    }
}

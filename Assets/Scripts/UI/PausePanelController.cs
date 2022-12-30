using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePanelController : MonoBehaviour
{
    [SerializeField] private InGameTimeController _timeController;
    [SerializeField] private DraggableObjectController _draggableObjectController;
    [SerializeField] private SceneLoader _sceneLoader;

    [SerializeField] private Button _backButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _restartButton;

    private void Start()
    {
        _backButton.onClick.AddListener(Close);
        _exitButton.onClick.AddListener(Exit);
        _restartButton.onClick.AddListener(Restart);
    }

    private void Close()
    {
        _timeController.ResumeGame();
        _draggableObjectController.SetInterract(true);
        this.gameObject.SetActive(false);
    }

    private void Exit()
    {
        _sceneLoader.LoadLevel(SceneLoader.MenuScene);
    }

    private void Restart()
    {
        _sceneLoader.LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }
}

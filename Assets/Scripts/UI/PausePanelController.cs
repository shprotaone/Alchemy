using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePanelController : MonoBehaviour
{
    [SerializeField] private DraggableObjectController _draggableObjectController;
    [SerializeField] private LevelInitializator _init;

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
        _draggableObjectController.SetInterract(true);
        this.gameObject.SetActive(false);
    }

    private void Exit()
    {
        SceneManager.LoadScene(MainMenu.mainMenuSceneName);
    }

    private void Restart()
    {
        _init.RestartGame();
        Close();
    }
}

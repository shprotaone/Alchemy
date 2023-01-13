using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public static readonly string mainMenuSceneName = "Menu";
    public static readonly string GameSceneName = "DevelopSimpleVersion";

    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _guideButton;
    [SerializeField] private Button _settingsOpenButton;

    [SerializeField] private GameObject _guidePanel;
    [SerializeField] private GameObject _settingPanel;

    private void Start()
    {
        _startGameButton.onClick.AddListener(StartGame);
        _guideButton.onClick.AddListener(Guide);
        _settingsOpenButton.onClick.AddListener(SettingController);
    }

    private void StartGame()
    {
        SceneManager.LoadScene(GameSceneName);
    }

    private void Guide()
    {
        _guidePanel.gameObject.SetActive(true);
    }

    private void SettingController()
    {
        if (_settingPanel.activeInHierarchy)
        {
            _settingPanel.SetActive(false);
        }
        else
        {
            _settingPanel.SetActive(true);
        }
    }
}

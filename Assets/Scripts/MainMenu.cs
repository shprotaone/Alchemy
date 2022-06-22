using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _settingsOpenButton;

    [SerializeField] private GameObject _mapPanel;
    [SerializeField] private GameObject _settingPanel;

    private void Start()
    {
        _startGameButton.onClick.AddListener(MapController);
        _settingsOpenButton.onClick.AddListener(SettingController);
    }

    private void MapController()
    {
        if (_mapPanel.activeInHierarchy)
        {
            _mapPanel.SetActive(false);
        }
        else
        {
            _mapPanel.SetActive(true);
        }
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

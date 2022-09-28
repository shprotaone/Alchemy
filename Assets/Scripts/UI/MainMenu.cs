using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _settingsOpenButton;
    [SerializeField] private Button _closeButton;

    [SerializeField] private GameObject _mapPanel;
    [SerializeField] private GameObject _settingPanel;

    private void Start()
    {
        _startGameButton.onClick.AddListener(MapController);
        _closeButton.onClick.AddListener(MapController);
        _settingsOpenButton.onClick.AddListener(SettingController);

        _mapPanel.SetActive(LevelPresetLoader.instance.MapIsOpen);
    }

    private void MapController()
    {
        if (_mapPanel.activeInHierarchy)
        {
            _mapPanel.SetActive(false);
            LevelPresetLoader.instance.MapIsOpen = false;
        }
        else
        {
            _mapPanel.SetActive(true);
            LevelPresetLoader.instance.MapIsOpen = true;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InGameTimeController _gameTimeController;

    [SerializeField] private GameProgress _gameProgress;
    [SerializeField] private GameObject _completeLevelPanel;
    [SerializeField] private GameObject _defeatLevelPanel;

    private void Start()
    {
        _gameTimeController.ResumeGame();
    }

    public void CompleteLevel()
    {
        _completeLevelPanel.SetActive(true);
        _gameTimeController.PauseGame();
        //_gameProgress.SetReachedLevel(LevelPresetLoader.instance.LevelPreset.levelNumber);
    }

    public void DefeatLevel()
    {
        _defeatLevelPanel.SetActive(true);
        _gameTimeController.PauseGame();
    }

}

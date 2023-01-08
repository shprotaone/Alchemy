using System.Collections.Generic;
using UnityEngine;

public class GameProgress : MonoBehaviour
{
    [SerializeField] private GameProgressSaver _progressSaver;
    [SerializeField] private List<LevelPreset> _levels;

    private int _currentLevelIndex = 1;
    private int _firstPlay;
    public int FirstPlay => _firstPlay;

    private void Start()
    {
        if (_firstPlay == 0)
        {
            _firstPlay = 1;           //сохранение значения в гейм прогресс
        }
    }

    public LevelPreset GetNextLevel()
    {
        LevelPreset preset = _levels[_currentLevelIndex];
        _currentLevelIndex++;
        return preset;
    }
}

using System.Collections.Generic;
using UnityEngine;

public class GameProgress : MonoBehaviour
{
    [SerializeField] private List<LevelPreset> _levels;

    private GameProgressSaver _saver;
    private int _currentLevelIndex = 0;
    private int _firstPlay;

    public int FirstPlay => _firstPlay;
    public LevelPreset CurrentLevel { get; private set; }
    public GameProgressSaver Saver => _saver;

    public void Init()
    {
        if (_firstPlay == 0)
        {
            _firstPlay = 1;           //сохранение значения в гейм прогресс
        }

        _saver = new GameProgressSaver();
        CurrentLevel = _levels[0];
    }

    public void SaveCurrentLevelProgress(int moneyOnSession)
    {
        _saver.SetMoneyInSession(moneyOnSession);
    }

    public LevelPreset LoadLevelFromIndex(int index)
    {
        CurrentLevel = _levels[index];

        return CurrentLevel;
    }

    public LevelPreset GetNextLevel()
    {
        _currentLevelIndex++;
        CurrentLevel = _levels[_currentLevelIndex];
        LevelPreset preset = _levels[_currentLevelIndex];   
        return preset;
    }
}

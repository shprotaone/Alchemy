using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private List<LevelPreset> _levels;

    private int _currentLevelIndex = 0;
    private int _firstPlay;

    public LevelPreset CurrentLevel { get; private set; }

    public void Init()
    {
        if (_firstPlay == 0)
        {
            _firstPlay = 1;           //���������� �������� � ���� ��������
        }

        CurrentLevel = _levels[_currentLevelIndex];
    }

    public LevelPreset GetFirstLevel()
    {
        _currentLevelIndex = 0;
        CurrentLevel = _levels[_currentLevelIndex];       
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

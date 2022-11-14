using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgress : MonoBehaviour
{
    private const string firstPlay = "FirstPlay";
    private const string levelReachedPrefs = "levelUnlocked";
   
    [SerializeField] private List<LevelButton> _levels;

    private int _levelReached = 3;
    private int _firstPlayInt = 0;

    public int FirstPlay => _firstPlayInt;

    public int LevelReached => PlayerPrefs.GetInt(levelReachedPrefs);

    private void Awake()
    {
        _firstPlayInt = PlayerPrefs.GetInt(firstPlay);

        if(_firstPlayInt == 0)
        {
            _firstPlayInt = 1;
            
            PlayerPrefs.SetInt(firstPlay, _firstPlayInt);
            PlayerPrefs.SetInt(levelReachedPrefs, _levelReached);
        }
        else
        {
            _levelReached = PlayerPrefs.GetInt(levelReachedPrefs, _levelReached);
        }

        if(_levels.Count != 0)
        {
            CheckLevelUnlocked();
        }       
    }

    public void SetReachedLevel(LevelNumber levelNumber)
    {
        _levelReached = (int)levelNumber;
        PlayerPrefs.SetInt(levelReachedPrefs, _levelReached);
    }

    private void CheckLevelUnlocked()
    {
        for (int i = 0; i < _levelReached + 1; i++)
        {
            _levels[i].UnlockLevel();
        }
    }
}

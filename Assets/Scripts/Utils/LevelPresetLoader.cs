using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPresetLoader : MonoBehaviour
{
    public static LevelPresetLoader instance = null;

    [SerializeField] private LevelPreset _currentPreset;

    public LevelPreset LevelPreset => _currentPreset;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);
    }

    public void SetLevelPreset(LevelPreset currentPreset)
    {
        _currentPreset = currentPreset;
    }

    public void ResetPreset()
    {
        _currentPreset = null;
    }
}

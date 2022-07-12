using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPresetLoader : MonoBehaviour
{
    public static LevelPresetLoader instance;

    [SerializeField] private LevelPreset _levelPreset;

    public LevelPreset LevelPreset => _levelPreset;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void SetLevelPreset(LevelPreset currentPreset)
    {
        _levelPreset = currentPreset;
    }
}

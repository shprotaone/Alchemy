using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static readonly int MenuScene = 0;
    public static readonly int GameScene = 1;

    [SerializeField] private LevelPresetLoader _loader;
    private LevelPreset _levelPreset;

    private void Start()
    {
        _loader = FindObjectOfType<LevelPresetLoader>();
    }

    //лучше пользоваться не int, а названием, но не менять при этом название.
    public void LoadLevel(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void SetLevelPreset(LevelPreset currentPreset)
    {
        _levelPreset = currentPreset;

        if(_loader != null)
        _loader.SetLevelPreset(_levelPreset);

        LoadLevel(GameScene);
    }
}

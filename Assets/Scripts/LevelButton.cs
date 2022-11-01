using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private LevelPreset _levelPreset;
    [SerializeField] private SceneLoader _sceneLoader;

    private void Start()
    {
        _button.onClick.AddListener(() => _sceneLoader.SetLevelPreset(_levelPreset));
    }
}

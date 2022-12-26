using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private LevelPreset _levelPreset;
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private Transform _levelNumberText;
    [SerializeField] private Transform _lockSprite;

    public LevelPreset LevelPreset => _levelPreset;

    public void UnlockLevel()
    {
        _button.interactable = true;
        _button.onClick.AddListener(() => _sceneLoader.SetLevelPreset(_levelPreset));

        _levelNumberText.gameObject.SetActive(true);
        _lockSprite.gameObject.SetActive(false);
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClaudronSystem : MonoBehaviour     //это будущая View
{
    [SerializeField] private SpriteRenderer _claudronSprite;
    [SerializeField] private MixingSystem _mixingSystem;
    [SerializeField] private WaterColoring _waterColor;
    [SerializeField] private CookHandler _cookSystem;
    [SerializeField] private ButtonEventCatcher _buttonEvent;

    private bool _isTutorial;


    private void Start()
    {
        _mixingSystem.ActiveButtonBrewDelegate += ClaudronButtonState;

        LevelInitializator.OnLevelStarted += ClearClaudron;
    }

    public void ClaudronButtonState()
    {
        if (_mixingSystem.IngredientsInClaudron.Count >= 2 && !_isTutorial)
        {
            _buttonEvent.Button.interactable = true;
        }
        else
        {
            _buttonEvent.Button.interactable = false;
        }
    }

    public void ClearClaudron()
    {
        if (!_isTutorial)
        {
            _waterColor.ResetWaterColor();
            _cookSystem.FillBottleReset();
            _mixingSystem.ClearMixSystem();
        }
    }

    public void SetTutorial(bool value)
    {
        _isTutorial = value;
    }

    private void OnDestroy()
    {
        _mixingSystem.ActiveButtonBrewDelegate -= ClaudronButtonState;
        LevelInitializator.OnLevelStarted -= ClearClaudron;
    }
}

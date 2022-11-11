using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClaudronSystem : MonoBehaviour
{
    [SerializeField] private Button _clearClaudronButton;
    [SerializeField] private SpriteRenderer _claudronSprite;
    [SerializeField] private MixingSystemv2 _mixingSystem;
    [SerializeField] private WaterColorv2 _waterColor;
    [SerializeField] private Cookv2 _cookSystem;
    [SerializeField] private ButtonEventCatcher _buttonEvent;

    [SerializeField] private SpriteRenderer _crunchSprite;

    private Claudron _currentClaudron;
    
    private bool _isTutorial;

    public Button ClearClaudronButton => _clearClaudronButton;  
    public float CookSpeed => _currentClaudron.speedMul;

    private void Start()
    {
        _mixingSystem = GetComponent<MixingSystemv2>();

        _clearClaudronButton.onClick.AddListener(ClearClaudron);
        _mixingSystem.ActiveButtonBrewDelegate += ClaudronButtonState;
    }

    private void ClaudronButtonState()
    {
        if (_mixingSystem.Ingredients.Count > 0 && !_isTutorial)
        {
            _clearClaudronButton.interactable = true;
            _buttonEvent.Button.interactable = true;
        }
        else
        {
            _clearClaudronButton.interactable = false;
            _buttonEvent.Button.interactable = false;
        }
    }

    public void ClearClaudron()
    {
        if (!_isTutorial)
        {
            _mixingSystem.ClearMixSystem();
            _waterColor.SetColor(Color.white);
        }
    }

    public void SetClaudron(Claudron claudron)
    {
        _currentClaudron = claudron;
        _claudronSprite.sprite = claudron.image;
    }

    public void SetTutorial(bool value)
    {
        _isTutorial = value;
    }

    public void CrunchClaudron(bool value)
    {
        if (_crunchSprite.enabled == value)
            return;

        _crunchSprite.enabled = value;
    }

    private void OnDestroy()
    {
        _mixingSystem.ActiveButtonBrewDelegate -= ClaudronButtonState;
    }
}

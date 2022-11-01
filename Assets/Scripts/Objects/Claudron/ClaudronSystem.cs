using UnityEngine;
using UnityEngine.UI;

public class ClaudronSystem : MonoBehaviour
{
    [SerializeField] private Button _clearClaudronButton;
    [SerializeField] private SpriteRenderer _claudronSprite;

    private Claudron _currentClaudron;
    private MixingSystemv2 _mixingSystem;
    private bool _isTutorial;

    public Button ClearClaudronButton => _clearClaudronButton;
    
    public float CookSpeed => _currentClaudron.speedMul;

    private void Start()
    {
        _mixingSystem = GetComponent<MixingSystemv2>();

        _clearClaudronButton.onClick.AddListener(ClearClaudron);
        _mixingSystem.RefreshDelegate += ClaudronButtonState;
    }

    private void ClaudronButtonState()
    {
        if (_mixingSystem.Ingredients.Count > 0 && !_isTutorial)
        {
            _clearClaudronButton.interactable = true;
        }
        else
        {
            _clearClaudronButton.interactable = false;
        }
    }

    public void ClearClaudron()
    {
        _mixingSystem.ClearMixSystem();
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

    private void OnDestroy()
    {
        _mixingSystem.RefreshDelegate -= ClaudronButtonState;
    }
}

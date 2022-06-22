using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClaudronSystem : MonoBehaviour
{
    [SerializeField] private Button _clearClaudronButton;
    [SerializeField] private SpriteRenderer _claudronSprite;

    private Claudron _currentClaudron;
    private MixingSystemv2 _mixingSystem;
    
    public float CookSpeed => _currentClaudron.speedMul;

    private void Start()
    {
        _mixingSystem = GetComponent<MixingSystemv2>();
        _clearClaudronButton.onClick.AddListener(ClearClaudron);
        _mixingSystem._refreshDelegate += ClaudronButtonState;
    }


    private void ClaudronButtonState()
    {
        if (_mixingSystem.Ingredients.Count > 0)
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

    private void OnDisable()
    {
        _mixingSystem._refreshDelegate -= ClaudronButtonState;
    }
}

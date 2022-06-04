using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Claudron : MonoBehaviour
{
    [SerializeField] private Button _clearClaudronButton;
    
    private MixingSystemv2 _mixingSystem;
    
    private float _cookSpeed = 1;
    public float CookSpeed => _cookSpeed;

    private void Start()
    {
        _mixingSystem = GetComponent<MixingSystemv2>();

        _clearClaudronButton.onClick.AddListener(ClearClaudron);

        _mixingSystem._refreshDelegate += ClaudronButtonState;
    }

    public void ClearClaudron()
    {
        _mixingSystem.ClearMixSystem();
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

    private void OnDisable()
    {
        _mixingSystem._refreshDelegate -= ClaudronButtonState;
    }
}

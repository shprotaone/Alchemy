using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpPanelController : MonoBehaviour
{
    [SerializeField] private Transform _box;

    [SerializeField] private Button _windowMoveButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _enableButton;

    [SerializeField] private UIController _uiController;

    private void Start()
    {
        _exitButton.onClick.AddListener(() => _box.gameObject.SetActive(false));
        _exitButton.onClick.AddListener(() => _uiController.SetInterractAllButtons(true));

        _enableButton.onClick.AddListener(() => _box.gameObject.SetActive(true));
        _enableButton.onClick.AddListener(() => _uiController.SetInterractAllButtons(false));
        _enableButton.onClick.AddListener(MoveButtonEnable);
    }

    private void MoveButtonEnable()
    {
        _windowMoveButton.interactable = true;
    }
}

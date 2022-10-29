using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step1 : Step
{
    [SerializeField] private UIController _uiController;
    [SerializeField] private RectTransform _dialogPos;
    [SerializeField] private GameObject _panel;

    private void Start()
    {
        _dialogView.NextButton.onClick.AddListener(_tutorialManager.NextStep);
    }

    public override void StepAction()
    {
        _dialogView.MovementWindow(_dialogPos);
        _uiController.SetInterractAllButtons(false);

        _panel.SetActive(true);        
    }
}

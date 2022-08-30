using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step1 : Step
{
    [SerializeField] private UIController _uiController;
    [SerializeField] private RectTransform _dialogPos;

    public override void StepAction()
    {
        _dialogView.MovementWindow(_dialogPos);
        _uiController.SetInterractButtons(false);

        _blackBackground.enabled = true;
        _dialogView.NextButton.onClick.AddListener(_tutorialManager.NextStep);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Step11 : Step
{
    [SerializeField] private BrightObject _brightObject;
    [SerializeField] private VisitorController _visitorController;
    [SerializeField] private RectTransform _dialogWindowPos;
    [SerializeField] private Button _cameraMovementButton;

    public override void StepAction()
    {
        _visitorController.OnVisitorOut += _tutorialManager.NextStep;
        _brightObject.BrightVisitors(false);

        _dialogView.MovementWindow(_dialogWindowPos);
        _cameraMovementButton.interactable = false;
    }

    private void OnDisable()
    {
        _visitorController.OnVisitorOut -= _tutorialManager.NextStep;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step15 : Step
{
    [SerializeField] private UIController _UIController;
    public override void StepAction()
    {
        _dialogView.EnableView(false);
        _UIController.SetInterractAllButtons(true);
        _tutorialManager.gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step2 : Step
{
    [SerializeField] private BrightObject _brightObject;  

    public override void StepAction()
    {
        _tutorialManager.FirstVisitorInit();
        _tutorialManager.FirstVisitor.VisitorView.BrightVisitor(_brightObject.DialogLayerName);
    }
}

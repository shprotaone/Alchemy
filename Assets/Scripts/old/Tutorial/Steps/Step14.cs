using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step14 : Step
{
    [SerializeField] private Shop _shopSystem;

    public override void StepAction()
    {
        _shopSystem.HideForTutorial(false);
    }
}

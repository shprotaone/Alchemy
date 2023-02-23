using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepHandler : MonoBehaviour {

    private IStepTutorial _step;

    public void Init(IStepTutorial step) {
        _step = step;
    }
    public void Call() {
        _step?.Next();
        _step = null;
    }
}

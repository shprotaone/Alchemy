using System;
using UnityEngine;

public class Tutorial : MonoBehaviour {

    [SerializeField] private Canvas _tutorialCanvas;
    [SerializeField] private IStepTutorial[] _steps;

    private int _tutroialStep = 0;
    void Start() {
        //TODO: проверка на активацию туториала
        _steps = GetComponentsInChildren<IStepTutorial>();
        _steps[0].Activate(this);
    }

    public void CallNextStep() {

        _steps[_tutroialStep].Deactivate();

        _tutroialStep++;
        _steps[_tutroialStep].Activate(this);
    }
}

using System;
using UnityEngine;

public class Tutorial : MonoBehaviour {

    [SerializeField] private Canvas _tutorialCanvas;
    [SerializeField] private IStepTutorial[] _steps;

    private int _tutroialStep = 0;
    private bool isActive = false;
    public void Init(bool flag) {
        
        if (flag) {
            _tutorialCanvas?.gameObject.SetActive(true);
            _steps = GetComponentsInChildren<IStepTutorial>();
            _steps[0].Activate(this);
            isActive = true;
        }
        else
        {
            _tutorialCanvas?.gameObject.SetActive(false);
        }
    }

    public void CallNextStep() {

        if (isActive)
        {
            _steps[_tutroialStep].Deactivate();

            _tutroialStep++;
            _steps[_tutroialStep].Activate(this);
        }      
    }

    public void Disable()
    {
        isActive= false;
    }
}

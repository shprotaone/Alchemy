using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private Step[] _steps;
    [SerializeField] private VisitorController _visitorController;

    private Visitor _firstVisitor;
    private Step _prevStep;
    private Step _currentStep;
    private int _stepCounter;

    public Visitor FirstVisitor => _firstVisitor;

    public void Init()
    {              
        _stepCounter = 1;
    }

    public void FirstVisitorInit()
    {
        _firstVisitor = _visitorController.CurrentVisitor;
    }

    public void NextStep()
    {       
        _currentStep = _steps[_stepCounter - 1];

        if (_prevStep != null)
        {
            _prevStep.DisableStep();
        }

        if (_currentStep != null)
        {
            _currentStep.gameObject.SetActive(true);
            _currentStep.EnableStep();
        }

        _prevStep = _steps[_stepCounter - 1];

        if (_stepCounter < _steps.Length)
        {            
            _stepCounter++;
        }
       
        _steps[_stepCounter - 1].gameObject.SetActive(true);
    }

    public void SkipStep()
    {
        if (_prevStep != null)
        {
            _prevStep.DisableStep();
        }

        _prevStep = _steps[_stepCounter - 1];

        _stepCounter++;
    }
}

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

    private void Start()
    {
        _firstVisitor = _visitorController.CurrentVisitor;
    }
    public void Init()
    {
        _steps = GetComponentsInChildren<Step>();
        
        _stepCounter = 1;
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

        print(_currentStep);
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

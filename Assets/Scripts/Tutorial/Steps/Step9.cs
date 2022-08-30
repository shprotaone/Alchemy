using UnityEngine;

public class Step9 : Step
{
    [SerializeField] private MixingSystemv2 _mixingSystem;

    public override void StepAction()
    {
        _mixingSystem.FilledBottleDelegete += _tutorialManager.NextStep;
    }

    private void OnDisable()
    {
        _mixingSystem.FilledBottleDelegete -= _tutorialManager.NextStep;
    }
}

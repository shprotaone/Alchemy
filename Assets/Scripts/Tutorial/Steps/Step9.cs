using UnityEngine;

public class Step9 : Step
{
    [SerializeField] private MixingSystemv2 _mixingSystem;
    [SerializeField] private ClaudronSystem _claudronSystem;

    public override void StepAction()
    {
        _claudronSystem.SetTutorial(false);
        _mixingSystem.FilledBottleDelegateForTutorial += _tutorialManager.NextStep;
    }

    private void OnDisable()
    {
        _mixingSystem.FilledBottleDelegateForTutorial -= _tutorialManager.NextStep;
    }
}

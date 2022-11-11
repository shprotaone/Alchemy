using UnityEngine;

public class Step9 : Step
{
    [SerializeField] private ClickController _cookButtonController;
    [SerializeField] private Step8b _prevStep;
    [SerializeField] private MixingSystemv2 _mixingSystem;
    [SerializeField] private ClaudronSystem _claudronSystem;

    private float _standartDelayTime;
    public override void StepAction()
    {
        _standartDelayTime = _prevStep.StandartDelayTime;
        _dialogView.EnableView(false);
        _mixingSystem.FilledBottleDelegateForTutorial += _tutorialManager.NextStep;
        _cookButtonController.ChangeResetDelayTime(_standartDelayTime);
    }

    private void OnDisable()
    {
        _mixingSystem.FilledBottleDelegateForTutorial -= _tutorialManager.NextStep;
    }
}

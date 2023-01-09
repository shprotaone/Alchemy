using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step8b : Step
{
    [SerializeField] private ClickController _cookButtonController;
    [SerializeField] private MixingSystemv2 _mixingSystem;
    [SerializeField] private ClaudronSystem _claudonSystem;

    public float StandartDelayTime { get; set; }
    public override void StepAction()
    {
        //StandartDelayTime = _cookButtonController.ResetDelayTime;
        //_cookButtonController.ChangeResetDelayTime(100);
        _cookButtonController.OnGoodPotion += _tutorialManager.NextStep;
        _cookButtonController.OnBadPotion += FailCoocked;

        _dialogView.NextButton.onClick.RemoveAllListeners();
        _dialogView.NextButton.onClick.AddListener(ResetTry);
    }

    private void FailCoocked()
    {
        _dialogView.EnableView(true);
        _dialogView.DialogText.text = "Не получилось, поробуй еще раз";
    }

    private void ResetTry()
    {
        _dialogView.EnableView(false);
        //_cookButtonController.Reset();
    }

    private void OnDisable()
    {
        //_cookButtonController.OnGoodPotion -= _tutorialManager.NextStep;
        //_cookButtonController.OnBadPotion -= FailCoocked;
        _dialogView.NextButton.onClick.RemoveListener(ResetTry);
        _dialogView.NextButton.onClick.AddListener(_tutorialManager.NextStep);
        _claudonSystem.SetTutorial(false);

    }
}

using UnityEngine;
using UnityEngine.UI;

public class Step5 : Step
{
    [SerializeField] private Button _shopButton;
    [SerializeField] private GameObject _frame;
    [SerializeField] private BrightObject _brightObject;

    public override void StepAction()
    {
        _frame.SetActive(true);
        _blackBackground.enabled = false;

        _shopButton.interactable = true;
        _shopButton.onClick.AddListener(_tutorialManager.NextStep);

        _dialogView.EnableView(false);
    }

    public override void EndStepAction()
    {
        _shopButton.onClick.RemoveListener(_tutorialManager.NextStep);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class Step8 : Step
{
    [SerializeField] private Button _boilButton;
    [SerializeField] private ClaudronSystem _claudronSystem;

    public override void StepAction()
    {
        _claudronSystem.SetTutorial(true);
        _boilButton.onClick.AddListener(_tutorialManager.NextStep);
    }

    private void OnDisable()
    {
        _boilButton.onClick.RemoveListener(_tutorialManager.NextStep);
    }
}

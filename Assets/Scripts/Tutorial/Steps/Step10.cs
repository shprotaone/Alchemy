using UnityEngine;
using UnityEngine.UI;

public class Step10 : Step
{   
    [SerializeField] private Button _cameraMovementButton;

    public override void StepAction()
    {        
        _cameraMovementButton.onClick.AddListener(_tutorialManager.NextStep);
        _cameraMovementButton.interactable = true;
    }

    private void OnDisable()
    {
        _cameraMovementButton.onClick.RemoveListener(_tutorialManager.NextStep);       
    }
}

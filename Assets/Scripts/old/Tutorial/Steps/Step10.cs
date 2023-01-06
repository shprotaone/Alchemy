using UnityEngine;
using UnityEngine.UI;

public class Step10 : Step
{   
    [SerializeField] private Button _cameraMovementButton;
    [SerializeField] private RectTransform _frameSprite;

    public override void StepAction()
    {
        _dialogView.EnableView(true);

        _frameSprite.gameObject.SetActive(true);

        _dialogView.NextButton.gameObject.SetActive(false);
        _cameraMovementButton.onClick.AddListener(_tutorialManager.NextStep);
        _cameraMovementButton.interactable = true;
    }

    private void OnDisable()
    {
        _cameraMovementButton.onClick.RemoveListener(_tutorialManager.NextStep);       
    }
}

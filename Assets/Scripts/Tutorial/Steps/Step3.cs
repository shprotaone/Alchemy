using UnityEngine;
using UnityEngine.UI;

public class Step3 : Step
{
    [SerializeField] private RectTransform _dialogWindowPos;
    [SerializeField] private GameObject _frameSprite;
    [SerializeField] private Button _cameraMovementButton;
    public override void StepAction()
    {
        _dialogView.MovementWindow(_dialogWindowPos);

        _dialogView.NextButton.onClick.RemoveAllListeners();
        _dialogView.NextButton.gameObject.SetActive(false);

        _blackBackground.enabled = false;
        _frameSprite.SetActive(true);
        _cameraMovementButton.interactable = true;
    }
}

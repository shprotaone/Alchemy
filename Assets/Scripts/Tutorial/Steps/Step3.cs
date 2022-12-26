using UnityEngine;
using UnityEngine.UI;

public class Step3 : Step
{
    [SerializeField] private BrightObject _brightObject;
    [SerializeField] private RectTransform _dialogWindowPos;
    [SerializeField] private GameObject _frameSprite;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _cameraMovementButton;
    public override void StepAction()
    {
        _dialogView.MovementWindow(_dialogWindowPos);
        _panel.SetActive(false);

        _dialogView.NextButton.onClick.RemoveAllListeners();
        _dialogView.NextButton.gameObject.SetActive(false);

        _frameSprite.SetActive(true);
        _cameraMovementButton.interactable = true;

        _tutorialManager.FirstVisitor.VisitorView.BrightVisitor(_brightObject.UILayerName);
    }
}

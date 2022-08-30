using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Step4 : Step
{
    [SerializeField] private BrightObject _brightObject;
    [SerializeField] private UIController _uiController;
    [SerializeField] private RectTransform _dialogWindowPos;
    [SerializeField] private GameObject _frameSprite;
    [SerializeField] private Button _moveCameraButton;

    private void Start()
    {
        _moveCameraButton.onClick.AddListener(_tutorialManager.NextStep);
    }

    public override void StepAction()
    {
        _dialogView.MovementWindow(_dialogWindowPos);
        _dialogView.NextButton.gameObject.SetActive(true);
        _dialogView.NextButton.onClick.AddListener(_tutorialManager.NextStep);

        _uiController.ShopSlotController(true);
        _brightObject.BrightObjects(true);

        _frameSprite.SetActive(false);
        _blackBackground.enabled = true;

        _moveCameraButton.onClick.RemoveListener(_tutorialManager.NextStep);
        _moveCameraButton.interactable = false;
    }

}

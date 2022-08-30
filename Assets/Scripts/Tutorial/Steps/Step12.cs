using UnityEngine;
using UnityEngine.UI;

public class Step12 : Step
{
    [SerializeField] private Button _guildButton;
    [SerializeField] private Button _closeGuildButton;
    [SerializeField] private GameObject _frameSprite;

    public override void StepAction()
    {
        _guildButton.onClick.AddListener(DisableFrame);
        _guildButton.onClick.AddListener(() => _dialogView.EnableView(false));

        _closeGuildButton.onClick.AddListener(_tutorialManager.NextStep);

        _guildButton.interactable = true;
        _frameSprite.SetActive(true);
    }

    private void DisableFrame()
    {
        _frameSprite?.SetActive(false);

        _guildButton.onClick.AddListener(_tutorialManager.NextStep);
        _guildButton.interactable = false;
        _guildButton.onClick.RemoveListener(DisableFrame);
    }

    private void OnDisable()
    {
        _closeGuildButton.onClick.RemoveListener(_tutorialManager.NextStep);      
    }
}

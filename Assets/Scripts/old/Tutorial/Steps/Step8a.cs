using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Step8a : Step
{
    [SerializeField] private Button _boilButton;   
    [SerializeField] private RectTransform _dialogPos;

    public override void StepAction()
    {
        _dialogView.MovementWindow(_dialogPos);
        _dialogView.NextButton.gameObject.SetActive(true);
        _dialogView.NextButton.onClick.AddListener(ButtonAction);
    }

    private void ButtonAction()
    {
        _boilButton.interactable = true;
        _dialogView.EnableView(false);
    }

    private void OnDisable()
    {
        _dialogView.NextButton.onClick.RemoveListener(ButtonAction);
    }
}

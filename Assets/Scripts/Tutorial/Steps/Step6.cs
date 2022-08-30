using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Step6 : Step
{
    [SerializeField] private GameObject _shopFrame;
    [SerializeField] private GameObject _slotFrame;
    [SerializeField] private Button _shopButton;
    [SerializeField] private BrightObject _brightObject;

    public override void StepAction()
    {
        _shopFrame.SetActive(false);
        _brightObject.BrightObjects(false);
        _slotFrame.SetActive(true);
        _shopButton.interactable = false;
    }
}


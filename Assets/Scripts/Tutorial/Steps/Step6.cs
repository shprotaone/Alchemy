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
        StartCoroutine(FrameDelay());
        _shopButton.interactable = false;
    }

    private IEnumerator FrameDelay()    //сомнительное решение
    {
        yield return new WaitForSeconds(0.9f);
        _slotFrame.SetActive(true);

        yield break;
    }
}


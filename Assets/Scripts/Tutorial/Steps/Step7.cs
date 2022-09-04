using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Step7 : Step
{
    [SerializeField] private GameObject _frameFirst;
    [SerializeField] private GameObject _frameSecond;

    [SerializeField] private Button _firstIngredientButton;
    [SerializeField] private Button _secondIngredientButton;
    [SerializeField] private Button _exitButton;

    private void OnEnable()
    {
        _exitButton.gameObject.SetActive(false);

        _firstIngredientButton.onClick.AddListener(() => DisableFrame(0));
        _firstIngredientButton.onClick.AddListener(StepAction);

        _secondIngredientButton.onClick.AddListener(() => DisableFrame(1));
        _secondIngredientButton.onClick.AddListener(StepAction);

        _exitButton.onClick.AddListener(_tutorialManager.NextStep);
    }

    public override void StepAction()
    {
       if(!_frameFirst.activeInHierarchy && !_frameSecond.activeInHierarchy)
        {
            _exitButton.gameObject.SetActive(true);
            _dialogView.EnableView(true);
            _dialogView.DialogText.text = _text;
            _dialogView.NextButton.gameObject.SetActive(false);

            _tutorialManager.SkipStep();
            DragController.instance.ObjectsInterractable(true);
        }
    }

    private void DisableFrame(int index)
    {
        if(index == 0)
        {
            _frameFirst.SetActive(false);
        }
        else
        {
            _frameSecond.SetActive(false);
        }
    }

    public override void EndStepAction()
    {
        _firstIngredientButton.onClick.RemoveListener(() => DisableFrame(0));
        _firstIngredientButton.onClick.RemoveListener(StepAction);

        _secondIngredientButton.onClick.RemoveListener(() => DisableFrame(1));
        _secondIngredientButton.onClick.RemoveListener(StepAction);

        _exitButton.onClick.RemoveListener(_tutorialManager.NextStep);
    }

}

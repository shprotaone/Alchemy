using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartDialogViewer : MonoBehaviour
{
    [SerializeField] private RectTransform _box;
    [SerializeField] private InGameTimeController _gameTimeController;
    [SerializeField] private DraggableObjectController _draggableController;
    [SerializeField] private Button _nextButton;
    [SerializeField] private TMP_Text _buttonText;
    [SerializeField] private TMP_Text _mainText;

    private string[] _dialogArray;
    private int _dialogIndex = 0;

    public void InitDialog(string[] textArray)
    {
        _box.gameObject.SetActive(true);
        _dialogArray = new string[textArray.Length];
        _dialogArray = textArray;

        _mainText.text = textArray[_dialogIndex];

        _nextButton.onClick.AddListener(NextText);
        ButtonNaming();

        _gameTimeController.PauseGame();
        _draggableController.SetInterract(false);
    }

    private void NextText()
    {
        _dialogIndex++;
        _mainText.text = _dialogArray[_dialogIndex];
        ButtonNaming();
    }

    private void ButtonNaming()
    {
        if(_dialogIndex >= _dialogArray.Length-1)
        {
            _buttonText.text = "Закрыть";
            _nextButton.onClick.RemoveListener(NextText);

            _nextButton.onClick.AddListener(DisableViewer);
        }
    }

    private void DisableViewer()
    {
        _gameTimeController.ResumeGame();
        _draggableController.SetInterract(true);
        _box.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _nextButton.onClick.RemoveListener(DisableViewer);
    }
}

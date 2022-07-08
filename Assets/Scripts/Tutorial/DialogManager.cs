using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class DialogManager : MonoBehaviour
{
    private const string closeText = "Закрыть";

    [SerializeField] private EventCounter _eventCounter;
    [SerializeField] private GameObject _dialogPanel;
    [SerializeField] private RectTransform _dialogBox;
    [SerializeField] private RectTransform _firstDialogWindowPos;
    [SerializeField] private RectTransform _secondDialogWindowPos;
    [SerializeField] private InGameTimeController _pause;
    [SerializeField] private TMP_Text _dialogText;
    [SerializeField] private Button _nextButton;

    private Color _standartPanelColor;

    private string[] _currentDialog;
    private int _dialogCount;

    private void Start()
    {
        _dialogPanel.SetActive(false);        
    }

    public void SetDialogArray(string[] texts)
    {
        _currentDialog = texts;
    }

    public void StartDialogSystem()
    {
        _standartPanelColor = _dialogPanel.GetComponent<Image>().color;
        _dialogPanel.SetActive(true);
        _dialogText.text = _currentDialog[_dialogCount];
        RefreshText();

        _pause.PauseGame();
    }

    public void NextDialog()
    {
        _dialogCount++;

        RefreshText();
        SetCloseTextInButton();
    }

    private void RefreshText()
    {
        if(_dialogCount < _currentDialog.Length)
        _dialogText.text = _currentDialog[_dialogCount];
    }

    /// <summary>
    /// Управление текстом кнопки
    /// </summary>
    private void SetCloseTextInButton()
    {
        if(_dialogCount == _currentDialog.Length-1)
        {
            _nextButton.GetComponentInChildren<TMP_Text>().text = closeText;           //куда то вынести надо  
        }
    }
    /// <summary>
    /// Позиция плашки окна с диалогом
    /// </summary>
    /// <param name="pos">1 - нижняя позиция,
    /// 2 - верхняя позиция</param>
    public void PlateMovement(int pos)
    {
        _pause.ResumeGame();
        if (pos == 1)
            _dialogBox.DOAnchorPos(_secondDialogWindowPos.anchoredPosition, 1, false);
        else if (pos == 2)
            _dialogBox.DOAnchorPos(_firstDialogWindowPos.anchoredPosition, 1, false);
    }

    public void PanelIsActive(bool flag)
    {
        Image panel = _dialogPanel.GetComponent<Image>();
        if (flag)
        {
            panel.color = _standartPanelColor;           
        }
        else
        {
            panel.color = new Color(0, 0, 0, 0);
        }        
    }

    public void DialogIsActive(bool flag)
    {
        _dialogPanel.SetActive(flag);
    }

    public void ButtonIsActive(bool flag)
    {
        _nextButton.gameObject.SetActive(flag);
    }
}

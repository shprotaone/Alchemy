using UnityEngine.UI;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DialogView : MonoBehaviour
{
    [SerializeField] private RectTransform _dialogWindow;
    [SerializeField] private Image _dialogBackground;
    [SerializeField] private Button _nextButton;
    [SerializeField] private TMP_Text _dialogText;

    public RectTransform DialogWindow => _dialogWindow;
    public Button NextButton => _nextButton;
    public TMP_Text DialogText => _dialogText;

    /// <summary>
    /// Перемещение окна в туториале
    /// </summary>
    /// <param name="pos"></param>
    public void MovementWindow(RectTransform pos)
    {
        _dialogWindow.DOAnchorPos(pos.anchoredPosition, 1, true);
    }

    public void EnableView(bool value)
    {
        _nextButton.gameObject.SetActive(value);
        _dialogText.enabled = value;
        _dialogBackground.enabled = value;
    }
}

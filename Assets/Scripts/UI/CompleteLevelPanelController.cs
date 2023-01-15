using DG.Tweening;
using TMPro;
using UnityEngine;

public class CompleteLevelPanelController : MonoBehaviour
{   
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Transform _resultCoinGroup;
    [SerializeField] private TMP_Text _titleText;

    public void Disable()
    {
        _canvasGroup.DOFade(0, 1).OnComplete(() =>
        {
            _canvasGroup.gameObject.SetActive(false);
            _canvasGroup.alpha = 1;          
        });
    }

    public void Enable()
    {
        _canvasGroup.gameObject.SetActive(true);
        _titleText.transform.DOScale(1.5f, 1.5f).SetLoops(-1, LoopType.Yoyo);
    }

    public void SetText(string resultText)
    {
        _titleText.text = resultText;
    }
}

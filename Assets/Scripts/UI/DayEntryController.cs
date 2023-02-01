using DG.Tweening;
using TMPro;
using UnityEngine;

public class DayEntryController : MonoBehaviour
{
    [SerializeField] private DayNotifySubject _dayNotify;
    [SerializeField] private DraggableObjectController _draggableObjectController;
    [SerializeField] private TMP_Text _dayTitle;
    [SerializeField] private TMP_Text _dayText;
    [SerializeField] private CanvasGroup _group;

    public Tween CallNextDay(int dayCount)
    {
        _group.gameObject.SetActive(true);

        _group.alpha = 1;
        _dayText.alpha = 0;
        _dayTitle.alpha = 0;

        _dayText.text = dayCount.ToString();       
        _dayTitle.DOFade(1, 2);
        return _dayText.DOFade(1, 2).OnComplete(DisablePanel);
    }

    public void DisablePanel()
    {
        _draggableObjectController.SetInterract(true);
        _group.DOFade(0, 2).OnComplete(()=> _group.gameObject.SetActive(false));
        _dayNotify.CallNotify();
    }
}

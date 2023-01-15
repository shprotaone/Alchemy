using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class DayEntryController : MonoBehaviour
{
    [SerializeField] private TMP_Text _dayTitle;
    [SerializeField] private TMP_Text _dayText;
    [SerializeField] private CanvasGroup _group;

    public void CallNextDay(int dayCount)
    {
        _group.gameObject.SetActive(true);

        _group.alpha = 1;
        _dayText.alpha = 0;
        _dayTitle.alpha = 0;

        _dayText.text = dayCount.ToString();
        _dayText.DOFade(1, 2).OnComplete(DisablePanel);
        _dayTitle.DOFade(1, 2);
    }

    public void DisablePanel()
    {
        _group.DOFade(0, 2).OnComplete(()=> _group.gameObject.SetActive(false));
    }
}

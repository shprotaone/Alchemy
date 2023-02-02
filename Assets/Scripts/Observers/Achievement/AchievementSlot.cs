using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementSlot : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private Image _completeImage;
    [SerializeField] private Slider _progressSlider;
    [SerializeField] private AchievementData _data;

    private void Start()
    {
        _name.text = _data.Name;

        InitSlider();
        _completeImage.gameObject.SetActive(_data.complete);
    }

    private void OnEnable()
    {
        Refresh();
    }

    private void InitSlider()
    {
        if (_data.goal != 0)
        {
            _progressSlider.maxValue = _data.goal;
            _progressSlider.value = _data.goalProgress;
        }
        else
        {
            _progressSlider.gameObject.SetActive(false);
        }
    }

    private void Refresh()
    {
        if (_data.complete)
        {
            _completeImage.gameObject.SetActive(true);
        }

        if(_data.goal != 0)
        {
            _progressSlider.value = _data.goalProgress;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementSlot : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private Text _valueText;
    [SerializeField] private Image _completeImage;
    [SerializeField] private Slider _progressSlider;
    [SerializeField] private AchievementData _data;

    private void Start()
    {
        _name.text = _data.Name;
        _description.text = _data.Desription;

        InitSlider();
        _completeImage.gameObject.SetActive(_data.Complete);
    }

    private void OnEnable()
    {
        Refresh();
    }

    private void InitSlider()
    {
        if (_data.Goal != 0)
        {
            _progressSlider.maxValue = _data.Goal;
            _progressSlider.value = _data.GoalProgress;

            _valueText.text = $"{_data.GoalProgress}/{_data.Goal}";
        }
        else
        {
            _progressSlider.gameObject.SetActive(false);
        }
    }

    private void Refresh()
    {
        if (_data.Complete)
        {
            _completeImage.gameObject.SetActive(true);
        }

        if(_data.Goal != 0)
        {
            _progressSlider.value = _data.GoalProgress;
            _valueText.text = $"{_data.GoalProgress}/{_data.Goal}";
        }
    }
}

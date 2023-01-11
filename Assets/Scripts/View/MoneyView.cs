using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private TMP_Text _moneyTaskText;
    [SerializeField] private Slider _taskSlider;

    private float _minValueSlider;
    private float _maxValueSlider;

    public void Init(int min,int max)
    {
        Money.OnChangeMoney += RefreshMoneyText;
        Money.OnChangeMoney += RefreshSliderValue;

        InitSlider(min, max);
    }

    public void InitSlider(float min,float max)
    {
        _taskSlider.minValue = min;
        _taskSlider.maxValue = max;
    }

    private void RefreshSliderValue(int value)
    {
        _taskSlider.DOValue(value, 0.5f);
    }
    public void RefreshMoneyText(int value)
    {
        _moneyText.text = value.ToString();
        RefreshSliderValue(value);
    }

    public void RefreshMoneyTaskText(int reward)
    {
        _moneyTaskText.text = reward.ToString();
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private TMP_Text _moneyTaskText;
    [SerializeField] private Slider _taskSlider;

    [SerializeField] private ParticleSystem _particleCompleteTask;
    [SerializeField] private Image _fillImage;
    [SerializeField] private Color _standartColorSlider;
    [SerializeField] private Color _winColorSlider;

    private bool _isComplete;
    private float _minValueSlider;
    private float _maxValueSlider;

    public void Init(int min,int max)
    {
        Money.OnChangeMoney += RefreshMoneyText;
        Money.OnChangeMoney += RefreshSliderValue;

        _isComplete = false;
        
        InitSlider(min, max);
        _fillImage.color = _standartColorSlider;
    }

    public void InitSlider(float min,float max)
    {
        _taskSlider.minValue = min;
        _taskSlider.maxValue = max;
        _particleCompleteTask.Stop();
    }

    private void RefreshSliderValue(int value)
    {
        _taskSlider.DOValue(value, 0.5f);

        if(value > _taskSlider.maxValue && !_isComplete)
        {
            _isComplete = true;
            CompleteTaskState();
        }
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

    private void CompleteTaskState()
    {
        _fillImage.color = _winColorSlider;
        _particleCompleteTask.Play();
    }
}

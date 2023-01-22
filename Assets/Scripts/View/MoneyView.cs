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
    private int _prevMoneyValue;

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
        _fillImage.color = _standartColorSlider;
        _isComplete = false;
        //_particleCompleteTask.Stop();
    }

    private void RefreshSliderValue(int value)
    {
        _taskSlider.DOValue(value, 0.5f);

        if(value > _taskSlider.maxValue)
        {
            _isComplete = true;
        }
        else
        {
            _isComplete = false;
        }

        CheckTaskState();
    }
    public void RefreshMoneyText(int value)
    {
        DOTween.To(() => _prevMoneyValue, x => _prevMoneyValue = x, value, 0.3f)
            .OnUpdate(() => _moneyText.text = _prevMoneyValue.ToString()).OnComplete(() => _prevMoneyValue = value);

        RefreshSliderValue(value);
    }

    public void RefreshMoneyTaskText(int reward)
    {
        _moneyTaskText.text = reward.ToString();
    }

    private void CheckTaskState()
    {
        if (_isComplete)
        {
            _fillImage.color = _winColorSlider;
        }
        else
        {
            _fillImage.color = _standartColorSlider;
        }
    }
}

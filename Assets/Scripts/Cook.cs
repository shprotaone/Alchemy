using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Cook : MonoBehaviour
{
    [SerializeField] private Button _cookButton;
    [SerializeField] private Slider _boilProgress;
    [SerializeField] private Settings _settings;

    private MixingSystemv2 _mixingSystem;        
    private Claudron _claudron;
    
    private bool _isRarePotion;

    private float _speed = 0;
    private bool _canFillBottle = false;

    public bool CanFillBottle => _canFillBottle;

    private void Start()
    {
        _mixingSystem = GetComponent<MixingSystemv2>();
        _claudron = GetComponent<Claudron>();

        _mixingSystem._refreshDelegate += RefreshBar;
        _cookButton.onClick.AddListener(Brew);
    }
    public void Brew()
    {
        _speed = 1;
        StartCoroutine(ProgressAnimation());
    }

    private IEnumerator ProgressAnimation()
    {
        float startTime = 0;
        _boilProgress.maxValue = CalcCookTime();

        while (startTime < _boilProgress.maxValue)
        {
            _canFillBottle = false;
            startTime += Time.deltaTime * _speed;
            _boilProgress.value = startTime;
            yield return new WaitForFixedUpdate();
        }

        _canFillBottle = true;
        _speed = 0;       
    }

    private float CalcCookTime()
    {
        CheckRareIngredient();
        float time = 0;

        if (_isRarePotion) 
        {
            time = _settings.timeBrewRare;
        }            
        else
        {
            switch (_mixingSystem.Ingredients.Count)
            {
                case 2:
                    time = _settings.timeBrew2;
                    break;

                case 3:
                    time = _settings.timeBrew3;
                    break;

                case 4:
                    time = _settings.timeBrew4;
                    break;

                default:
                    break;
            }
        }

        time *= _claudron.CookSpeed;

        return time;
    }

    private void CheckRareIngredient()
    {
        foreach (var item in _mixingSystem.Ingredients)
        {
            if (item.IngredientData.isRareIngredient)
            {
                _isRarePotion = true;
            }
        }

        _isRarePotion = false;
    }

    private void RefreshBar()
    {
        _boilProgress.value = 0;
    }

    private void OnDisable()
    {
        _mixingSystem._refreshDelegate -= RefreshBar;
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Cook : MonoBehaviour
{
    private const float cookingSpeed = 1f;

    [SerializeField] private Button _cookButton;
    [SerializeField] private Slider _boilProgress;
    [SerializeField] private Settings _settings;

    private MixingSystemv2 _mixingSystem;        
    private Claudron _claudron;
    private WaterColorv2 _waterColor;
    private AudioSource _audioSource;
    
    private bool _isRarePotion;

    private float _speed = 0;
    private bool _canFillBottle = false;

    public bool CanFillBottle => _canFillBottle;

    private void Start()
    {
        _waterColor = GetComponentInChildren<WaterColorv2>();
        _mixingSystem = GetComponent<MixingSystemv2>();
        _claudron = GetComponent<Claudron>();
        _audioSource = GetComponent<AudioSource>();

        _mixingSystem._refreshDelegate += RefreshBar;
        _cookButton.onClick.AddListener(Brew);
    }
    public void Brew()
    {
        _speed = cookingSpeed;
        _waterColor.Boiled();
        _audioSource.Play();
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

        _waterColor.StopBoiled();
        _audioSource.Stop();
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

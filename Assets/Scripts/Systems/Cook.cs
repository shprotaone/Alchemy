using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cook : MonoBehaviour
{
    private const float cookingSpeed = 1f;
    private float _delayTime = 5;

    [SerializeField] private Button _cookButton;
    [SerializeField] private Slider _boilProgress;
    [SerializeField] private Settings _settings;
    [SerializeField] private AudioClip _brewSound;
    [SerializeField] private SpriteRenderer _crunchSprite;
    [SerializeField] private TMP_Text _delayText;
    [SerializeField] private Firewood _firewood;

    private MixingSystemv2 _mixingSystem;
    private ClaudronSystem _claudron;   
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
        _claudron = GetComponent<ClaudronSystem>();
        _audioSource = GetComponent<AudioSource>();

        _mixingSystem._refreshDelegate += RefreshBar;
        _cookButton.onClick.AddListener(Brew);
    }
    public void Brew()
    {
        _speed = cookingSpeed;
        _waterColor.Boiled();
        _audioSource.PlayOneShot(_brewSound);
        StartCoroutine(ProgressAnimation());
    }

    private IEnumerator ProgressAnimation()
    {
        float startTime = 0;
        float endTime = CalcCookTime();


        _boilProgress.maxValue = endTime;

        while (startTime < _boilProgress.maxValue)
        {
            _canFillBottle = false;
            startTime += Time.deltaTime * _speed;
            _boilProgress.value = startTime;
            yield return new WaitForFixedUpdate();
        }

        _mixingSystem.CheckPotion();
        CheckTruePotion(_mixingSystem.BottleFilled);
        _waterColor.StopBoiled();
        _audioSource.Stop();
        
        _speed = 0;
    }

    private void CheckTruePotion(bool value)
    {
        if (value)
        {
            _canFillBottle = true;
        }
        else
        {
            _crunchSprite.enabled = true;
            _canFillBottle = false;
            StartCoroutine(FailedDelay());
        }
    }

    private IEnumerator FailedDelay()
    {
        float startTimer = _delayTime;
        _delayText.gameObject.SetActive(true);

        while (startTimer > 0)
        {
            _cookButton.interactable = false;            
            _delayText.text = startTimer.ToString();
            startTimer--;
            yield return new WaitForSeconds(1f);
        }

        _cookButton.interactable = true;
        _delayText.gameObject.SetActive(false);
        startTimer = _delayTime;

        yield return null;
    }

    public void CleanClaudron()
    {
        _crunchSprite.enabled = false;
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

        if (_firewood.Activated)
        {
            time -= _firewood.SpeedMultiply;
        }
        
        print("Current cook Time " + time);

        return time;
    }

    private void CheckRareIngredient()
    {
        foreach (var item in _mixingSystem.Ingredients)
        {
            if (item.IngredientData.resourceRarity == ResourceRarity.rare)
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

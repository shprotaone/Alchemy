using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PotionTaskSystem : MonoBehaviour
{    
    [SerializeField] private JSONReader _jsonReader;
    [SerializeField] private StringToSprite _stringToSprite;

    [SerializeField] private PotionCyclopedia _potionCyclopedia;
    [SerializeField] private Money _moneySystem;
    [SerializeField] private VisitorController _visitorController;
    [SerializeField] private PotionSizer _currentSizer;
    [SerializeField] private GuildSystem _guildSystem;

    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private Transform _jarTransform;

    [SerializeField] private bool _imageTask;
    
    private RewardCalculator _rewardCalculator;
    
    private PotionSizer _potionSizer;
    private PotionSizer _basePotionSizer;
    private Potion _currentPotion;

    private int _numberTask;
    private bool _rareTaskInclude;
    private bool _tutorialLevel;
    
    public Potion CurrentPotion => _currentPotion;
    public GameObject CoinPrefab => _coinPrefab;
    public Transform JarTransform => _jarTransform;

    private void Start()
    {
        TutorialSystem.OnEndedTutorial += TutorialMode;

        _potionSizer = _jsonReader.PotionSizer;
        _currentSizer = new PotionSizer();
        _rewardCalculator = new RewardCalculator();

        _currentPotion = GetComponent<Potion>();

    }

    public void TakeTask(PotionTask task)
    {
        PotionData currentPotionData;

        if (_tutorialLevel)
        {
            currentPotionData = GetFirstTaskPotion();
        }
        else
        {
            currentPotionData = GetTaskPotion();
        }
          
        _currentPotion.FillPotion(currentPotionData);
        
        if (_imageTask)
        {
            Sprite[] ingredientsSprite = new Sprite[_currentPotion.Ingredients.Length];

            for (int i = 0; i < ingredientsSprite.Length; i++)
            {
                if(_currentPotion.Ingredients[i] != "*")
                ingredientsSprite[i] = _stringToSprite.ParseStringToSprite(_currentPotion.Ingredients[i]);
            }

            task.FillTask(ingredientsSprite,GetReward(_currentPotion));
        }
        else
        {
            task.FillTask(_currentPotion.PotionName, GetReward(_currentPotion));
        }
    }

    public void TaskComplete(int reward, float rewardRep)
    {
        _moneySystem.Increase(reward);
        _potionCyclopedia.FindPotion(_currentPotion.PotionName);
        _guildSystem.AddRep(_currentPotion.GuildsType, rewardRep);

        _visitorController.DisableVisitor();
    }

    public void TaskCanceled(float penaltyRep)
    {
        _guildSystem.RemoveRep(_currentPotion.GuildsType, penaltyRep);
        _visitorController.DisableVisitor();
    }

    public void SetPotionSizer(bool rare)
    {
        _rareTaskInclude = rare;

        if (!_rareTaskInclude)
        {
            OnlyCommonPotion();
            _currentSizer = _basePotionSizer;
        }
        else
        {
            _currentSizer = _potionSizer;            
        }
    }

    private PotionData GetTaskPotion()
    {
        _numberTask = Random.Range(0, _currentSizer.Potions.Length);
        return _currentSizer.Potions[_numberTask];
    }    

    public PotionData GetFirstTaskPotion()
    {
        _tutorialLevel = false;
        return _currentSizer.Potions[0];
    }

    private int GetReward(Potion potion)
    {
        _rewardCalculator.Calculate(potion.GuildsType, potion.Rarity);
        return _rewardCalculator.Reward;
    }

    private void OnlyCommonPotion()
    {
        List<PotionData> result = new List<PotionData>();

        foreach (var item in _potionSizer.Potions)
        {
            if (item.rarity == ResourceRarity.common.ToString())
            {
                result.Add(item);
            }
        }
        _basePotionSizer = new PotionSizer();
        _basePotionSizer.Potions = result.ToArray();
    }

    public void SetTutorialMode()
    {
        _tutorialLevel = true;
    }
    public void TutorialMode(bool value)
    {
        SetPotionSizer(_rareTaskInclude);
    }
    private void OnDisable()
    {
        TutorialSystem.OnEndedTutorial -= TutorialMode;
    }
}

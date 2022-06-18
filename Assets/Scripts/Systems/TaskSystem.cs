using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TaskSystem : MonoBehaviour
{    
    [SerializeField] private JSONReader _jsonReader;
    [SerializeField] private StringToSprite _stringToSprite;

    [SerializeField] private PotionCyclopedia _potionCyclopedia;
    [SerializeField] private Money _moneySystem;
    [SerializeField] private PotionSizer _currentSizer;
    [SerializeField] private GuildSystem _guildSystem;

    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private Transform _jarTransform;

    [SerializeField] private bool _trainingTask;
    [SerializeField] private bool _commonType;

    private RewardCalculator _rewardCalculator;
    
    private PotionSizer _potionSizer;
    private PotionSizer _basePotionSizer;
    private Potion _currentPotion;

    private int _numberTask;
    private bool _init;
    
    public Potion CurrentPotion => _currentPotion;
    public GameObject CoinPrefab => _coinPrefab;
    public Transform JarTransform => _jarTransform;
    public PotionCyclopedia PotionCyclopedia => _potionCyclopedia;

    private void Start()
    {
        _potionSizer = _jsonReader.PotionSizer;
        _currentSizer = new PotionSizer();
        _rewardCalculator = new RewardCalculator();

        _currentPotion = GetComponent<Potion>();

        if (_commonType)
        {
            OnlyCommonPotion();
            _currentSizer = _basePotionSizer;
        }
        else
        {
            _currentSizer = _potionSizer;
        }
    }

    public void TakeTask(Task task)
    {
        PotionData currentPotionData = SetTaskPotion();

        _currentPotion.FillPotion(currentPotionData);
        
        if (_trainingTask)
        {
            Sprite firstIngredient = _stringToSprite.ParseStringToSprite(_currentPotion.Ingredients[0]);
            Sprite secondIngredient = _stringToSprite.ParseStringToSprite(_currentPotion.Ingredients[1]);
            Sprite thirdIngredient = null;
            if (_currentPotion.Ingredients[2] != "*")
            {
                thirdIngredient = _stringToSprite.ParseStringToSprite(_currentPotion.Ingredients[2]);
            }
             

            task.FillTask(firstIngredient, secondIngredient,thirdIngredient,SetReward(_currentPotion));  //к системе тасков добавить стоимость, занести ее в JSON таблицу
        }
        else
        {
            task.FillTask(_currentPotion.PotionName, SetReward(_currentPotion));
        }
    }

    public void TaskComplete(int reward, float rewardRep)
    {
        _moneySystem.Increase(reward);
        _potionCyclopedia.FindPotion(_currentPotion.PotionName);
        _guildSystem.AddRep(_currentPotion.GuildsType, rewardRep);
    }

    public void TaskCanceled(float penaltyRep)
    {
        _guildSystem.RemoveRep(_currentPotion.GuildsType, penaltyRep);
    }

    private PotionData SetTaskPotion()
    {
        _numberTask = Random.Range(0, _currentSizer.Potions.Length);
        return _currentSizer.Potions[_numberTask];
    }    

    private int SetReward(Potion potion)
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
}

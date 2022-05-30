using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TaskSystem : MonoBehaviour
{    
    [SerializeField] private JSONReader _jsonReader;
    [SerializeField] private StringToSprite _stringToSprite;
    [SerializeField] private Money _moneySystem;
    
    [SerializeField] private bool _trainingTask;
    [SerializeField] private bool _commonType;

    private RewardCalculator _rewardCalculator;
    private PotionSizer _currentSizer;
    private PotionSizer _potionSizer;
    private PotionSizer _basePotionSizer;
    private PotionData _currentPotion;

    private int _numberTask;
    
    public PotionData CurrentPotion => _currentPotion;

    private void Awake()
    {
        _potionSizer = _jsonReader._potionSizer;
        _currentSizer = new PotionSizer();
        _rewardCalculator = new RewardCalculator();

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
        PotionData currentPotion = SetTaskPotion();
        
        if (_trainingTask)
        {
            Sprite firstIngredient = _stringToSprite.ParseStringToSprite(currentPotion.firstIngredient);
            Sprite secondIngredient = _stringToSprite.ParseStringToSprite(currentPotion.secondIngredient);

            task.FillTask(firstIngredient, secondIngredient, SetReward(currentPotion));  //к системе тасков добавить стоимость, занести ее в JSON таблицу
        }
        else
        {
            task.FillTask(currentPotion.name, SetReward(currentPotion));
        }
        
        _currentPotion = currentPotion;
    }

    public void TaskComplete(int reward)
    {
        _moneySystem.Increase(reward);
    }

    private PotionData SetTaskPotion()
    {
        _numberTask = Random.Range(0, _currentSizer.Potions.Length);
        return _currentSizer.Potions[_numberTask];
    }    

    private int SetReward(PotionData potionData)
    {
        _rewardCalculator.Calculate(potionData.GuildsType, potionData.Rarity);
        return _rewardCalculator.Reward;
    }

    private void OnlyCommonPotion()
    {
        List<PotionData> result = new List<PotionData>();
        foreach (var item in _potionSizer.Potions)
        {
            if (item.Rarity == ResourceRarity.Common)
            {
                result.Add(item);
            }
        }

        _basePotionSizer = new PotionSizer();
        _basePotionSizer.Potions = result.ToArray();
    }
}

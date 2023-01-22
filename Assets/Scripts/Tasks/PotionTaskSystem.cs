using System;
using System.Collections.Generic;
using UnityEngine;

public class PotionTaskSystem : MonoBehaviour
{
    [SerializeField] private TradeSystem _tradeSystem;
    [SerializeField] private VisitorController _visitorController;
    [SerializeField] private LabelToSprite _labelToSprite;
    [SerializeField] private TaskChance _chances;
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private Transform _jarTransform;

    [SerializeField] private bool _imageTask;

    private RewardCalculator _rewardCalculator;
    private List<PotionLabelType> _labels;
    
    private Potion _currentPotion;

    public void Init(List<CounterTask> chances, Money moneySystem)
    {
        _currentPotion = new Potion();
        _rewardCalculator = new RewardCalculator();
        _chances = new TaskChance(chances);
    }

    public PotionTask GetTaskv2()
    {
        PotionTask potionTask;

        _currentPotion = GetPotionForTask();

        potionTask = new PotionTask(_currentPotion, _visitorController, this);
        potionTask.SetReward((int)_rewardCalculator.GetReward(_currentPotion.Labels.Count));

        _tradeSystem.SetTask(potionTask);

        return potionTask;
    }    

    private Potion GetPotionForTask()
    {
        //TODO: CLEAR
        int count = _chances.GetTaskCount();

        _labels = new List<PotionLabelType>();    

        for (int i = 0; i < count; i++)
        {
            _labels.Add(_chances.GetRandomLabel());
        }

        return new Potion(_labels);
    }  

    public Sprite[] GetLabels(int count)
    {
        Sprite[] sprites = new Sprite[count];

        for (int i = 0; i < count; i++)
        {
            sprites[i] = _labelToSprite.GetSprite(_labels[i]);
        }

        return sprites;
    }

}

#region OldSystem

//[SerializeField] private JSONReader _jsonReader;

//[SerializeField] private StringToSprite _stringToSprite;
//[SerializeField] private PotionTaskList _potionCyclopedia;
//[SerializeField] private ContrabandPotionSystem _contrabandPotionSystem;
//[SerializeField] private PotionSizer _potionSizer;
//[SerializeField] private GuildSystem _guildSystem;


//private float _coinReward;
//private float _rewardMultiply = 1f;
//private float _penaltyMultiply = 1f;


//private int _numberTask;
//private bool _tutorialLevel;

//public PotionSizer PotionSizer => _potionSizer;

//public int CalculateReward(int matchIndex)
//{
//    float result = _rewardCalculator.CalculateResult(_currentTask.RewardCoin,
//                                                     _currentTask.CurrentPotion.Ingredients.Count, 
//                                                     matchIndex);

//    return (int)result;
//    //float result = currentReward * _rewardMultiply;
//    //result = LowRepReward((int)result);

//    //return (int)result;
//}



//public PotionTask GetTask()
//{
//    PotionData currentPotionData;
//    PotionTask potionTask;

//    if (_tutorialLevel)
//    {
//        currentPotionData = GetFirstTaskPotion();
//    }
//    else
//    {
//        currentPotionData = GetPotionForTask();
//    }

//    _currentPotion = new Potion(currentPotionData);

//    if (_contrabandPotionSystem.IsActive)
//    {
//        if (_currentPotion.PotionName == _contrabandPotionSystem.ContrabandPotion.PotionName)
//        {
//            _currentPotion.SetContraband(true);
//        }
//    }

//    potionTask = new PotionTask(_currentPotion,_visitorController,this);

//    _currentTask = potionTask;

//    return potionTask;
//}

//private PotionData GetPotionForTask()
//{
//    int digit = Random.Range(0, 101);

//    if (!_contrabandPotionSystem.IsActive)
//    {
//        _numberTask = Random.Range(0, _potionSizer.Potions.Length);
//    }
//    else
//    {
//        if (digit <= _contrabandPotionSystem.ContrabandPotionChance)
//        {
//            return _contrabandPotionSystem.ContrabandPotion.PotionData;
//        }
//        else
//        {
//            _numberTask = Random.Range(0, _potionSizer.Potions.Length);
//        }
//    }

//    return _potionSizer.Potions[_numberTask];
//}

//public void SetPenaltyMultiply(float multiply)
//{
//    if (multiply != 0)
//    {
//        _penaltyMultiply = multiply;
//    }
//    else
//    {
//        Debug.LogWarning("Множитель равен 0, поэтому отключен");
//    }        
//}

//public Sprite[] GetIngredientSprites(PotionTask task)
//{
//    Sprite[] ingredientsSprite = new Sprite[task.CurrentPotion.Ingredients.Count];

//    for (int i = 0; i < ingredientsSprite.Length; i++)
//    {
//        if (task.CurrentPotion.Ingredients[i] != null)
//            ingredientsSprite[i] = _stringToSprite.ParseStringToSprite(task.CurrentPotion.Ingredients[i]);
//    }

//    return ingredientsSprite;
//}

//public PotionData GetFirstTaskPotion()
//{
//    _tutorialLevel = false;
//    return _potionSizer.Potions[0];
//}

//public int LowRepReward(int stockReward)
//{
//    bool divideReward = _guildSystem.GuildDictionary[_visitorController.CurrentVisitor.Guild] < 60;

//    if (divideReward) return stockReward / 2;
//    else return stockReward;
//}

//public void SetTutorialMode(bool value)   //проверить на использование
//{
//    _tutorialLevel = value;
//}

/// <summary>
/// процентный множитель
/// </summary>
/// <param name="multiply"></param>
//public void SetRewardMultiply(float multiply)
//{
//    if(multiply != 0)
//    {
//        _rewardMultiply = multiply;
//    }       
//}

//public void TaskComplete(Potion bottlePotion)
//    {
//        int indexMatch = MatchCalculate.IndexMatchLabel(bottlePotion.Labels, _currentPotion.Labels);
//        float result = _rewardCalculator.CalculateResult(bottlePotion.Labels.Count,indexMatch);

//        _moneySystem.Increase((int)result);

//        Debug.Log("Получил " + result + "Совпадений " + indexMatch);
////StartCoinAnimation();
//        _visitorController.DisableVisitor();

//        OnTaskComplete?.Invoke();
//    }

#endregion
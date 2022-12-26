using UnityEngine;

public class PotionTaskSystem : MonoBehaviour
{    
    [SerializeField] private JSONReader _jsonReader;

    [SerializeField] private StringToSprite _stringToSprite;
    [SerializeField] private PotionCyclopedia _potionCyclopedia;
    [SerializeField] private ContrabandPotionSystem _contrabandPotionSystem;
    
    [SerializeField] private VisitorController _visitorController;
    [SerializeField] private PotionSizer _potionSizer;
    [SerializeField] private GuildSystem _guildSystem;

    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private Transform _jarTransform;

    [SerializeField] private bool _imageTask;

    private Money _moneySystem;
    private RewardCalculator _rewardCalculator;

    private float _coinReward;
    private float _rewardMultiply = 1f;
    private float _penaltyMultiply = 1f;
    
    private Potion _currentPotion;
    private PotionTask _currentTask;

    private int _numberTask;
    private bool _tutorialLevel;

    public PotionSizer PotionSizer => _potionSizer;
    public bool ImageTask => _imageTask;

    public void InitPotionSizer(Money moneySystem,SizerType sizer, int countForCustomSizer)
    {
        _currentPotion = new Potion();
        _moneySystem = moneySystem;
        _potionSizer = _jsonReader.PotionSizer;

        _rewardCalculator = new RewardCalculator();

        PotionSizerSelection sizerSelector = new PotionSizerSelection(_potionSizer);
        _potionSizer = sizerSelector.SizerSelector(sizer);
        _potionSizer = sizerSelector.SetRangeSizerWithRandom(countForCustomSizer);

        _potionCyclopedia.InitPotionCyclopedia();
    }

    public PotionTask GetTask()
    {
        PotionData currentPotionData;
        PotionTask potionTask;

        if (_tutorialLevel)
        {
            currentPotionData = GetFirstTaskPotion();
        }
        else
        {
            currentPotionData = GetTaskPotion();
        }

        _currentPotion = new Potion(currentPotionData);

        if (_contrabandPotionSystem.IsActive)
        {
            if (_currentPotion.PotionName == _contrabandPotionSystem.ContrabandPotion.PotionName)
            {
                _currentPotion.SetContraband(true);
            }
        }
        
        potionTask = new PotionTask(_currentPotion,_visitorController,this);
        
        _currentTask = potionTask;

        return potionTask;
    }

    public void TaskComplete(Potion potionInBottle)
    {
        int countMatch = MatchCalculate.IndexMatch(potionInBottle, _currentTask.CurrentPotion);
        _coinReward = CalculateReward(countMatch);

        _moneySystem.Increase((int)_coinReward);
        StartCoinAnimation();
        _visitorController.DisableVisitor();    
    }

    private void StartCoinAnimation()
    {
        GameObject curCoin = ObjectPool.SharedInstance.GetObject(ObjectType.COINDROP);
        curCoin.transform.position = _currentTask.CurrentTaskView.transform.position;
        Coin coin = curCoin.GetComponent<Coin>();
        coin.Movement(_jarTransform.position);
    }

    public void TaskCanceled()
    {
        //_guildSystem.RemoveRep(_currentPotion.GuildsType, penaltyRep * _penaltyMultiply);
        _visitorController.DisableVisitor();
    }

    private PotionData GetTaskPotion()
    {
        int digit  = Random.Range(0,101);

        if (!_contrabandPotionSystem.IsActive)
        {
            _numberTask = Random.Range(0, _potionSizer.Potions.Length);
        }
        else
        {
            if (digit <= _contrabandPotionSystem.ContrabandPotionChance)
            {
                return _contrabandPotionSystem.ContrabandPotion.PotionData;
            }
            else
            {
                _numberTask = Random.Range(0, _potionSizer.Potions.Length);
            }
        }
        
        return _potionSizer.Potions[_numberTask];
    }    

    public PotionData GetFirstTaskPotion()
    {
        _tutorialLevel = false;
        return _potionSizer.Potions[0];
    }

    public int LowRepReward(int stockReward)
    {
        bool divideReward = _guildSystem.GuildDictionary[_visitorController.CurrentVisitor.Guild] < 60;

        if (divideReward) return stockReward / 2;
        else return stockReward;
    }

    public void SetTutorialMode(bool value)   //проверить на использование
    {
        _tutorialLevel = value;
    }

    /// <summary>
    /// процентный множитель
    /// </summary>
    /// <param name="multiply"></param>
    public void SetRewardMultiply(float multiply)
    {
        if(multiply != 0)
        {
            _rewardMultiply = multiply;
        }       
    }

    public int CalculateReward(int matchIndex)
    {
        float result = _rewardCalculator.CalculateResult(_currentTask.RewardCoin,
                                                         _currentTask.CurrentPotion.Ingredients.Count, 
                                                         matchIndex);

        return (int)result;
        //float result = currentReward * _rewardMultiply;
        //result = LowRepReward((int)result);

        //return (int)result;
    }

    public void SetPenaltyMultiply(float multiply)
    {
        if (multiply != 0)
        {
            _penaltyMultiply = multiply;
        }
        else
        {
            Debug.LogWarning("Множитель равен 0, поэтому отключен");
        }        
    }

    public Sprite[] GetIngredientSprites(PotionTask task)
    {
        Sprite[] ingredientsSprite = new Sprite[task.CurrentPotion.Ingredients.Count];

        for (int i = 0; i < ingredientsSprite.Length; i++)
        {
            if (task.CurrentPotion.Ingredients[i] != null)
                ingredientsSprite[i] = _stringToSprite.ParseStringToSprite(task.CurrentPotion.Ingredients[i]);
        }

        return ingredientsSprite;
    }
}

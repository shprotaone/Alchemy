using UnityEngine;

public class PotionTaskSystem : MonoBehaviour
{    
    [SerializeField] private JSONReader _jsonReader;

    [SerializeField] private StringToSprite _stringToSprite;
    [SerializeField] private PotionCyclopedia _potionCyclopedia;
    [SerializeField] private ContrabandPotionSystem _contrabandPotionSystem;
    [SerializeField] private Money _moneySystem;
    [SerializeField] private VisitorController _visitorController;
    [SerializeField] private PotionSizer _potionSizer;
    [SerializeField] private GuildSystem _guildSystem;

    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private Transform _jarTransform;

    [SerializeField] private bool _imageTask;

    private float _coinReward;
    private float _rewardMultiply = 1f;
    private float _penaltyMultiply = 1f;
    
    private Potion _currentPotion;

    private int _numberTask;
    private bool _tutorialLevel;
    
    public Potion CurrentPotion => _currentPotion;
    public GameObject CoinPrefab => _coinPrefab;
    public Transform JarTransform => _jarTransform;
    public PotionSizer PotionSizer => _potionSizer;
    public bool ImageTask => _imageTask;

    /// <summary>
    /// Инициализация текущего списка зелий
    /// </summary>
    /// 
    public void Init()
    {                
        _currentPotion = new Potion();       
    }  

    public void SetPotionSizer(SizerType sizer, int countForCustomSizer)
    {
        _potionSizer = _jsonReader.PotionSizer;
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
        

        potionTask = new PotionTask(_currentPotion,_visitorController.CurrentVisitor.TaskView, _visitorController.CurrentVisitor);
        _coinReward = potionTask.RewardCoin;
        _coinReward = CalculateReward();

        potionTask.SetReward((int)_coinReward);

        return potionTask;
    }

    public void TaskComplete(float rewardRep)
    {
        _moneySystem.Increase((int)_coinReward);
        
        //_potionCyclopedia.AddNewPotion(_currentPotion);
        _guildSystem.AddRep(_currentPotion.GuildsType, rewardRep);

        _visitorController.DisableVisitor();

        if (_currentPotion.Contraband)
        {
            GetGemReward(_currentPotion);
        }
    }

    public void TaskCanceled(float penaltyRep)
    {
        _guildSystem.RemoveRep(_currentPotion.GuildsType, penaltyRep * _penaltyMultiply);
        _visitorController.DisableVisitor();
    }

    private PotionData GetTaskPotion()
    {
        int digit  = Random.Range(0,101);

        if (!_contrabandPotionSystem)
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

    private void GetGemReward(Potion potion)
    {
        _contrabandPotionSystem.AddCounter();
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

    public int CalculateReward()
    {
        float result = _coinReward * _rewardMultiply;
        result = LowRepReward((int)result);

        return (int)result;
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

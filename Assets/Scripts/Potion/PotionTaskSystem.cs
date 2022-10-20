using UnityEngine;

public class PotionTaskSystem : MonoBehaviour
{    
    [SerializeField] private JSONReader _jsonReader;
    [SerializeField] private StringToSprite _stringToSprite;

    [SerializeField] private PotionCyclopedia _potionCyclopedia;
    [SerializeField] private Money _moneySystem;
    [SerializeField] private VisitorController _visitorController;
    [SerializeField] private PotionSizer _potionSizer;
    [SerializeField] private GuildSystem _guildSystem;

    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private Transform _jarTransform;

    [SerializeField] private bool _imageTask;
    
    private RewardCalculator _rewardCalculator;
    private float _rewardMultiply = 1f;
    
    private Potion _currentPotion;

    private int _numberTask;
    private bool _tutorialLevel;
    private bool _contrabandLevel;
    
    public Potion CurrentPotion => _currentPotion;
    public GameObject CoinPrefab => _coinPrefab;
    public Transform JarTransform => _jarTransform;

    /// <summary>
    /// Инициализация текущего списка зелий
    /// </summary>
    public void InitPotionSizer(LevelNumber levelNumber)
    {
        _potionSizer = _jsonReader.PotionSizer;
        PotionSizerSelection sizerSelector = new PotionSizerSelection(_potionSizer);
        _potionSizer = sizerSelector.SizerSelector(levelNumber);
       
        _rewardCalculator = new RewardCalculator();
        _currentPotion = new Potion();
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
        FillViewPotion(task);

        if (_contrabandLevel)
        {
            if (_numberTask == GetContrabandPotionIndex())
            {
                _currentPotion.SetContraband();
            }
        }       
    }

    /// <summary>
    /// Заполняет изображение или название
    /// </summary>
    /// <param name="task"></param>
    private void FillViewPotion(PotionTask task)
    {
        if (_imageTask)
        {
            Sprite[] ingredientsSprite = new Sprite[_currentPotion.Ingredients.Count];

            for (int i = 0; i < ingredientsSprite.Length; i++)
            {
                if (_currentPotion.Ingredients[i] != null)
                    ingredientsSprite[i] = _stringToSprite.ParseStringToSprite(_currentPotion.Ingredients[i]);
            }

            task.FillTask(ingredientsSprite, GetReward(_currentPotion));
        }
        else
        {
            task.FillTask(_currentPotion.PotionName, GetReward(_currentPotion));
        }
    }

    public void TaskComplete(int reward, float rewardRep)
    {
        _moneySystem.Increase(reward);
        
        _potionCyclopedia.AddNewPotion(_currentPotion);
        _guildSystem.AddRep(_currentPotion.GuildsType, rewardRep);

        _visitorController.DisableVisitor();
    }

    public void TaskCanceled(float penaltyRep)
    {
        _guildSystem.RemoveRep(_currentPotion.GuildsType, penaltyRep);
        _visitorController.DisableVisitor();
    }

    private PotionData GetTaskPotion()
    {
        _numberTask = Random.Range(0, _potionSizer.Potions.Length);
        return _potionSizer.Potions[_numberTask];
    }    

    public PotionData GetFirstTaskPotion()
    {
        _tutorialLevel = false;
        return _potionSizer.Potions[0];
    }

    private int GetReward(Potion potion)
    {
        int reward = 0;
        float resultReward = 0;

        _rewardCalculator.Calculate(_visitorController.CurrentVisitor.Guild, potion.Rarity);

        resultReward = _rewardCalculator.Reward * _rewardMultiply;

        reward = (int)resultReward;
        reward = LowRepReward(reward);
        
        print(reward);
        return reward;
    }

    private int LowRepReward(int stockReward)
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
        _rewardMultiply = multiply;
    }
    /// <summary>
    /// Присваивает рандомный индекс контрабандному зелью
    /// </summary>
    /// <returns></returns>
    public int GetContrabandPotionIndex()
    {
        return Random.Range(0,_potionSizer.Potions.Length);
    }

    public void SetContrabandtLevel()
    {
        _contrabandLevel = true;
    }
}

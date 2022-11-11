using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelPreset : ScriptableObject
{    
    [Header("Стартовые настройки")]

    public bool ShopController;
    public Sprite backgroundSprite;
    public bool isTutorial;
    public int addCommonResourceCount;
    public SizerType sizer;
    public int countIngredientInPotionForSizer;
    public float visitorTime;
    
    [Space]
    [Header("Настройки денег")]
    public int startMoney;
    public int minRangeMoney;
    public int MoneyGoal;

    [Space]
    [Header("Настройки условий игры")]
    public bool timerIsActive;
    public int levelTimeInSeconds;

    [Space]
    [Header("Настройка аренды")]
    
    public bool rentActive;
    public int rent;
    public int secondsForRent;

    [Space]
    [Header("Настройка множителя стоимости ингредиентов")]
    
    public bool isCostMultiplayActive;
    public IngredientData[] ingredientsForMultiplyCost;
    public int multiplyCostIngredient;

    [Space]
    [Header("Настройка для добавки ресурсов")]
    public bool isRandomResourceAdd;
    public bool addCommonResource;
    public bool addRareResource;
    public int countRandomResource;
    public int countTryAddRandomResource;

    [Space]
    [Header("Настройка задания по накоплению ингредиентов")]
    public bool isCollectIngredient;
    public int collectCommonResourceCount;
    public int collectRareResourceCount;

    [Space]
    [Header("Настройка множителей награды и ошибки")]
    public float _rewardMultiply;
    public float _penaltyMultyply;

    [Space]
    [Header("Настройка штрафа гильдий")]
    public int timeDecreaseReputation;
    public int reduceValue;

    [Space]
    [Header("Настройки для контрабандного уровня")]
    public bool isContrabandLevel;
    public int contrabadPotionChance;
    public int contrabandTimer;
    public int contrabandVisitorTimer;

    [Header("Вступительный текст")]
    [TextArea]
    public string[] levelTaskText;
    

    [Header("Текст в заданиях")]
    [TextArea(5, 10)]
    public string goalText;
    public bool DropDownMenu;
}

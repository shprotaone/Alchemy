using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelPreset : ScriptableObject
{
    [Header("Стартовые настройки")]

    public LevelNumber levelNumber;
    public Sprite backgroundSprite;
    public int addCommonResourceCount;
    [HideInInspector]
    public int startBottleCount = 10000;
    public int visitorCount;

    public int chance1Label;
    public int chance2Label;
    public int chance3Label;

    [Space]
    [Header("Настройки денег")]
    public int startMoney;
    public int minRangeMoney;

    [Header("Вступительный текст")]
    [TextArea]
    public string[] levelTaskText;
    
    [Header("Текст в заданиях")]
    [TextArea(5, 10)]
    public string goalText;
    public bool DropDownMenu;

}

#region Old
//public int MoneyGoal;

//[Space]
//[Header("Настройки условий игры")]
//public bool timerIsActive;
//public int levelTimeInSeconds;

//[Space]
//[Header("Настройка аренды")]

//public bool rentActive;
//public int rent;
//public int secondsForRent;

//[Space]
//[Header("Настройка множителя стоимости ингредиентов")]

//public bool isCostMultiplyActive;
//public IngredientData[] ingredientsForMultiplyCost;
//public int multiplyCostIngredient;

//[Space]
//[Header("Настройка для добавки ресурсов")]
//public bool isRandomResourceAdd;
//public bool addCommonResource;
//public bool addRareResource;
//public int countRandomResource;
//public int countTryAddRandomResource;

//[Space]
//[Header("Настройка задания по накоплению ингредиентов")]
//public bool isCollectIngredient;
//public int collectCommonResourceCount;
//public int collectRareResourceCount;

//[Space]
//[Header("Настройка множителей награды и ошибки")]
//public float _rewardMultiply;
//public float _penaltyMultiply;

//[Space]
//[Header("Настройки для контрабандного уровня")]
//public bool isContrabandLevel;
//public int contrabadPotionChance;
//public int contrabandTimer;
//public int contrabandVisitorTimer;
#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelPreset : ScriptableObject
{    
    [Header("��������� ���������")]

    public bool ShopController;
    public Sprite backgroundSprite;
    public bool isTutorial;
    public int addCommonResourceCount;
    public SizerType sizer;
    public int countIngredientInPotionForSizer;
    public float visitorTime;
    
    [Space]
    [Header("��������� �����")]
    public int startMoney;
    public int minRangeMoney;
    public int MoneyGoal;

    [Space]
    [Header("��������� ������� ����")]
    public bool timerIsActive;
    public int levelTimeInSeconds;

    [Space]
    [Header("��������� ������")]
    
    public bool rentActive;
    public int rent;
    public int secondsForRent;

    [Space]
    [Header("��������� ��������� ��������� ������������")]
    
    public bool isCostMultiplayActive;
    public IngredientData[] ingredientsForMultiplyCost;
    public int multiplyCostIngredient;

    [Space]
    [Header("��������� ��� ������� ��������")]
    public bool isRandomResourceAdd;
    public bool addCommonResource;
    public bool addRareResource;
    public int countRandomResource;
    public int countTryAddRandomResource;

    [Space]
    [Header("��������� ������� �� ���������� ������������")]
    public bool isCollectIngredient;
    public int collectCommonResourceCount;
    public int collectRareResourceCount;

    [Space]
    [Header("��������� ���������� ������� � ������")]
    public float _rewardMultiply;
    public float _penaltyMultyply;

    [Space]
    [Header("��������� ������ �������")]
    public int timeDecreaseReputation;
    public int reduceValue;

    [Space]
    [Header("��������� ��� �������������� ������")]
    public bool isContrabandLevel;
    public int contrabadPotionChance;
    public int contrabandTimer;
    public int contrabandVisitorTimer;

    [Header("������������� �����")]
    [TextArea]
    public string[] levelTaskText;
    

    [Header("����� � ��������")]
    [TextArea(5, 10)]
    public string goalText;
    public bool DropDownMenu;
}

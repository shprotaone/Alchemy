using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelPreset : ScriptableObject
{
    [Header("��������� ���������")]

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
    [Header("��������� �����")]
    public int startMoney;
    public int minRangeMoney;

    [Header("������������� �����")]
    [TextArea]
    public string[] levelTaskText;
    
    [Header("����� � ��������")]
    [TextArea(5, 10)]
    public string goalText;
    public bool DropDownMenu;

}

#region Old
//public int MoneyGoal;

//[Space]
//[Header("��������� ������� ����")]
//public bool timerIsActive;
//public int levelTimeInSeconds;

//[Space]
//[Header("��������� ������")]

//public bool rentActive;
//public int rent;
//public int secondsForRent;

//[Space]
//[Header("��������� ��������� ��������� ������������")]

//public bool isCostMultiplyActive;
//public IngredientData[] ingredientsForMultiplyCost;
//public int multiplyCostIngredient;

//[Space]
//[Header("��������� ��� ������� ��������")]
//public bool isRandomResourceAdd;
//public bool addCommonResource;
//public bool addRareResource;
//public int countRandomResource;
//public int countTryAddRandomResource;

//[Space]
//[Header("��������� ������� �� ���������� ������������")]
//public bool isCollectIngredient;
//public int collectCommonResourceCount;
//public int collectRareResourceCount;

//[Space]
//[Header("��������� ���������� ������� � ������")]
//public float _rewardMultiply;
//public float _penaltyMultiply;

//[Space]
//[Header("��������� ��� �������������� ������")]
//public bool isContrabandLevel;
//public int contrabadPotionChance;
//public int contrabandTimer;
//public int contrabandVisitorTimer;
#endregion

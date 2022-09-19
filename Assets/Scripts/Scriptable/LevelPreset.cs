using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelPreset : ScriptableObject
{
    public LevelNumber levelNumber;
    public bool rareIngredientBlock;
    public bool rareTask;
    public bool ShopController;

    [Header("Настройки денег")]
    public int startMoney;
    public int minRangeMoney;
    public int MoneyGoal;

    [Header("Настройки условий игры")]
    public int levelTimeInSeconds;
    public int rent;
    public int secondsForRent;

    [Header("Вступительный текст")]
    [TextArea]
    public string[] levelTaskText;
    public Sprite backgroundSprite;
}

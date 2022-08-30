using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelPreset : ScriptableObject
{
    public LevelNumber levelNumber;
    public bool startWindow;
    public bool rareIngredientBlock;
    public bool rareTask;
    public bool ShopController;
    public int[] eventCount;
    public int startMoney;
    public int completeGoal;
    public int resourceCountAdd;
    [TextArea]
    public string levelTaskText;
    public Sprite backgroundSprite;
}

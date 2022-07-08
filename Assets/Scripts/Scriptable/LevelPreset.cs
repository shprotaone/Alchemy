using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelPreset : ScriptableObject
{
    public bool startWindow;
    public bool rareIngredientBlock;
    public bool rareTask;
    public int[] eventCount;
    public int startMoney;
    public int completeGoal;
    public int resourceCountAdd;
    [TextArea]
    public string[] dialog;
}

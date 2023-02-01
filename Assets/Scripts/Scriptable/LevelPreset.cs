using UnityEngine;

[CreateAssetMenu]
public class LevelPreset : ScriptableObject
{
    [Header("Стартовые настройки")]

    public LevelNumber levelNumber;
    public Sprite backgroundSprite;
    public AudioClip _mainSound;
    public int addCommonResourceCount;

    [HideInInspector]
    public int startBottleCount = 10000;
    public int visitorCount;

    [Header ("Настройки по количеству элементов")]
    public int chance1Label;
    public int chance2Label;
    public int chance3Label;
    public int moneyTaskComplete;

    [Header("Настройка по типу игредиентов")]
    public int chanceWater;
    public int chanceFire;
    public int chanceStone;

    public bool withEvent;
}

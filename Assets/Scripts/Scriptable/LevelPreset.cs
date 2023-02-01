using UnityEngine;

[CreateAssetMenu]
public class LevelPreset : ScriptableObject
{
    [Header("��������� ���������")]

    public LevelNumber levelNumber;
    public Sprite backgroundSprite;
    public AudioClip _mainSound;
    public int addCommonResourceCount;

    [HideInInspector]
    public int startBottleCount = 10000;
    public int visitorCount;

    [Header ("��������� �� ���������� ���������")]
    public int chance1Label;
    public int chance2Label;
    public int chance3Label;
    public int moneyTaskComplete;

    [Header("��������� �� ���� �����������")]
    public int chanceWater;
    public int chanceFire;
    public int chanceStone;

    public bool withEvent;
}

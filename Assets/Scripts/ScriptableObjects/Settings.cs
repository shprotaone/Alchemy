using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Settings : ScriptableObject
{
    [Header("Время на выполнение легкого квеста, сек.")]
    public float questTimeEasy;

    [Header("Время на выполнение сложного квеста, сек.")]
    public float questTimeHard;

    [Header("Интервал выдачи квеста, сек.")]
    public float questDelay;

    [Header("Ускорение выдачи квестов при выполнении, сек.")]
    public float questSpeedup;

    [Header("Кол-во выполненных квестов для ускорения выдачи квестов")]
    public int questSpeedupStep;

    [Header("Минимальное время выдачи квеста, сек.")]
    public float questLimit;

    [Header("Время до выдачи первого квеста, сек.")]
    public float questFirst;

    [Header("Штраф за провал квеста (минимальное кол-во квестов до следующего квеста данной гильдии)")]
    public int questPenalty;

    [Header("Вознаграждение за легкий квест")]
    public int questRewardEasy;

    [Header("Вознаграждение за сложный квест")]
    public int questRewardHard;

    [Header("Сколько репутации получает гильдия за выполненный квест")]
    public int repReward;

    [Header("Сколько репутации теряет гильдия за невыполненный квест")]
    public int repPenalty;

    [Header("Изменение репутации вторичных гильдий")]
    public int repChangeSec;

    [Header("Минимум репутации для выдачи квестов + для разрешения покупки репутации")]
    public int repMin;

    [Header("Предел покупки репутации в магазине")]
    public int repLimit;

    [Header("Репутация, при которой выдают заказы на редкое зелье")]
    public int repMax;

    [Header("Репутация, при которой можно купить редкий ингридиент")]
    public int repRare;

    [Header("Начальная репутация")]
    public int rep;

    [Header("Начальные деньги")]
    public int money;

    [Header("Цена редкого ингридиента")]
    public int costRare;

    [Header("Максимум редких ингридиентов")]
    public int rareMax;

    [Header("Цена покупки репутации")]
    public int costRep;

    [Header("Сколько репутации покупаем за раз")]
    public int repAdd;

    [Header("Время варки зелья из 2 ингредиентов, сек.")]
    public int timeBrew2;

    [Header("Время варки зелья из 3 ингредиентов, сек.")]
    public int timeBrew3;

    [Header("Время варки зелья из 4 ингредиентов, сек.")]
    public int timeBrew4;

    [Header("Время варки редкого зелья, сек.")]
    public int timeBrewRare;

    [Header("Время действия дров, сек.")]
    public int timeWood;

    [Header("Во сколько раз дрова ускоряют варку")]
    public int woodSpeedup;

    [Header("Скорость полета ингридиентов на свое место")]
    public float resourceSpeed;

    [Header("Скорость полета бутылок на свое место")]
    public float bottleSpeed;

    [Header("Скорость перемещения камеры")]
    public float camSpeed;

    [Header("Как долго нельзя закрыть подсказку, сек.")]
    public float helpTime;

    [Header("Формула шанса невыплаты бандитами: X + rep / 100 * Y")]
    public int banditsX;
    public int banditsY;

    [Header("Формула шанса двойной награды от священников: X + rep / 100 * Y")]
    public int priestsX;
    public int priestsY;

    [Header("Формула шанса доп. награды от магов: X + rep / 100 * Y")]
    public int magiciansX;
    public int magiciansY;

    [Header("Цвета воды")]
    public Color[] colors = new Color[15];

    [Header("Названия цветов")]
    public string[] colorNames = new string[11];

    [Header("Названия эффектов")]
    public string[] effectNames = new string[4];
}
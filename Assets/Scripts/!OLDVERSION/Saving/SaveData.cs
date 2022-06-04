using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int money;

    public int bottleCount;
    public int bottleCost;

    public int fuelCount;

    public int amountRed;
    public int amountBlue;
    public int amountYellow;
    public int amountWhite;
    public int amountLadan;
    public int amountEye;
    public int amountStone;
    public int amountSand;

    public int cauldronId;

    public int bottle1Color;
    public int bottle2Color;
    public int bottle3Color;
    public int bottle4Color;
    public int bottle5Color;
    public int bottle6Color;
    public int bottle7Color;
    public int bottle8Color;

    public int bottle1Effect;
    public int bottle2Effect;
    public int bottle3Effect;
    public int bottle4Effect;
    public int bottle5Effect;
    public int bottle6Effect;
    public int bottle7Effect;
    public int bottle8Effect;

    public int repWarriors;
    public int repBandits;
    public int repPriests;
    public int repMagicians;

    public bool[] added = new bool[55];

    public bool[] bottleUsage = new bool[8];

    public bool mainGame;
    public bool[] helpShown = new bool[7];

    public bool firstQuest;
    public int firstAmount;

    public SaveData(MoneySystem moneySystem, Bottles bottles, ShopSystem shopSystem, Fuel fuel, ResourceSystem resourceSystem, MixingSystem mixingSystem, PotionSystem potionSystem, GuildSystemv1 guildSystem, RecipesMenu recipesMenu, Tutorial tutorial, QuestsSystem questsSystem)
    {
        money = moneySystem.money;

        bottleCount = bottles.bottleCount;
        //bottleCost = shopSystem.bottleCost;

        fuelCount = fuel.fuelCount;

        amountRed = resourceSystem.GetAmount(ResourceType.Red);
        amountBlue = resourceSystem.GetAmount(ResourceType.Blue);
        amountYellow = resourceSystem.GetAmount(ResourceType.Yellow);
        amountWhite = resourceSystem.GetAmount(ResourceType.White);
        amountLadan = resourceSystem.GetAmount(ResourceType.Ladan);
        amountEye = resourceSystem.GetAmount(ResourceType.Eye);
        amountStone = resourceSystem.GetAmount(ResourceType.Stone);
        amountSand = resourceSystem.GetAmount(ResourceType.Sand);

        cauldronId = mixingSystem.GetCauldron();

        bottle1Color = potionSystem.GetColor(0);
        bottle2Color = potionSystem.GetColor(1);
        bottle3Color = potionSystem.GetColor(2);
        bottle4Color = potionSystem.GetColor(3);
        bottle5Color = potionSystem.GetColor(4);
        bottle6Color = potionSystem.GetColor(5);
        bottle7Color = potionSystem.GetColor(6);
        bottle8Color = potionSystem.GetColor(7);

        bottle1Effect = potionSystem.GetEffect(0);
        bottle2Effect = potionSystem.GetEffect(1);
        bottle3Effect = potionSystem.GetEffect(2);
        bottle4Effect = potionSystem.GetEffect(3);
        bottle5Effect = potionSystem.GetEffect(4);
        bottle6Effect = potionSystem.GetEffect(5);
        bottle7Effect = potionSystem.GetEffect(6);
        bottle8Effect = potionSystem.GetEffect(7);

        repWarriors = guildSystem.repWarriors;
        repBandits = guildSystem.repBandits;
        repPriests = guildSystem.repPriests;
        repMagicians = guildSystem.repMagicians;

        added = recipesMenu.addedArr;

        bottleUsage = bottles.bottleUsage;

        mainGame = tutorial.mainGame;
        helpShown = tutorial.helpShown;

        firstQuest = questsSystem.firstQuest;
        firstAmount = questsSystem.firstAmount;
    }
}
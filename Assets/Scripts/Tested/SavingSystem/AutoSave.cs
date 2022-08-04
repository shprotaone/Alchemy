using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutoSave : MonoBehaviour
{
    public UnityEngine.GameObject moneySystem;
    public UnityEngine.GameObject bottles;
    public UnityEngine.GameObject resourceSystem;
    public UnityEngine.GameObject mixingSystem;
    public UnityEngine.GameObject potionSystem;
    public UnityEngine.GameObject guildSystem;
    public UnityEngine.GameObject recipes;
    public UnityEngine.GameObject UIControls;
    //public Settings settings;

    //private void Awake()
    //{
    //    StartCoroutine(AutoSaveDelay());

    //    SaveData save = SaveGameSystem.LoadGame();
    //    if (save != null)
    //    {
    //        moneySystem.GetComponent<MoneySystem>().money = save.money;

    //        for (int i = 0; i < save.bottleCount - 2; i++)
    //            bottles.GetComponent<Bottles>().AddBottle();
    //        if (save.bottleCost != 0)
    //            //moneySystem.GetComponent<ShopSystem>().bottleCost = save.bottleCost;

    //        resourceSystem.GetComponent<Fuel>().fuelCount = save.fuelCount;

    //        resourceSystem.GetComponent<ResourceSystem>().AddResource(ResourceType.Red, save.amountRed);
    //        resourceSystem.GetComponent<ResourceSystem>().AddResource(ResourceType.Blue, save.amountBlue);
    //        resourceSystem.GetComponent<ResourceSystem>().AddResource(ResourceType.Yellow, save.amountYellow);
    //        resourceSystem.GetComponent<ResourceSystem>().AddResource(ResourceType.White, save.amountWhite);
    //        resourceSystem.GetComponent<ResourceSystem>().AddResource(ResourceType.Ladan, save.amountLadan);
    //        resourceSystem.GetComponent<ResourceSystem>().AddResource(ResourceType.Eye, save.amountEye);
    //        resourceSystem.GetComponent<ResourceSystem>().AddResource(ResourceType.Stone, save.amountStone);
    //        resourceSystem.GetComponent<ResourceSystem>().AddResource(ResourceType.Sand, save.amountSand);

    //        mixingSystem.GetComponent<MixingSystem>().ChangeCauldron(save.cauldronId);

    //        if (!save.mainGame)
    //        {
    //            potionSystem.GetComponent<PotionSystem>().SetColor(0, (PotionColor)save.bottle3Color);
    //            potionSystem.GetComponent<PotionSystem>().SetColor(1, (PotionColor)save.bottle3Color);
    //        }
    //        else
    //        {
    //            potionSystem.GetComponent<PotionSystem>().SetColor(0, (PotionColor)save.bottle1Color);
    //            potionSystem.GetComponent<PotionSystem>().SetColor(1, (PotionColor)save.bottle2Color);
    //        }
    //        potionSystem.GetComponent<PotionSystem>().SetColor(2, (PotionColor)save.bottle3Color);
    //        potionSystem.GetComponent<PotionSystem>().SetColor(3, (PotionColor)save.bottle4Color);
    //        potionSystem.GetComponent<PotionSystem>().SetColor(4, (PotionColor)save.bottle5Color);
    //        potionSystem.GetComponent<PotionSystem>().SetColor(5, (PotionColor)save.bottle6Color);
    //        potionSystem.GetComponent<PotionSystem>().SetColor(6, (PotionColor)save.bottle7Color);
    //        potionSystem.GetComponent<PotionSystem>().SetColor(7, (PotionColor)save.bottle8Color);

    //        if (!save.mainGame)
    //        {
    //            potionSystem.GetComponent<PotionSystem>().SetEffect(0, (PotionEffect)save.bottle3Effect);
    //            potionSystem.GetComponent<PotionSystem>().SetEffect(1, (PotionEffect)save.bottle3Effect);
    //        }
    //        else
    //        {
    //            potionSystem.GetComponent<PotionSystem>().SetEffect(0, (PotionEffect)save.bottle1Effect);
    //            potionSystem.GetComponent<PotionSystem>().SetEffect(1, (PotionEffect)save.bottle2Effect);
    //        }
    //        potionSystem.GetComponent<PotionSystem>().SetEffect(2, (PotionEffect)save.bottle3Effect);
    //        potionSystem.GetComponent<PotionSystem>().SetEffect(3, (PotionEffect)save.bottle4Effect);
    //        potionSystem.GetComponent<PotionSystem>().SetEffect(4, (PotionEffect)save.bottle5Effect);
    //        potionSystem.GetComponent<PotionSystem>().SetEffect(5, (PotionEffect)save.bottle6Effect);
    //        potionSystem.GetComponent<PotionSystem>().SetEffect(6, (PotionEffect)save.bottle7Effect);
    //        potionSystem.GetComponent<PotionSystem>().SetEffect(7, (PotionEffect)save.bottle8Effect);

    //        guildSystem.GetComponent<GuildSystemv1>().repWarriors = save.repWarriors;
    //        guildSystem.GetComponent<GuildSystemv1>().repBandits = save.repBandits;
    //        guildSystem.GetComponent<GuildSystemv1>().repPriests = save.repPriests;
    //        guildSystem.GetComponent<GuildSystemv1>().repMagicians = save.repMagicians;

    //        recipes.GetComponent<RecipesMenu>().addedArr = save.added;

    //        recipes.GetComponent<RecipesMenu>().pass = true;
    //        for (int i = 0; i < recipes.GetComponent<RecipesMenu>().addedArr.Length; i++)
    //            if (recipes.GetComponent<RecipesMenu>().addedArr[i])
    //                recipes.GetComponent<RecipesMenu>().AddPotionData(recipes.GetComponent<RecipesMenu>().potions[i]);
    //        recipes.GetComponent<RecipesMenu>().pass = false;

    //        if (save.mainGame)
    //            bottles.GetComponent<Bottles>().bottleUsage = save.bottleUsage;
    //        else
    //            bottles.GetComponent<Bottles>().bottleUsage = new bool[8] { false, false, false, false, false, false, false, false };

    //        UIControls.GetComponent<Tutorial>().mainGame = save.mainGame;
    //        UIControls.GetComponent<Tutorial>().helpShown = save.helpShown;

    //        guildSystem.GetComponent<QuestsSystem>().firstQuest = save.firstQuest;
    //        guildSystem.GetComponent<QuestsSystem>().firstAmount = save.firstAmount;
    //    }

    //    else
    //    {
    //        guildSystem.GetComponent<GuildSystemv1>().repWarriors = settings.rep;
    //        guildSystem.GetComponent<GuildSystemv1>().repBandits = settings.rep;
    //        guildSystem.GetComponent<GuildSystemv1>().repPriests = settings.rep;
    //        guildSystem.GetComponent<GuildSystemv1>().repMagicians = settings.rep;

    //        moneySystem.GetComponent<MoneySystem>().money = settings.money;

    //        recipes.GetComponent<RecipesMenu>().pass = false;
    //    }
    //}

    //IEnumerator AutoSaveDelay()
    //{
    //    yield return new WaitForSeconds(1);
    //    SaveGameSystem.SaveGame(moneySystem.GetComponent<MoneySystem>(), bottles.GetComponent<Bottles>(), moneySystem.GetComponent<ShopSystem>(), resourceSystem.GetComponent<Fuel>(),
    //        resourceSystem.GetComponent<ResourceSystem>(), mixingSystem.GetComponent<MixingSystem>(),
    //        potionSystem.GetComponent<PotionSystem>(), guildSystem.GetComponent<GuildSystemv1>(), recipes.GetComponent<RecipesMenu>(), UIControls.GetComponent<Tutorial>(), guildSystem.GetComponent<QuestsSystem>());
    //    StartCoroutine(AutoSaveDelay());
    //}
}
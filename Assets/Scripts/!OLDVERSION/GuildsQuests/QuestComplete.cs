using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class QuestComplete : MonoBehaviour
{
    public UnityEngine.GameObject guildSystem;
    public UnityEngine.GameObject potionSystem;
    public UnityEngine.GameObject moneySystem;
    public UnityEngine.GameObject resourceSystem;
    public UnityEngine.GameObject UIControls;
    public UnityEngine.GameObject coinDrop;
    public UnityEngine.GameObject coinTarget;
    public PotionColor potionColor;
    public PotionEffect potionEffect;
    public bool haveQuest = false;
    public Settings settings;

    public Image warriorsIngredient1;
    public Image warriorsIngredient2;
    public Image banditsIngredient1;
    public Image banditsIngredient2;
    public Image priestsIngredient1;
    public Image priestsIngredient2;
    public Image magiciansIngredient1;
    public Image magiciansIngredient2;

    public AudioClip completeTask;

    private int reward;
    private UnityEngine.GameObject[] curCoins = new UnityEngine.GameObject[5];

    public void NewQuest(PotionColor _potionColor, PotionEffect _potionEffect, int _reward)
    {
        potionColor = _potionColor;
        potionEffect = _potionEffect;
        haveQuest = true;
        reward = _reward;
    }

    public void EndQuest()
    {
        haveQuest = false;
    }

    private void DestroyCoin()
    {
        foreach (var item in curCoins)
        {
            Destroy(item);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bottle") && haveQuest)
        {
            if (collision.GetComponent<BottlePotion>().potionColor == potionColor && collision.GetComponent<BottlePotion>().potionEffect == potionEffect) 
            {
                GetComponent<SpriteRenderer>().enabled = false;

                warriorsIngredient1.gameObject.SetActive(false);
                warriorsIngredient2.gameObject.SetActive(false);
                banditsIngredient1.gameObject.SetActive(false);
                banditsIngredient2.gameObject.SetActive(false);
                priestsIngredient1.gameObject.SetActive(false);
                priestsIngredient2.gameObject.SetActive(false);
                magiciansIngredient1.gameObject.SetActive(false);
                magiciansIngredient2.gameObject.SetActive(false);

                if (guildSystem.GetComponent<QuestsSystem>().firstAmount < 6 && !UIControls.GetComponent<Tutorial>().mainGame)
                    guildSystem.GetComponent<QuestsSystem>().GiveFirst();
                else if (guildSystem.GetComponent<QuestsSystem>().firstQuest)
                {
                    UIControls.GetComponent<Tutorial>().ToggleMessage("Не забывай экспериментировать с ресурсами чтобы получать новые рецепты зелий");
                    UIControls.GetComponent<Tutorial>().mainGame = true;
                    guildSystem.GetComponent<QuestsSystem>().firstQuest = false;
                }

                if (guildSystem.GetComponent<QuestsSystem>().questTime > settings.questLimit && guildSystem.GetComponent<QuestsSystem>().questStep == settings.questSpeedupStep)
                {
                    guildSystem.GetComponent<QuestsSystem>().questTime -= settings.questSpeedup;
                    guildSystem.GetComponent<QuestsSystem>().questStep = 0;
                }

                guildSystem.GetComponent<QuestsSystem>().questStep++;       //квесты разбиты на шаги? 

                collision.GetComponent<BottlePotion>().justGiven = true;
                collision.transform.Find("Water").GetComponent<SpriteRenderer>().color = Color.white;
                collision.transform.Find("Water").gameObject.SetActive(false);
                collision.GetComponent<BottlePotion>().potionColor = PotionColor.Empty;
                collision.GetComponent<BottlePotion>().potionEffect = PotionEffect.Empty;

                switch (collision.tag)      //Выбор бутылки? 
                {
                    case "Bottle1":
                        potionSystem.GetComponent<PotionSystem>().SetColor(0, PotionColor.Empty);
                        potionSystem.GetComponent<PotionSystem>().SetEffect(0, PotionEffect.Empty);
                        break;

                    case "Bottle2":
                        potionSystem.GetComponent<PotionSystem>().SetColor(1, PotionColor.Empty);
                        potionSystem.GetComponent<PotionSystem>().SetEffect(1, PotionEffect.Empty);
                        break;

                    case "Bottle3":
                        potionSystem.GetComponent<PotionSystem>().SetColor(2, PotionColor.Empty);
                        potionSystem.GetComponent<PotionSystem>().SetEffect(2, PotionEffect.Empty);
                        break;

                    case "Bottle4":
                        potionSystem.GetComponent<PotionSystem>().SetColor(3, PotionColor.Empty);
                        potionSystem.GetComponent<PotionSystem>().SetEffect(3, PotionEffect.Empty);
                        break;

                    case "Bottle5":
                        potionSystem.GetComponent<PotionSystem>().SetColor(4, PotionColor.Empty);
                        potionSystem.GetComponent<PotionSystem>().SetEffect(4, PotionEffect.Empty);
                        break;

                    case "Bottle6":
                        potionSystem.GetComponent<PotionSystem>().SetColor(5, PotionColor.Empty);
                        potionSystem.GetComponent<PotionSystem>().SetEffect(5, PotionEffect.Empty);
                        break;

                    case "Bottle7":
                        potionSystem.GetComponent<PotionSystem>().SetColor(6, PotionColor.Empty);
                        potionSystem.GetComponent<PotionSystem>().SetEffect(6, PotionEffect.Empty);
                        break;

                    case "Bottle8":
                        potionSystem.GetComponent<PotionSystem>().SetColor(7, PotionColor.Empty);
                        potionSystem.GetComponent<PotionSystem>().SetEffect(7, PotionEffect.Empty);
                        break;

                    default:
                        break;
                }

                haveQuest = false;

                switch (tag)        //выдача заказа в зависимости от типа гильдии
                {
                    case "Warrior":
                        moneySystem.GetComponent<MoneySystem>().AddMoney(reward);
                        for (int i = 0; i < 5; i++)
                        {
                            curCoins[i] = Instantiate(coinDrop, transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0), Quaternion.identity);
                            curCoins[i].transform.DOMove(coinTarget.transform.position, 1, false).SetEase(Ease.InOutBack, 0.5f).OnComplete(DestroyCoin);
                        }
                        GetComponent<AudioSource>().clip = completeTask;
                        GetComponent<AudioSource>().Play();

                        guildSystem.GetComponent<QuestsSystem>().StopQuest(Guild.Warriors);
                        if (!guildSystem.GetComponent<QuestsSystem>().firstQuest)
                        {
                            guildSystem.GetComponent<GuildSystemv1>().addRep(Guild.Warriors, settings.repReward);
                            guildSystem.GetComponent<GuildSystemv1>().addRep(Guild.Priests, settings.repChangeSec);
                            guildSystem.GetComponent<GuildSystemv1>().removeRep(Guild.Bandits, settings.repChangeSec);
                            guildSystem.GetComponent<GuildSystemv1>().removeRep(Guild.Magicians, settings.repChangeSec);
                        }
                        if (guildSystem.GetComponent<QuestsSystem>().delayWarriors > 0)
                            guildSystem.GetComponent<QuestsSystem>().delayWarriors--;
                        if (potionEffect != PotionEffect.Normal)
                            guildSystem.GetComponent<QuestsSystem>().delayWarriors = settings.questPenalty;
                        break;

                    case "Bandit":
                        if (Random.Range(0, 100) < settings.banditsX + guildSystem.GetComponent<GuildSystemv1>().GetRep(Guild.Bandits) / 100 * settings.banditsY)
                        {
                            moneySystem.GetComponent<MoneySystem>().AddMoney(reward);
                            for (int i = 0; i < 5; i++)
                            {
                                curCoins[i] = Instantiate(coinDrop, transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0), Quaternion.identity);
                                curCoins[i].transform.DOMove(coinTarget.transform.position, 1, false).SetEase(Ease.InOutBack, 0.5f).OnComplete(DestroyCoin);
                            }
                            GetComponent<AudioSource>().clip = completeTask;
                            GetComponent<AudioSource>().Play();
                        }

                        guildSystem.GetComponent<QuestsSystem>().StopQuest(Guild.Bandits);
                        if (!guildSystem.GetComponent<QuestsSystem>().firstQuest)
                        {
                            guildSystem.GetComponent<GuildSystemv1>().addRep(Guild.Bandits, settings.repReward);
                            guildSystem.GetComponent<GuildSystemv1>().addRep(Guild.Magicians, settings.repChangeSec);
                            guildSystem.GetComponent<GuildSystemv1>().removeRep(Guild.Warriors, settings.repChangeSec);
                            guildSystem.GetComponent<GuildSystemv1>().removeRep(Guild.Priests, settings.repChangeSec);
                        }
                        if (guildSystem.GetComponent<QuestsSystem>().delayBandits > 0)
                            guildSystem.GetComponent<QuestsSystem>().delayBandits--;
                        if (potionEffect != PotionEffect.Normal)
                            guildSystem.GetComponent<QuestsSystem>().delayBandits = settings.questPenalty;
                        break;

                    case "Priest":
                        if (Random.Range(0, 100) < settings.priestsX + guildSystem.GetComponent<GuildSystemv1>().GetRep(Guild.Priests) / 100 * settings.priestsY)
                            moneySystem.GetComponent<MoneySystem>().AddMoney(reward * 2);
                        else
                            moneySystem.GetComponent<MoneySystem>().AddMoney(reward);

                        for (int i = 0; i < 5; i++)
                        {
                            curCoins[i] = Instantiate(coinDrop, transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0), Quaternion.identity);
                            curCoins[i].transform.DOMove(coinTarget.transform.position, 1, false).SetEase(Ease.InOutBack, 0.5f).OnComplete(DestroyCoin);
                        }
                        GetComponent<AudioSource>().clip = completeTask;
                        GetComponent<AudioSource>().Play();

                        guildSystem.GetComponent<QuestsSystem>().StopQuest(Guild.Priests);
                        if (!guildSystem.GetComponent<QuestsSystem>().firstQuest)
                        {
                            guildSystem.GetComponent<GuildSystemv1>().addRep(Guild.Priests, settings.repReward);
                            guildSystem.GetComponent<GuildSystemv1>().addRep(Guild.Warriors, settings.repChangeSec);
                            guildSystem.GetComponent<GuildSystemv1>().removeRep(Guild.Magicians, settings.repChangeSec);
                            guildSystem.GetComponent<GuildSystemv1>().removeRep(Guild.Bandits, settings.repChangeSec);
                        }
                        if (guildSystem.GetComponent<QuestsSystem>().delayPriests > 0)
                            guildSystem.GetComponent<QuestsSystem>().delayPriests--;
                        if (potionEffect != PotionEffect.Normal)
                            guildSystem.GetComponent<QuestsSystem>().delayPriests = settings.questPenalty;
                        break;

                    case "Magician":
                        if (Random.Range(0, 100) < settings.magiciansX + guildSystem.GetComponent<GuildSystemv1>().GetRep(Guild.Magicians) / 100 * settings.magiciansY)
                        {
                            switch (Random.Range(0,3))
                            {
                                case 0:
                                    resourceSystem.GetComponent<ResourceSystem>().AddResource(ResourceType.Red, 1);
                                    break;
                                case 1:
                                    resourceSystem.GetComponent<ResourceSystem>().AddResource(ResourceType.Blue, 1);
                                    break;
                                case 2:
                                    resourceSystem.GetComponent<ResourceSystem>().AddResource(ResourceType.Yellow, 1);
                                    break;
                                case 3:
                                    resourceSystem.GetComponent<ResourceSystem>().AddResource(ResourceType.White, 1);
                                    break;
                                default:
                                    break;
                            }
                        }
                        moneySystem.GetComponent<MoneySystem>().AddMoney(reward);
                        for (int i = 0; i < 5; i++)
                        {
                            curCoins[i] = Instantiate(coinDrop, transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0), Quaternion.identity);
                            curCoins[i].transform.DOMove(coinTarget.transform.position, 1, false).SetEase(Ease.InOutBack, 0.5f).OnComplete(DestroyCoin);
                        }
                        GetComponent<AudioSource>().clip = completeTask;
                        GetComponent<AudioSource>().Play();

                        guildSystem.GetComponent<QuestsSystem>().StopQuest(Guild.Magicians);
                        if (!guildSystem.GetComponent<QuestsSystem>().firstQuest)
                        {
                            guildSystem.GetComponent<GuildSystemv1>().addRep(Guild.Magicians, settings.repReward);
                            guildSystem.GetComponent<GuildSystemv1>().addRep(Guild.Bandits, settings.repChangeSec);
                            guildSystem.GetComponent<GuildSystemv1>().removeRep(Guild.Priests, settings.repChangeSec);
                            guildSystem.GetComponent<GuildSystemv1>().removeRep(Guild.Warriors, settings.repChangeSec);
                        }
                        if (guildSystem.GetComponent<QuestsSystem>().delayMagicians > 0)
                            guildSystem.GetComponent<QuestsSystem>().delayMagicians--;
                        if (potionEffect != PotionEffect.Normal)
                            guildSystem.GetComponent<QuestsSystem>().delayMagicians = settings.questPenalty;
                        break;

                    default:
                        break;
                }                
            }
        }
    }
}
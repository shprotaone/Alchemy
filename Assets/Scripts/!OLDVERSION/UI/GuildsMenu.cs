using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuildsMenu : MonoBehaviour
{
    public UnityEngine.GameObject guildSystem;
    public UnityEngine.GameObject moneySystem;
    public UnityEngine.GameObject resourceSystem;

    public TextMeshProUGUI textLadan;
    public TextMeshProUGUI textEye;
    public TextMeshProUGUI textStone;
    public TextMeshProUGUI textSand;

    public TextMeshProUGUI textRepWarriors;
    public TextMeshProUGUI textRepBandits;
    public TextMeshProUGUI textRepPriests;
    public TextMeshProUGUI textRepMagicians;

    public Slider repWarriors;
    public Slider repBandits;
    public Slider repPriests;
    public Slider repMagicians;
    public Slider repWarriorsGuilds;
    public Slider repBanditsGuilds;
    public Slider repPriestsGuilds;
    public Slider repMagiciansGuilds;

    public Button buyLadan;
    public Button buyEye;
    public Button buySand;
    public Button buyStone;

    public Button buyRepWarriors;
    public Button buyRepBandits;
    public Button buyRepPriests;
    public Button buyRepMagicians;

    public Settings settings;

    private void OnEnable()
    {
        textLadan.text = "Купить святой ладан: " + settings.costRare.ToString();
        textEye.text = "Купить глаз вурдалака: " + settings.costRare.ToString();
        textSand.text = "Купить пустынный песок: " + settings.costRare.ToString();
        textStone.text = "Купить волшебный камень: " + settings.costRare.ToString();

        textRepWarriors.text = "Купить репутацию: " + settings.costRep.ToString();
        textRepBandits.text = "Купить репутацию: " + settings.costRep.ToString();
        textRepPriests.text = "Купить репутацию: " + settings.costRep.ToString();
        textRepMagicians.text = "Купить репутацию: " + settings.costRep.ToString();

        repWarriorsGuilds.value = repWarriors.value;
        repBanditsGuilds.value = repBandits.value;
        repPriestsGuilds.value = repPriests.value;
        repMagiciansGuilds.value = repMagicians.value;

        if (repWarriors.value >= settings.repRare && moneySystem.GetComponent<MoneySystem>().GetMoney() > settings.costRare && resourceSystem.GetComponent<ResourceSystem>().GetAmount(ResourceType.Eye) < settings.rareMax)
            buyEye.interactable = true;
        else
            buyEye.interactable = false;

        if (repBandits.value >= settings.repRare && moneySystem.GetComponent<MoneySystem>().GetMoney() > settings.costRare && resourceSystem.GetComponent<ResourceSystem>().GetAmount(ResourceType.Sand) < settings.rareMax)
            buySand.interactable = true;
        else
            buySand.interactable = false;

        if (repPriests.value >= settings.repRare && moneySystem.GetComponent<MoneySystem>().GetMoney() > settings.costRare && resourceSystem.GetComponent<ResourceSystem>().GetAmount(ResourceType.Ladan) < settings.rareMax)
            buyLadan.interactable = true;
        else
            buyLadan.interactable = false;

        if (repMagicians.value >= settings.repRare && moneySystem.GetComponent<MoneySystem>().GetMoney() > settings.costRare && resourceSystem.GetComponent<ResourceSystem>().GetAmount(ResourceType.Stone) < settings.rareMax)
            buyStone.interactable = true;
        else
            buyStone.interactable = false;
    }

    public void BuyRep(int guildID)
    {
        if (moneySystem.GetComponent<MoneySystem>().GetMoney() >= settings.costRep)
        {
            moneySystem.GetComponent<MoneySystem>().SpendMoney(settings.costRep);

            if (moneySystem.GetComponent<MoneySystem>().GetMoney() < settings.costRep)
            {
                buyRepWarriors.interactable = false;
                buyRepBandits.interactable = false;
                buyRepPriests.interactable = false;
                buyRepMagicians.interactable = false;
            }

            if (moneySystem.GetComponent<MoneySystem>().GetMoney() < settings.costRare)
            {
                buyEye.interactable = false;
                buySand.interactable = false;
                buyLadan.interactable = false;
                buyStone.interactable = false;
            }

            switch (guildID)
            {
                case 0:
                    guildSystem.GetComponent<GuildSystemv1>().addRep(Guild.Warriors, settings.repAdd);
                    repWarriorsGuilds.value = repWarriors.value;
                    if (repWarriors.value >= settings.repLimit)
                        buyRepWarriors.interactable = false;
                    break;

                case 1:
                    guildSystem.GetComponent<GuildSystemv1>().addRep(Guild.Bandits, settings.repAdd);
                    repBanditsGuilds.value = repBandits.value;
                    if (repBandits.value >= settings.repLimit)
                        buyRepBandits.interactable = false;
                    break;

                case 2:
                    guildSystem.GetComponent<GuildSystemv1>().addRep(Guild.Priests, settings.repAdd);
                    repPriestsGuilds.value = repPriests.value;
                    if (repPriests.value >= settings.repLimit)
                        buyRepPriests.interactable = false;
                    break;

                case 3:
                    guildSystem.GetComponent<GuildSystemv1>().addRep(Guild.Magicians, settings.repAdd);
                    repMagiciansGuilds.value = repMagicians.value;
                    if (repMagicians.value >= settings.repLimit)
                        buyRepMagicians.interactable = false;
                    break;

                default:
                    break;
            }
        }
    }

    public void BuyRareResource(int resID)
    {
        if (moneySystem.GetComponent<MoneySystem>().GetMoney() >= settings.costRare)
        {
            moneySystem.GetComponent<MoneySystem>().SpendMoney(settings.costRare);

            if (moneySystem.GetComponent<MoneySystem>().GetMoney() < settings.costRep)
            {
                buyRepWarriors.interactable = false;
                buyRepBandits.interactable = false;
                buyRepPriests.interactable = false;
                buyRepMagicians.interactable = false;
            }

            if (moneySystem.GetComponent<MoneySystem>().GetMoney() < settings.costRare)
            {
                buyEye.interactable = false;
                buySand.interactable = false;
                buyLadan.interactable = false;
                buyStone.interactable = false;
            }

            switch (resID)
            {
                case 0:
                    if (resourceSystem.GetComponent<ResourceSystem>().GetAmount(ResourceType.Eye) < settings.rareMax)
                    {
                        resourceSystem.GetComponent<ResourceSystem>().AddResource(ResourceType.Eye, 1);
                        if (resourceSystem.GetComponent<ResourceSystem>().GetAmount(ResourceType.Eye) == settings.rareMax)
                            buyEye.interactable = false;
                    }
                    break;

                case 1:
                    if (resourceSystem.GetComponent<ResourceSystem>().GetAmount(ResourceType.Sand) < settings.rareMax)
                    {
                        resourceSystem.GetComponent<ResourceSystem>().AddResource(ResourceType.Sand, 1);
                        if (resourceSystem.GetComponent<ResourceSystem>().GetAmount(ResourceType.Sand) == settings.rareMax)
                            buySand.interactable = false;
                    }
                    break;

                case 2:
                    if (resourceSystem.GetComponent<ResourceSystem>().GetAmount(ResourceType.Ladan) < settings.rareMax)
                    {
                        resourceSystem.GetComponent<ResourceSystem>().AddResource(ResourceType.Ladan, 1);
                        if (resourceSystem.GetComponent<ResourceSystem>().GetAmount(ResourceType.Ladan) == settings.rareMax)
                            buyLadan.interactable = false;
                    }
                    break;

                case 3:
                    if (resourceSystem.GetComponent<ResourceSystem>().GetAmount(ResourceType.Stone) < settings.rareMax)
                    {
                        resourceSystem.GetComponent<ResourceSystem>().AddResource(ResourceType.Stone, 1);
                        if (resourceSystem.GetComponent<ResourceSystem>().GetAmount(ResourceType.Stone) == settings.rareMax)
                            buyStone.interactable = false;
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Guild
{
    Warriors,
    Bandits,
    Priests,
    Magicians
}

public class GuildSystemv1 : MonoBehaviour
{
    public Slider sliderWarriors;
    public Slider sliderBandits;
    public Slider sliderPriests;
    public Slider sliderMagicians;

    public Button buyRepWarriors;
    public Button buyRepBandits;
    public Button buyRepPriests;
    public Button buyRepMagicians;
    public Button helpButton;

    public int repWarriors;
    public int repBandits;
    public int repPriests;
    public int repMagicians;

    public Settings settings;

    public UnityEngine.GameObject UIControls;
    public UnityEngine.GameObject moneySystem;

    private void Start()
    {
        sliderWarriors.value = repWarriors;
        sliderBandits.value = repBandits;
        sliderPriests.value = repPriests;
        sliderMagicians.value = repMagicians;

        if (repWarriors <= settings.repMin)
            buyRepWarriors.interactable = true;
        if (repBandits <= settings.repMin)
            buyRepBandits.interactable = true;
        if (repPriests <= settings.repMin)
            buyRepPriests.interactable = true;
        if (repMagicians <= settings.repMin)
            buyRepMagicians.interactable = true;
    }

    public void addRep(Guild guild, int amount)
    {
        switch (guild)
        {
            case Guild.Warriors:
                repWarriors += amount;
                if (repWarriors > 100)
                    repWarriors = 100;
                if (repWarriors > 75 && moneySystem.GetComponent<MoneySystem>().GetMoney() >= settings.costRare && !helpButton.interactable)
                {
                    UIControls.GetComponent<Tutorial>().helpStep = 2;
                    UIControls.GetComponent<Tutorial>().GetHelp();
                }
                sliderWarriors.value = repWarriors;
                break;

            case Guild.Bandits:
                repBandits += amount;
                if (repBandits > 100)
                    repBandits = 100;
                if (repBandits > 75 && moneySystem.GetComponent<MoneySystem>().GetMoney() >= settings.costRare && !helpButton.interactable)
                {
                    UIControls.GetComponent<Tutorial>().helpStep = 2;
                    UIControls.GetComponent<Tutorial>().GetHelp();
                }
                sliderBandits.value = repBandits;
                break;

            case Guild.Priests:
                repPriests += amount;
                if (repPriests > 100)
                    repPriests = 100;
                if (repPriests > 75 && moneySystem.GetComponent<MoneySystem>().GetMoney() >= settings.costRare && !helpButton.interactable)
                {
                    UIControls.GetComponent<Tutorial>().helpStep = 2;
                    UIControls.GetComponent<Tutorial>().GetHelp();
                }
                sliderPriests.value = repPriests;
                break;

            case Guild.Magicians:
                repMagicians += amount;
                if (repMagicians > 100)
                    repMagicians = 100;
                if (repMagicians > 75 && moneySystem.GetComponent<MoneySystem>().GetMoney() >= settings.costRare && !helpButton.interactable)
                {
                    UIControls.GetComponent<Tutorial>().helpStep = 2;
                    UIControls.GetComponent<Tutorial>().GetHelp();
                }
                sliderMagicians.value = repMagicians;
                break;

            default:
                break;
        }
    }

    public void removeRep(Guild guild, int amount)
    {
        switch (guild)
        {
            case Guild.Warriors:
                repWarriors -= amount;
                if (repWarriors < 0)
                    repWarriors = 0;
                sliderWarriors.value = repWarriors;
                if (repWarriors <= settings.repMin && moneySystem.GetComponent<MoneySystem>().GetMoney() >= settings.costRep)
                {
                    buyRepWarriors.interactable = true;
                    if (!helpButton.interactable)
                    {
                        UIControls.GetComponent<Tutorial>().helpStep = 1;
                        UIControls.GetComponent<Tutorial>().GetHelp();
                    }
                }
                break;

            case Guild.Bandits:
                repBandits -= amount;
                if (repBandits < 0)
                    repBandits = 0;
                sliderBandits.value = repBandits;
                if (repBandits <= settings.repMin && moneySystem.GetComponent<MoneySystem>().GetMoney() >= settings.costRep)
                {
                    buyRepBandits.interactable = true;
                    if (!helpButton.interactable)
                    {
                        UIControls.GetComponent<Tutorial>().helpStep = 1;
                        UIControls.GetComponent<Tutorial>().GetHelp();
                    }
                }
                break;

            case Guild.Priests:
                repPriests -= amount;
                if (repPriests < 0)
                    repPriests = 0;
                sliderPriests.value = repPriests;
                if (repPriests <= settings.repMin && moneySystem.GetComponent<MoneySystem>().GetMoney() >= settings.costRep)
                {
                    buyRepPriests.interactable = true;
                    if (!helpButton.interactable)
                    {
                        UIControls.GetComponent<Tutorial>().helpStep = 1;
                        UIControls.GetComponent<Tutorial>().GetHelp();
                    }
                }
                break;

            case Guild.Magicians:
                repMagicians -= amount;
                if (repMagicians < 0)
                    repMagicians = 0;
                sliderMagicians.value = repMagicians;
                if (repMagicians <= settings.repMin && moneySystem.GetComponent<MoneySystem>().GetMoney() >= settings.costRep)
                {
                    buyRepMagicians.interactable = true;
                    if (!helpButton.interactable)
                    {
                        UIControls.GetComponent<Tutorial>().helpStep = 1;
                        UIControls.GetComponent<Tutorial>().GetHelp();
                    }
                }
                break;

            default:
                break;
        }
    }

    public float GetRep(Guild guild)
    {
        switch (guild)
        {
            case Guild.Warriors:
                return repWarriors;

            case Guild.Bandits:
                return repBandits;

            case Guild.Priests:
                return repPriests;

            case Guild.Magicians:
                return repMagicians;

            default:
                return 0;
        }
    }

    public int CalcExtraTime()
    {
        int time = 0;

        if (repWarriors < 40)
            time += (40 - repWarriors) / 5;

        if (repBandits < 40)
            time += (40 - repBandits) / 5;

        if (repMagicians < 40)
            time += (40 - repMagicians) / 5;

        if (repPriests < 40)
            time += (40 - repPriests) / 5;

        return time;
    }
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    public GameObject resourceSystem;
    public GameObject bottles;
    public GameObject cauldron;
    public GameObject UIControls;

    public Button buttonBuyBottle;
    public TextMeshProUGUI textBuyBottle;
    public Button buttonBuyCauldron;
    public TextMeshProUGUI textBuyCauldron;

    public AudioClip buyResource;
    public AudioClip buyUpgrade;

    public int bottleCost = 500;
    public int cauldronCost = 3000;

    private void Start()
    {
        textBuyBottle.text = "Купить бутылку: " + bottleCost;
        if (bottles.GetComponent<Bottles>().GetBottleCount() == 8)  //Информирует о том, что бутылок максимальное количество
        {
            buttonBuyBottle.interactable = false;
            textBuyBottle.text = "Максимум бутылок";
        }

        switch (cauldron.GetComponent<MixingSystem>().GetCauldron())    //выбор текущего котла
        {
            case 0:
                textBuyCauldron.text = "Купить нормальный котел: 3000";
                break;

            case 1:
                textBuyCauldron.text = "Купить хороший котел: 6000";
                break;

            case 2:
                buttonBuyCauldron.interactable = false;
                textBuyCauldron.text = "Куплен лучший котел";
                break;

            default:
                break;
        }
    }

    public void BuyResource(int resNumber)
    {
        switch ((ResourceType)resNumber)
        {
            case ResourceType.Red:
                if (GetComponent<MoneySystem>().GetMoney() >= 100)
                {
                    GetComponent<AudioSource>().clip = buyResource;
                    GetComponent<AudioSource>().Play();
                    GetComponent<MoneySystem>().SpendMoney(100);
                    resourceSystem.GetComponent<ResourceSystem>().AddResource(ResourceType.Red, 1);
                }
                break;

            case ResourceType.Blue:
                if (GetComponent<MoneySystem>().GetMoney() >= 100)
                {
                    GetComponent<AudioSource>().clip = buyResource;
                    GetComponent<AudioSource>().Play();
                    GetComponent<MoneySystem>().SpendMoney(100);
                    resourceSystem.GetComponent<ResourceSystem>().AddResource(ResourceType.Blue, 1);
                }
                break;

            case ResourceType.Yellow:
                if (GetComponent<MoneySystem>().GetMoney() >= 100)
                {
                    GetComponent<AudioSource>().clip = buyResource;
                    GetComponent<AudioSource>().Play();
                    GetComponent<MoneySystem>().SpendMoney(100);
                    resourceSystem.GetComponent<ResourceSystem>().AddResource(ResourceType.Yellow, 1);
                }
                break;

            case ResourceType.White:
                if (GetComponent<MoneySystem>().GetMoney() >= 100)
                {
                    GetComponent<AudioSource>().clip = buyResource;
                    GetComponent<AudioSource>().Play();
                    GetComponent<MoneySystem>().SpendMoney(100);
                    resourceSystem.GetComponent<ResourceSystem>().AddResource(ResourceType.White, 1);
                }
                break;

            default:
                break;
        }

        if (!UIControls.GetComponent<Tutorial>().mainGame && resourceSystem.GetComponent<ResourceSystem>().GetAmount(ResourceType.Red) == 1 && resourceSystem.GetComponent<ResourceSystem>().GetAmount(ResourceType.Blue) == 1)
        {
            UIControls.GetComponent<Tutorial>().canOpenShop = false;
            UIControls.GetComponent<Tutorial>().ToggleMessage("Все готово, возвращаемся на рабочее место.");
            UIControls.GetComponent<Popups>().popupOpenID = 0;
            UIControls.GetComponent<Popups>().popupOpen = false;
            UIControls.GetComponent<Popups>().popupShop.gameObject.SetActive(false);
        }
    }

    public void BuyResourceX5(int resNumber)
    {
        switch ((ResourceType)resNumber)
        {
            case ResourceType.Red:
                if (GetComponent<MoneySystem>().GetMoney() >= 500)
                {
                    GetComponent<AudioSource>().clip = buyResource;
                    GetComponent<AudioSource>().Play();
                    GetComponent<MoneySystem>().SpendMoney(500);
                    resourceSystem.GetComponent<ResourceSystem>().AddResource(ResourceType.Red, 5);
                }
                break;

            case ResourceType.Blue:
                if (GetComponent<MoneySystem>().GetMoney() >= 500)
                {
                    GetComponent<AudioSource>().clip = buyResource;
                    GetComponent<AudioSource>().Play();
                    GetComponent<MoneySystem>().SpendMoney(500);
                    resourceSystem.GetComponent<ResourceSystem>().AddResource(ResourceType.Blue, 5);
                }
                break;

            case ResourceType.Yellow:
                if (GetComponent<MoneySystem>().GetMoney() >= 500)
                {
                    GetComponent<AudioSource>().clip = buyResource;
                    GetComponent<AudioSource>().Play();
                    GetComponent<MoneySystem>().SpendMoney(500);
                    resourceSystem.GetComponent<ResourceSystem>().AddResource(ResourceType.Yellow, 5);
                }
                break;

            case ResourceType.White:
                if (GetComponent<MoneySystem>().GetMoney() >= 500)
                {
                    GetComponent<AudioSource>().clip = buyResource;
                    GetComponent<AudioSource>().Play();
                    GetComponent<MoneySystem>().SpendMoney(500);
                    resourceSystem.GetComponent<ResourceSystem>().AddResource(ResourceType.White, 5);
                }
                break;

            default:
                break;
        }
    }

    public void BuyResourceX10(int resNumber)
    {
        switch ((ResourceType)resNumber)
        {
            case ResourceType.Red:
                if (GetComponent<MoneySystem>().GetMoney() >= 1000)
                {
                    GetComponent<AudioSource>().clip = buyResource;
                    GetComponent<AudioSource>().Play();
                    GetComponent<MoneySystem>().SpendMoney(1000);
                    resourceSystem.GetComponent<ResourceSystem>().AddResource(ResourceType.Red, 10);
                }
                break;

            case ResourceType.Blue:
                if (GetComponent<MoneySystem>().GetMoney() >= 1000)
                {
                    GetComponent<AudioSource>().clip = buyResource;
                    GetComponent<AudioSource>().Play();
                    GetComponent<MoneySystem>().SpendMoney(1000);
                    resourceSystem.GetComponent<ResourceSystem>().AddResource(ResourceType.Blue, 10);
                }
                break;

            case ResourceType.Yellow:
                if (GetComponent<MoneySystem>().GetMoney() >= 1000)
                {
                    GetComponent<AudioSource>().clip = buyResource;
                    GetComponent<AudioSource>().Play();
                    GetComponent<MoneySystem>().SpendMoney(1000);
                    resourceSystem.GetComponent<ResourceSystem>().AddResource(ResourceType.Yellow, 10);
                }
                break;

            case ResourceType.White:
                if (GetComponent<MoneySystem>().GetMoney() >= 1000)
                {
                    GetComponent<AudioSource>().clip = buyResource;
                    GetComponent<AudioSource>().Play();
                    GetComponent<MoneySystem>().SpendMoney(1000);
                    resourceSystem.GetComponent<ResourceSystem>().AddResource(ResourceType.White, 10);
                }
                break;

            default:
                break;
        }
    }

    public void BuyCauldron()
    {
        switch (cauldron.GetComponent<MixingSystem>().GetCauldron())
        {
            case 0:
                if (GetComponent<MoneySystem>().GetMoney() >= 3000)
                {
                    GetComponent<AudioSource>().clip = buyUpgrade;
                    GetComponent<AudioSource>().Play();
                    GetComponent<MoneySystem>().SpendMoney(3000);
                    cauldron.GetComponent<MixingSystem>().ChangeCauldron(1);
                    textBuyCauldron.text = "Купить хороший котел: 6000";
                    cauldronCost = 6000;
                }
                break;

            case 1:
                if (GetComponent<MoneySystem>().GetMoney() >= 6000)
                {
                    GetComponent<AudioSource>().clip = buyUpgrade;
                    GetComponent<AudioSource>().Play();
                    GetComponent<MoneySystem>().SpendMoney(6000);
                    cauldron.GetComponent<MixingSystem>().ChangeCauldron(2);
                    buttonBuyCauldron.interactable = false;
                    textBuyCauldron.text = "Куплен лучший котел";
                }
                break;

            default:
                break;
        }
    }

    public void BuyBottle()
    {
        if (GetComponent<MoneySystem>().GetMoney() >= bottleCost)
        {
            GetComponent<AudioSource>().clip = buyUpgrade;
            GetComponent<AudioSource>().Play();
            bottles.GetComponent<Bottles>().AddBottle();
            GetComponent<MoneySystem>().SpendMoney(bottleCost);
            if (bottles.GetComponent<Bottles>().GetBottleCount() == 8)
            {
                buttonBuyBottle.interactable = false;
                textBuyBottle.text = "Максимум бутылок";
            }
            else
            {
                bottleCost *= 2;
                textBuyBottle.text = "Купить бутылку: " + bottleCost;
            }
        }
    }

    public void BuyFuel()
    {
        if (GetComponent<MoneySystem>().GetMoney() >= 150)
        {
            GetComponent<AudioSource>().clip = buyUpgrade;
            GetComponent<AudioSource>().Play();
            resourceSystem.GetComponent<Fuel>().AddFuel();
            GetComponent<MoneySystem>().SpendMoney(150);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    public GameObject moneySystem;
    public GameObject cauldron;
    public GameObject bottles;
    public GameObject UIControls;
    public GameObject resourceSystem;

    public Button[] buyResX1;
    public Button[] buyResX5;
    public Button[] buyResX10;
    public Button buyBottle;
    public Button buyFuel;
    public Button buyCauldron;

    private void Update()
    {
        if (!UIControls.GetComponent<Tutorial>().mainTutorial)
        {
            buyFuel.interactable = false;
            if (resourceSystem.GetComponent<ResourceSystem>().GetAmount(ResourceType.Red) == 1) buyResX1[0].interactable = false;
            if (resourceSystem.GetComponent<ResourceSystem>().GetAmount(ResourceType.Blue) == 1) buyResX1[1].interactable = false;
            return;
        }

        if (moneySystem.GetComponent<MoneySystem>().GetMoney() < 100)
        {
            foreach (var item in buyResX1)
                item.interactable = false;
        }
        else
        {
            foreach (var item in buyResX1)
                item.interactable = true;
        }

        if (moneySystem.GetComponent<MoneySystem>().GetMoney() < 500)
        {
            foreach (var item in buyResX5)
                item.interactable = false;
        }
        else
        {
            foreach (var item in buyResX5)
                item.interactable = true;
        }

        if (moneySystem.GetComponent<MoneySystem>().GetMoney() < 1000)
        {
            foreach (var item in buyResX10)
                item.interactable = false;
        }
        else
        {
            foreach (var item in buyResX10)
                item.interactable = true;
        }

        if (moneySystem.GetComponent<MoneySystem>().GetMoney() < 150)
            buyFuel.interactable = false;
        else
            buyFuel.interactable = true;

        if (moneySystem.GetComponent<MoneySystem>().GetMoney() < moneySystem.GetComponent<ShopSystem>().bottleCost || bottles.GetComponent<Bottles>().GetBottleCount() == 8)
            buyBottle.interactable = false;
        else
            buyBottle.interactable = true;

        if (cauldron.GetComponent<MixingSystem>().GetCauldron() != 2)
        {
            if (moneySystem.GetComponent<MoneySystem>().GetMoney() < moneySystem.GetComponent<ShopSystem>().cauldronCost)
                buyCauldron.interactable = false;
            else
                buyCauldron.interactable = true;
        }        
    }
}
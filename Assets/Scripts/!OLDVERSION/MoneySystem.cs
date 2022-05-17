using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneySystem : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI moneyTextShop;
    public TextMeshProUGUI moneyTextGuilds;

    public GameObject UIControls;
    public GameObject mixingSystem;

    public Button helpButton;

    public int money = 500;

    private void Start()
    {
        moneyTextShop.text = money.ToString();
        moneyTextGuilds.text = "Деньги: " + money.ToString();
        moneyText.text = money.ToString();
    }

    public void AddMoney(int amount)
    {
        money += amount;
        moneyTextShop.text = money.ToString();
        moneyTextGuilds.text = "Деньги: " + money.ToString();
        moneyText.text = money.ToString();
        if (money > 4000 && mixingSystem.GetComponent<MixingSystem>().cauldronId == 0 && !helpButton.interactable)
        {
            UIControls.GetComponent<Tutorial>().helpStep = 3;
            UIControls.GetComponent<Tutorial>().GetHelp();
        }
    }

    public void SpendMoney(int amount)
    {
        money -= amount;
        moneyTextShop.text = money.ToString();
        moneyTextGuilds.text = "Деньги: " + money.ToString();
        moneyText.text = money.ToString();
    }

    public int GetMoney()
    {
        return money;
    }
}
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Button helpButton;
    public GameObject[] helpScreens;
    public GameObject message;
    public GameObject helpTutorial1;
    public GameObject helpTutorial2;
    public GameObject resourceSystem;
    public GameObject moneySystem;
    public TextMeshProUGUI messageText;
    public Settings settings;
    public bool mainGame = false;
    public int helpStep;
    public bool canMove = true;
    public bool canOpenPopups = true;
    public bool canOpenShop = true;
    public bool mainTutorial = false;
    public bool[] helpShown = new bool[7];
    public bool readingHelp = false;
    public bool helpBuy = false;
    public bool messageShown = false;

    private float helptime = 0;

    private void Start()
    {
        string path = Application.persistentDataPath + "/data.save";
        if (!File.Exists(path))
        {
            for (int i = 0; i < helpShown.Length; i++)
                helpShown[i] = false;
        }
    }

    public void ToggleMessage(string text)
    {
        if (messageShown)
        {
            messageShown = false;
            message.SetActive(false);
            Time.timeScale = 1;

            switch (messageText.text)
            {
                case "Добро пожаловать в игру Алхимик! Вы управляете этой лавкой и вам нужно выполнять заказы чтобы получать деньги и репутацию.":
                    ToggleMessage("Вот ваш первый клиент, сверху указано что он хочет у вас купить. Он никуда не торопится, но следующие клиенты будут не так терпеливы, так что в будущем придется торопиться с их обслуживанием.");
                    break;
                case "Вот ваш первый клиент, сверху указано что он хочет у вас купить. Он никуда не торопится, но следующие клиенты будут не так терпеливы, так что в будущем придется торопиться с их обслуживанием.":
                    ToggleMessage("Теперь когда мы знаем что хочет клиент, приготовим ему это зелье! Для этого перейдем вглубь лавки к котлу.");
                    break;
                case "Теперь когда мы знаем что хочет клиент, приготовим ему это зелье! Для этого перейдем вглубь лавки к котлу.":
                    helpTutorial1.SetActive(true);
                    helpBuy = true;
                    break;
                case "Это рабочее место. По центру находится котел, слева и справа будут располагаться ресурсы для зельеварения.":
                    ToggleMessage("Давай купим ингредиенты для зелья нашего клиента.");
                    break;
                case "Давай купим ингредиенты для зелья нашего клиента.":
                    canMove = false;
                    canOpenShop = true;
                    helpTutorial2.SetActive(true);
                    break;
                case "Все готово, возвращаемся на рабочее место.":
                    ToggleMessage("Перетащите оба ресурса в котел, и нажмите на кнопку сварить зелье.");
                    break;
                case "Следи за репутацией вверху экрана. Если ее будет слишком мало, гильдии не будут покупать зелья и ты проиграешь.":
                    ToggleMessage("Твой первый клиент был добр и в помощь начинающему алхимику дал тебе немного ресурсов.");
                    break;
                case "Твой первый клиент был добр и в помощь начинающему алхимику дал тебе немного ресурсов.":
                    resourceSystem.GetComponent<ResourceSystem>().AddResource(ResourceType.Red, 5);
                    resourceSystem.GetComponent<ResourceSystem>().AddResource(ResourceType.Blue, 5);
                    resourceSystem.GetComponent<ResourceSystem>().AddResource(ResourceType.Yellow, 5);
                    resourceSystem.GetComponent<ResourceSystem>().AddResource(ResourceType.White, 5);
                    moneySystem.GetComponent<MoneySystem>().AddMoney(200);
                    canMove = true;
                    canOpenPopups = true;
                    canOpenShop = true;
                    mainTutorial = true;
                    break;
                default:
                    break;
            }
        }
        else
        {
            messageText.text = text;
            messageShown = true;
            message.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void GetHelp()
    {
        if (!mainGame) return;
        if (!helpShown[helpStep]) helpButton.interactable = true;
        helpShown[helpStep] = true;
    }

    public void ShowHelp()
    {
        readingHelp = true;
        switch (helpStep)
        {
            case 0:
                GetComponent<Popups>().PopupOpen(1);
                helpScreens[0].SetActive(true);
                break;
            case 1:
                GetComponent<Popups>().PopupOpen(2);
                helpScreens[1].SetActive(true);
                break;
            case 2:
                GetComponent<Popups>().PopupOpen(2);
                helpScreens[2].SetActive(true);
                break;
            case 3:
                GetComponent<Popups>().PopupOpen(1);
                helpScreens[3].SetActive(true);
                break;
            case 4:
                GetComponent<Popups>().PopupOpen(1);
                helpScreens[4].SetActive(true);
                break;
            case 5:
                if (GetComponent<CameraMovement>().dir == 1)
                    GetComponent<CameraMovement>().MoveCam();
                helpScreens[5].SetActive(true);
                break;
            case 6:
                GetComponent<Popups>().PopupOpen(3);
                helpScreens[6].SetActive(true);
                break;
            default:
                break;
        }
        helpButton.interactable = false;
    }

    private void Update()
    {
        if (readingHelp)
            helptime += Time.deltaTime;

        if (helptime >= settings.helpTime && Input.touchCount > 0)
        {
            readingHelp = false;
            helptime = 0;
            foreach (var item in helpScreens)
                item.SetActive(false);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Bottles : MonoBehaviour
{
    public Camera cam;

    public Button drainButton;
    public Button helpButton;
    public TextMeshProUGUI amountText;

    public UnityEngine.GameObject[] bottlePos;
    public UnityEngine.GameObject[] bottle;
    public UnityEngine.GameObject cauldron;
    public UnityEngine.GameObject UIControls;
    public UnityEngine.GameObject water;
    public UnityEngine.GameObject potionSystem;
    public UnityEngine.GameObject bottleSpawner;
    public UnityEngine.GameObject moneySystem;

    public Settings settings;

    public bool[] takenSpace = new bool[8];
    public bool[] bottleUsage = new bool[8];

    private Touch touch;
    private Transform toDrag;
    private Rigidbody2D toDragRB;

    public bool canTake = true;
    private bool isDragging = false;
    private bool justTook = false;
    private bool justReturned = false;

    private float time = 1;

    public int bottleCount = 2;
    public int freeBottles;
    private int takenBottleNumber;

    private void Start()
    {
        amountText.text = bottleCount.ToString();
        freeBottles = bottleCount;

        for (int i = 0; i < bottle.Length; i++)     //Сортировка какая-то? 
        {
            if (bottleUsage[i])
            {
                for (int j = 0; j < takenSpace.Length; j++)
                {
                    if (!takenSpace[j])
                    {
                        takenSpace[j] = true;
                        bottle[i].transform.position = bottlePos[j].transform.position;
                        bottle[i].SetActive(true);
                        freeBottles--;
                        amountText.text = freeBottles.ToString();
                        break;
                    }
                }
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < bottleCount; i++)
        {
            if (bottle[i].transform.position != bottlePos[bottle[i].GetComponent<BottlePotion>().takenSpace].transform.position && //определение если бутылка пустая возвращать в правую часть инвентаря? 
                bottle[i].GetComponent<BottlePotion>().potionColor != PotionColor.Empty && !isDragging)
            {
                bottle[i].transform.position = Vector3.MoveTowards(bottle[i].transform.position, bottlePos[bottle[i].GetComponent<BottlePotion>().takenSpace].transform.position, settings.bottleSpeed);    //само возвращение
            }
        }

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && canTake)
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                    return;

                isDragging = true;

                Vector3 pos = cam.ScreenToWorldPoint(touch.position);
                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

                if (Physics2D.Raycast(pos, Vector2.zero) && hit.collider.gameObject.layer == LayerMask.NameToLayer("Bottle"))   //логика взятия бутылки
                {
                    time = 0;
                    canTake = false;
                    toDrag = hit.collider.gameObject.transform;

                    if (toDrag != null)
                        toDragRB = toDrag.GetComponent<Rigidbody2D>();

                    takenSpace[toDrag.GetComponent<BottlePotion>().takenSpace] = false;
                }

                if (Physics2D.Raycast(pos, Vector2.zero) && hit.transform.CompareTag("BottleSpawner") && freeBottles > 0)       //логика убирания бутылки с уменьшение количества
                {
                    freeBottles--;
                    amountText.text = freeBottles.ToString();

                    time = 0;
                    canTake = false;

                    for (int i = 0; i < bottleCount; i++)       //??
                    {
                        if (!bottleUsage[i])
                        {
                            toDrag = bottle[i].transform;
                            toDrag.gameObject.SetActive(true);
                            toDrag.position = pos;
                            bottleUsage[i] = true;
                            break;
                        }
                    }

                    if (toDrag != null)
                        toDragRB = toDrag.GetComponent<Rigidbody2D>();

                    toDragRB.simulated = false;
                }
            }

            time += Time.deltaTime;     //??

            if (touch.phase == TouchPhase.Moved && isDragging)
            {
                if (toDrag != null)
                {
                    toDragRB.simulated = false;
                    toDrag.position = cam.ScreenToWorldPoint(touch.position);
                }
            }

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) 
            {
                if (time < 0.1f)
                    UIControls.GetComponent<CameraMovement>().MoveCam(); // ??
                time = 1;

                isDragging = false;
                if (toDrag != null)
                {
                    StartCoroutine(Wait());
                    toDragRB.simulated = true;
                }
            }
        }

        if (Input.touchCount == 0 && toDrag)
        {
            if (cauldron.GetComponent<MixingSystem>().isReady && cauldron.GetComponent<MixingSystem>().bottleIn && !justReturned)   //если напиток готов
            {
                if (!UIControls.GetComponent<Tutorial>().mainGame && !UIControls.GetComponent<Tutorial>().mainTutorial)         //часть туториала
                {
                    UIControls.GetComponent<Tutorial>().ToggleMessage("Давай вернемся к нашему клиенту и отдадим ему зелье.");
                    UIControls.GetComponent<Tutorial>().canMove = true;
                }

                justTook = true;

                for (int i = 0; i < takenSpace.Length; i++)
                {
                    if (!takenSpace[i])
                    {
                        toDrag.GetComponent<BottlePotion>().AddPotion(cauldron.GetComponent<MixingSystem>().inCauldron,                 //отдебажить и понять что это
                            cauldron.GetComponent<MixingSystem>().inCauldronColored, cauldron.GetComponent<MixingSystem>().isRare, i);
                        takenSpace[i] = true;
                        break;
                    }
                }

                toDrag.Find("Water").gameObject.SetActive(true);
                toDrag.Find("Water").GetComponent<SpriteRenderer>().color = water.GetComponent<SpriteRenderer>().color;

                toDrag.transform.Find("PSMask").Find("GlowPS").GetComponent<ParticleSystem>().Stop();       //сброс свечения редких зельев
                toDrag.transform.Find("PSMask").Find("BoilPS").GetComponent<ParticleSystem>().Stop();
                toDrag.transform.Find("PSMask").Find("FirePS").GetComponent<ParticleSystem>().Stop();
                toDrag.transform.Find("PSMask").Find("SmokePS").GetComponent<ParticleSystem>().Stop();

                switch (cauldron.GetComponent<MixingSystem>().GetBrewEffect())      //активация свечения зелья после приготовления
                {
                    case PotionEffect.Glowing:
                        toDrag.transform.Find("PSMask").Find("GlowPS").GetComponent<ParticleSystem>().Play();
                        break;
                    case PotionEffect.Boiling:
                        toDrag.transform.Find("PSMask").Find("BoilPS").GetComponent<ParticleSystem>().Play();
                        break;
                    case PotionEffect.Burning:
                        toDrag.transform.Find("PSMask").Find("FirePS").GetComponent<ParticleSystem>().Play();
                        break;
                    case PotionEffect.Smoking:
                        toDrag.transform.Find("PSMask").Find("SmokePS").GetComponent<ParticleSystem>().Play();
                        break;
                    default:
                        break;
                }

                cauldron.GetComponent<MixingSystem>().TakePotion();
            }
            #region присвоение бутылке цвета, который находится в котле? 

            if (cauldron.GetComponent<MixingSystem>().bottleIn && cauldron.GetComponent<MixingSystem>().inCauldron.Count == 0 && toDrag.GetComponent<BottlePotion>().potionColor != PotionColor.Empty && !justTook)
            {
                justReturned = true;
                toDrag.GetComponent<BottlePotion>().justDrained = true;
                cauldron.GetComponent<MixingSystem>().isReady = true;
                switch (toDrag.GetComponent<BottlePotion>().potionColor)
                {
                    case PotionColor.Black:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(ResourceType.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(ResourceType.Blue);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(ResourceType.Yellow);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(ResourceType.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(ResourceType.Blue);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(ResourceType.Yellow);
                        water.GetComponent<WaterColor>().targetColor = settings.colors[6];
                        break;
                    case PotionColor.Gray:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(ResourceType.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(ResourceType.Blue);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(ResourceType.Yellow);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(ResourceType.White);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(ResourceType.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(ResourceType.Blue);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(ResourceType.Yellow);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(ResourceType.White);
                        water.GetComponent<WaterColor>().targetColor = settings.colors[7];
                        break;
                    case PotionColor.Purple:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(ResourceType.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(ResourceType.Blue);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(ResourceType.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(ResourceType.Blue);
                        water.GetComponent<WaterColor>().targetColor = settings.colors[0];
                        break;
                    case PotionColor.Orange:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(ResourceType.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(ResourceType.Yellow);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(ResourceType.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(ResourceType.Yellow);
                        water.GetComponent<WaterColor>().targetColor = settings.colors[1];
                        break;
                    case PotionColor.Green:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(ResourceType.Blue);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(ResourceType.Yellow);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(ResourceType.Blue);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(ResourceType.Yellow);
                        water.GetComponent<WaterColor>().targetColor = settings.colors[2];
                        break;
                    case PotionColor.Violet:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(ResourceType.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(ResourceType.Blue);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(ResourceType.White);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(ResourceType.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(ResourceType.Blue);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(ResourceType.White);
                        water.GetComponent<WaterColor>().targetColor = settings.colors[9];
                        break;
                    case PotionColor.Peach:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(ResourceType.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(ResourceType.Yellow);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(ResourceType.White);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(ResourceType.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(ResourceType.Yellow);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(ResourceType.White);
                        water.GetComponent<WaterColor>().targetColor = settings.colors[8];
                        break;
                    case PotionColor.Lime:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(ResourceType.Blue);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(ResourceType.Yellow);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(ResourceType.White);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(ResourceType.Blue);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(ResourceType.Yellow);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(ResourceType.White);
                        water.GetComponent<WaterColor>().targetColor = settings.colors[10];
                        break;
                    case PotionColor.Pink:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(ResourceType.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(ResourceType.White);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(ResourceType.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(ResourceType.White);
                        water.GetComponent<WaterColor>().targetColor = settings.colors[3];
                        break;
                    case PotionColor.LightBlue:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(ResourceType.Blue);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(ResourceType.White);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(ResourceType.Blue);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(ResourceType.White);
                        water.GetComponent<WaterColor>().targetColor = settings.colors[5];
                        break;
                    case PotionColor.Gold:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(ResourceType.Yellow);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(ResourceType.White);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(ResourceType.Yellow);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(ResourceType.White);
                        water.GetComponent<WaterColor>().targetColor = settings.colors[4];
                        break;
                    default:
                        break;
                }
                #endregion

                #region присвоение эффекта приготовленного зелья? 
                switch (toDrag.GetComponent<BottlePotion>().potionEffect)
                {
                    case PotionEffect.Glowing:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(ResourceType.Ladan);
                        cauldron.GetComponent<MixingSystem>().isRare = true;
                        break;
                    case PotionEffect.Boiling:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(ResourceType.Eye);
                        cauldron.GetComponent<MixingSystem>().isRare = true;
                        break;
                    case PotionEffect.Burning:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(ResourceType.Stone);
                        cauldron.GetComponent<MixingSystem>().isRare = true;
                        break;
                    case PotionEffect.Smoking:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(ResourceType.Sand);
                        cauldron.GetComponent<MixingSystem>().isRare = true;
                        break;
                    default:
                        break;
                }
                #endregion

                toDrag.Find("Water").GetComponent<SpriteRenderer>().color = Color.white;
                toDrag.Find("Water").gameObject.SetActive(false);
                toDrag.GetComponent<BottlePotion>().potionColor = PotionColor.Empty;
                toDrag.GetComponent<BottlePotion>().potionEffect = PotionEffect.Empty;
                drainButton.interactable = true;

                switch (toDrag.tag)     //??
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
            }
            
        }
        #region Сброс состояния бутылки на обычный? 
        if (toDrag && !isDragging)      
        {
            if (toDrag.GetComponent<BottlePotion>().potionColor == PotionColor.Empty)
            {
                takenBottleNumber = toDrag.GetComponent<BottlePotion>().takenSpace;
                toDrag.position = Vector3.MoveTowards(toDrag.position, bottleSpawner.transform.position, settings.bottleSpeed);
                if (toDrag.position == bottleSpawner.transform.position)
                {
                    switch (toDrag.tag)
                    {
                        case "Bottle1":
                            bottleUsage[0] = false;
                            break;
                        case "Bottle2":
                            bottleUsage[1] = false;
                            break;
                        case "Bottle3":
                            bottleUsage[2] = false;
                            break;
                        case "Bottle4":
                            bottleUsage[3] = false;
                            break;
                        case "Bottle5":
                            bottleUsage[4] = false;
                            break;
                        case "Bottle6":
                            bottleUsage[5] = false;
                            break;
                        case "Bottle7":
                            bottleUsage[6] = false;
                            break;
                        case "Bottle8":
                            bottleUsage[7] = false;
                            break;
                        default:
                            break;
                    }
                    
                    int max = 0;
                    for (int i = 0; i < bottleCount; i++)
                    {
                        if (bottle[i].GetComponent<BottlePotion>().potionColor != PotionColor.Empty && bottle[i].GetComponent<BottlePotion>().takenSpace > takenBottleNumber &&
                            (toDrag.GetComponent<BottlePotion>().justDrained || toDrag.GetComponent<BottlePotion>().justGiven))
                        {
                            if (max < bottle[i].GetComponent<BottlePotion>().takenSpace)
                                max = bottle[i].GetComponent<BottlePotion>().takenSpace;

                            bottle[i].GetComponent<BottlePotion>().takenSpace--;
                            takenSpace[bottle[i].GetComponent<BottlePotion>().takenSpace] = true;
                        }
                    }
                    if (max > 0)
                        takenSpace[max] = false;

                    toDrag.GetComponent<BottlePotion>().justDrained = false;
                    toDrag.GetComponent<BottlePotion>().justGiven = false;
                    toDrag.gameObject.SetActive(false);
                    canTake = true;
                    toDrag = null;
                    toDragRB.simulated = true;
                    toDragRB = null;
                    justTook = false;
                    justReturned = false;
                    freeBottles++;
                    amountText.text = freeBottles.ToString();
                }
            }
            else
            {
                //if (toDrag.position == bottlePos[toDrag.GetComponent<BottlePotion>().takenSpace].transform.position)
                //{
                //    if (freeBottles == 0 && moneySystem.GetComponent<MoneySystem>().GetMoney() >= moneySystem.GetComponent<ShopSystem>().bottleCost && !helpButton.interactable)
                //    {
                //        UIControls.GetComponent<Tutorial>().helpStep = 0;
                //        UIControls.GetComponent<Tutorial>().GetHelp();
                //    }
                //    takenSpace[toDrag.GetComponent<BottlePotion>().takenSpace] = true;
                //    canTake = true;
                //    toDrag = null;
                //    toDragRB.simulated = true;
                //    toDragRB = null;
                //    justTook = false;
                //    justReturned = false;
                //}
            }
        }
        #endregion
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.02f);

        if (toDragRB)
            toDragRB.simulated = false;
    }

    public void AddBottle()
    {
        bottleCount++;
        freeBottles++;
        amountText.text = freeBottles.ToString();
    }

    public int GetBottleCount()
    {
        return bottleCount;
    }
}
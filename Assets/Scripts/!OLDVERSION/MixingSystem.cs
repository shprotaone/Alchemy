using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MixingSystem : MonoBehaviour
{
    public TextMeshProUGUI wrongBrewText;
    public Button brewButton;
    public Button drainButton;
    public Button fuelButton;
    public Button buyCauldron;
    public Button helpButton;
    public Slider progressBar;

    public UnityEngine.GameObject resourceSystem;
    public UnityEngine.GameObject water;
    public UnityEngine.GameObject waterBoil;
    public UnityEngine.GameObject recipes;
    public UnityEngine.GameObject UIControls;
    public UnityEngine.GameObject table;

    public Sprite tableOff;
    public Sprite tableOn;

    public Potionv1[] potions;

    public List<ResourceType> inCauldron;
    public List<ResourceType> inCauldronColored;

    public Cauldrons[] cauldrons;

    public Settings settings;

    public AudioClip addResource;
    public AudioClip brewSound;
    public AudioClip takePotionSound;

    public int cauldronId;

    public bool isRare = false;
    public bool isWrong = false;
    public bool isReady = false;
    public bool bottleIn = false;
    public bool isBrewing = false;

    private bool showTimer = false;
    private bool isFueled = false;
    private bool slowCooldown = false;
    private bool cauldronSpawned = false;

    public float time = 0f;
    public float startTime;
    private float speed = 1;
    private float curSpeed = 0;
    public float cooldownTime;
    public float speedMul;
    public float chance;

    private string showTime = null;

    private void Start()
    {
        if (!cauldronSpawned)
        {
            cauldronId = 0;
            GetComponent<SpriteRenderer>().sprite = cauldrons[cauldronId].image;
            speedMul = cauldrons[cauldronId].speedMul;
            chance = cauldrons[cauldronId].chance;
        }
    }

    public void ChangeCauldron(int id)
    {
        cauldronSpawned = true;
        cauldronId = id;
        GetComponent<SpriteRenderer>().sprite = cauldrons[cauldronId].image;
        speedMul = cauldrons[cauldronId].speedMul;
        chance = cauldrons[cauldronId].chance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Bottle"))
        {
            GetComponent<AudioSource>().clip = addResource;
            GetComponent<AudioSource>().Play();

            isReady = false;
            if (UIControls.GetComponent<Tutorial>().mainGame)
            {
                drainButton.interactable = true;
                brewButton.interactable = true;
            }
            else if (inCauldron.Count == 1)
            {
                brewButton.interactable = true;
            }
            buyCauldron.interactable = false;
            Destroy(collision.gameObject);
        }

       

        water.GetComponent<WaterColor>().ChangeColor(inCauldronColored, isWrong);
        waterBoil.GetComponent<WaterColor>().ChangeColor(inCauldronColored, isWrong);

        if (collision.gameObject.layer == LayerMask.NameToLayer("Bottle"))
            bottleIn = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bottle"))
            bottleIn = false;
    }

    public void Brew()
    {
        cooldownTime = cauldrons[cauldronId].cooldown;
        drainButton.interactable = false;
        resourceSystem.GetComponent<DragResources>().StartMixing();

        if (!isRare && inCauldron.Count < 2 || isRare && inCauldron.Count < 3)
            isWrong = true;

        time = TimeCalc();      //время, за которое варится
        startTime = time;
        brewButton.interactable = false;
        showTimer = true;
        curSpeed = speed;
        StartCoroutine(Brewing());
    }

    public float TimeCalc()
    {
        float time = 0;     

        if (isRare)
            time = settings.timeBrewRare;

        else
        {
            switch (inCauldron.Count)
            {
                case 2:
                    time = settings.timeBrew2;
                    break;

                case 3:
                    time = settings.timeBrew3;
                    break;

                case 4:
                    time = settings.timeBrew4;
                    break;

                default:
                    break;
            }
        }

        time *= speedMul;

        return time;
    }

    IEnumerator Brewing()
    {
        if (isWrong)
        {
            water.GetComponent<WaterColor>().ClearWater();
            waterBoil.GetComponent<WaterColor>().ChangeColor(inCauldronColored, isWrong);
            wrongBrewText.gameObject.SetActive(true);

            if (isFueled)
            {
                slowCooldown = true;
                yield return new WaitForSeconds(cooldownTime / (speed / 2));
                slowCooldown = false;
            }
            else
                yield return new WaitForSeconds(cooldownTime / speed);

            isReady = false;
            inCauldron.Clear();
            inCauldronColored.Clear();
        }
        else
        {
            GetComponent<AudioSource>().clip = brewSound;
            GetComponent<AudioSource>().Play();
            isBrewing = true;

            yield return new WaitForSeconds(time / speed);
            if (!helpButton.interactable)
            {
                UIControls.GetComponent<Tutorial>().helpStep = 6;
                UIControls.GetComponent<Tutorial>().GetHelp();
            }
            if (UIControls.GetComponent<Tutorial>().mainGame) drainButton.interactable = true;
            else if (!UIControls.GetComponent<Tutorial>().mainTutorial) UIControls.GetComponent<Tutorial>().ToggleMessage("Возьми пустую бутылку с верхней полки и налей в неё зелье, перетащив на котел.");
            isReady = true;
            isBrewing = false;
        }

        resourceSystem.GetComponent<DragResources>().StopMixing();

        isWrong = false;
        showTimer = false;
        wrongBrewText.gameObject.SetActive(false);

        switch (GetBrewEffect())
        {
            case PotionEffect.Normal:
                switch (GetBrewColor())
                {
                    case PotionColor.Black:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[3]);
                        break;
                    case PotionColor.Gray:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[13]);
                        break;
                    case PotionColor.Purple:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[48]);
                        break;
                    case PotionColor.Orange:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[38]);
                        break;
                    case PotionColor.Green:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[18]);
                        break;
                    case PotionColor.Violet:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[53]);
                        break;
                    case PotionColor.Peach:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[28]);
                        break;
                    case PotionColor.Lime:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[33]);
                        break;
                    case PotionColor.Pink:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[43]);
                        break;
                    case PotionColor.LightBlue:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[23]);
                        break;
                    case PotionColor.Gold:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[8]);
                        break;
                    default:
                        break;
                }
                break;

            case PotionEffect.Glowing:
                switch (GetBrewColor())
                {
                    case PotionColor.Black:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[2]);
                        break;
                    case PotionColor.Gray:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[12]);
                        break;
                    case PotionColor.Purple:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[47]);
                        break;
                    case PotionColor.Orange:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[37]);
                        break;
                    case PotionColor.Green:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[17]);
                        break;
                    case PotionColor.Violet:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[52]);
                        break;
                    case PotionColor.Peach:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[27]);
                        break;
                    case PotionColor.Lime:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[32]);
                        break;
                    case PotionColor.Pink:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[42]);
                        break;
                    case PotionColor.LightBlue:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[22]);
                        break;
                    case PotionColor.Gold:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[7]);
                        break;
                    default:
                        break;
                }
                break;

            case PotionEffect.Boiling:
                switch (GetBrewColor())
                {
                    case PotionColor.Black:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[0]);
                        break;
                    case PotionColor.Gray:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[10]);
                        break;
                    case PotionColor.Purple:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[45]);
                        break;
                    case PotionColor.Orange:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[35]);
                        break;
                    case PotionColor.Green:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[15]);
                        break;
                    case PotionColor.Violet:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[50]);
                        break;
                    case PotionColor.Peach:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[25]);
                        break;
                    case PotionColor.Lime:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[30]);
                        break;
                    case PotionColor.Pink:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[40]);
                        break;
                    case PotionColor.LightBlue:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[20]);
                        break;
                    case PotionColor.Gold:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[5]);
                        break;
                    default:
                        break;
                }
                break;

            case PotionEffect.Burning:
                switch (GetBrewColor())
                {
                    case PotionColor.Black:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[1]);
                        break;
                    case PotionColor.Gray:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[11]);
                        break;
                    case PotionColor.Purple:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[46]);
                        break;
                    case PotionColor.Orange:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[36]);
                        break;
                    case PotionColor.Green:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[16]);
                        break;
                    case PotionColor.Violet:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[51]);
                        break;
                    case PotionColor.Peach:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[26]);
                        break;
                    case PotionColor.Lime:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[31]);
                        break;
                    case PotionColor.Pink:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[41]);
                        break;
                    case PotionColor.LightBlue:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[21]);
                        break;
                    case PotionColor.Gold:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[6]);
                        break;
                    default:
                        break;
                }
                break;

            case PotionEffect.Smoking:
                switch (GetBrewColor())
                {
                    case PotionColor.Black:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[4]);
                        break;
                    case PotionColor.Gray:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[14]);
                        break;
                    case PotionColor.Purple:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[49]);
                        break;
                    case PotionColor.Orange:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[39]);
                        break;
                    case PotionColor.Green:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[19]);
                        break;
                    case PotionColor.Violet:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[54]);
                        break;
                    case PotionColor.Peach:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[29]);
                        break;
                    case PotionColor.Lime:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[34]);
                        break;
                    case PotionColor.Pink:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[44]);
                        break;
                    case PotionColor.LightBlue:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[24]);
                        break;
                    case PotionColor.Gold:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[9]);
                        break;
                    default:
                        break;
                }
                break;

            default:
                break;
        }
    }

    public void TakePotion()
    {
        GetComponent<AudioSource>().clip = takePotionSound;
        GetComponent<AudioSource>().Play();

        isReady = false;
        isRare = false;
        inCauldron.Clear();
        inCauldronColored.Clear();
        progressBar.value = 0;
        drainButton.interactable = false;
        water.GetComponent<WaterColor>().ClearWater();
        waterBoil.GetComponent<WaterColor>().ClearWater();
    }

    public void DrainCauldron()
    {
        drainButton.interactable = false;

        foreach (var item in inCauldron)
        {
            if (Random.value <= chance)
            {
                switch (item)
                {
                    case ResourceType.Red:
                        resourceSystem.GetComponent<ResourceSystem>().AddResource(ResourceType.Red, 1);
                        break;

                    case ResourceType.Blue:
                        resourceSystem.GetComponent<ResourceSystem>().AddResource(ResourceType.Blue, 1);
                        break;

                    case ResourceType.Yellow:
                        resourceSystem.GetComponent<ResourceSystem>().AddResource(ResourceType.Yellow, 1);
                        break;

                    case ResourceType.White:
                        resourceSystem.GetComponent<ResourceSystem>().AddResource(ResourceType.White, 1);
                        break;

                    case ResourceType.Ladan:
                        resourceSystem.GetComponent<ResourceSystem>().AddResource(ResourceType.Ladan, 1);
                        break;

                    case ResourceType.Eye:
                        resourceSystem.GetComponent<ResourceSystem>().AddResource(ResourceType.Eye, 1);
                        break;

                    case ResourceType.Stone:
                        resourceSystem.GetComponent<ResourceSystem>().AddResource(ResourceType.Stone, 1);
                        break;

                    case ResourceType.Sand:
                        resourceSystem.GetComponent<ResourceSystem>().AddResource(ResourceType.Sand, 1);
                        break;

                    default:
                        break;
                }
            }
        }
        buyCauldron.interactable = true;;
        inCauldron.Clear();
        inCauldronColored.Clear();
        isWrong = false;
        isRare = false;
        isReady = false;
        brewButton.interactable = false;
        progressBar.value = 0;
        water.GetComponent<WaterColor>().ClearWater();
        waterBoil.GetComponent<WaterColor>().ClearWater();
    }

    private void Update()
    {
        if (isBrewing)
        {
            waterBoil.GetComponent<SpriteRenderer>().color = Color.Lerp(waterBoil.GetComponent<SpriteRenderer>().color,
                new Vector4(waterBoil.GetComponent<SpriteRenderer>().color.r, waterBoil.GetComponent<SpriteRenderer>().color.g, waterBoil.GetComponent<SpriteRenderer>().color.b, 1), 0.02f);
        }
        else
        {
            waterBoil.GetComponent<SpriteRenderer>().color = Color.Lerp(waterBoil.GetComponent<SpriteRenderer>().color,
                new Vector4(waterBoil.GetComponent<SpriteRenderer>().color.r, waterBoil.GetComponent<SpriteRenderer>().color.g, waterBoil.GetComponent<SpriteRenderer>().color.b, 0), 0.02f);
        }

        if (showTimer)                              //показывает процесс варки
        {
            time -= Time.deltaTime * curSpeed;
            if (slowCooldown)
                cooldownTime -= Time.deltaTime * (curSpeed / 2);
            else
                cooldownTime -= Time.deltaTime * curSpeed;

            if (isWrong)
                showTime = cooldownTime.ToString("F0");
            else
            {
                showTime = time.ToString("F0");
                progressBar.value = (startTime - time) / startTime;
            }
        }
    }

    public void fuelSpeedUp()
    {
        fuelButton.interactable = false;
        speed *= settings.woodSpeedup;
        isFueled = true;
        table.GetComponent<SpriteRenderer>().sprite = tableOn;
        StartCoroutine(FuelCombustion());
    }

    IEnumerator FuelCombustion()
    {
        yield return new WaitForSeconds(settings.timeWood / (speed / settings.woodSpeedup));
        if (resourceSystem.GetComponent<Fuel>().GetFuelCount() > 0)
            fuelButton.interactable = true;
        speed /= settings.woodSpeedup;
        isFueled = false;
        table.GetComponent<SpriteRenderer>().sprite = tableOff;
    }

    public int GetCauldron()
    {
        return cauldronId;
    }

    public PotionEffect GetBrewEffect()
    {
        foreach (var item in inCauldron)
        {
            if (item==ResourceType.Ladan)
                return PotionEffect.Glowing;
            if (item == ResourceType.Eye)
                return PotionEffect.Boiling;
            if (item == ResourceType.Stone)
                return PotionEffect.Burning;
            if (item == ResourceType.Sand)
                return PotionEffect.Smoking;
        }
        return PotionEffect.Normal;
    }

    public PotionColor GetBrewColor()
    {
        switch (inCauldronColored.Count)
        {
            case 2:
                if (inCauldronColored.Contains(ResourceType.Red) && inCauldronColored.Contains(ResourceType.Blue))
                    return PotionColor.Purple;

                if (inCauldronColored.Contains(ResourceType.Red) && inCauldronColored.Contains(ResourceType.Yellow))
                    return PotionColor.Orange;

                if (inCauldronColored.Contains(ResourceType.Blue) && inCauldronColored.Contains(ResourceType.Yellow))
                    return PotionColor.Green;

                if (inCauldronColored.Contains(ResourceType.Red) && inCauldronColored.Contains(ResourceType.White))
                    return PotionColor.Pink;

                if (inCauldronColored.Contains(ResourceType.Blue) && inCauldronColored.Contains(ResourceType.White))
                    return PotionColor.LightBlue;

                if (inCauldronColored.Contains(ResourceType.Yellow) && inCauldronColored.Contains(ResourceType.White))
                    return PotionColor.Gold;
                break;

            case 3:
                if (inCauldronColored.Contains(ResourceType.Red) && inCauldronColored.Contains(ResourceType.Blue) && inCauldronColored.Contains(ResourceType.White))
                    return PotionColor.Violet;

                if (inCauldronColored.Contains(ResourceType.Red) && inCauldronColored.Contains(ResourceType.Yellow) && inCauldronColored.Contains(ResourceType.White))
                    return PotionColor.Peach;

                if (inCauldronColored.Contains(ResourceType.Blue) && inCauldronColored.Contains(ResourceType.Yellow) && inCauldronColored.Contains(ResourceType.White))
                    return PotionColor.Lime;

                if (inCauldronColored.Contains(ResourceType.Red) && inCauldronColored.Contains(ResourceType.Blue) && inCauldronColored.Contains(ResourceType.Yellow))
                    return PotionColor.Black;
                break;

            case 4:
                return PotionColor.Gray;

            default:
                break;
        }
        return PotionColor.Empty;
    }
}
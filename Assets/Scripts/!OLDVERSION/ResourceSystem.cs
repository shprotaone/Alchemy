using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ResourceData
{
    public ResourceType resourceName;
    public int amount;
    public ResourceRarity resourceRarity;

    public ResourceData(ResourceType _resourceName, ResourceRarity _resourceRarity)
    {
        resourceName = _resourceName;
        amount = 0;
        resourceRarity = _resourceRarity;
    }
}

public class ResourceSystem : MonoBehaviour
{
    public TextMeshProUGUI textRed;
    public TextMeshProUGUI textBlue;
    public TextMeshProUGUI textYellow;
    public TextMeshProUGUI textWhite;
    public TextMeshProUGUI textLadan;
    public TextMeshProUGUI textEye;
    public TextMeshProUGUI textStone;
    public TextMeshProUGUI textSand;

    public GameObject platformRed;
    public GameObject platformBlue;
    public GameObject platformYellow;
    public GameObject platformWhite;
    public GameObject platformLadan;
    public GameObject platformEye;
    public GameObject platformStone;
    public GameObject platformSand;

    public Sprite red;
    public Sprite redEmpty;
    public Sprite blue;
    public Sprite blueEmpty;
    public Sprite yellow;
    public Sprite yellowEmpty;
    public Sprite white;
    public Sprite whiteEmpty;
    public Sprite ladan;
    public Sprite ladanEmpty;
    public Sprite eye;
    public Sprite eyeEmpty;
    public Sprite stone;
    public Sprite stoneEmpty;
    public Sprite sand;
    public Sprite sandEmpty;

    public List<ResourceData> resources = new List<ResourceData>();

    private void Awake()
    {
        ResourceData red = new ResourceData(ResourceType.Red, ResourceRarity.Common);
        resources.Add(red);
        ResourceData blue = new ResourceData(ResourceType.Blue, ResourceRarity.Common);
        resources.Add(blue);
        ResourceData yellow = new ResourceData(ResourceType.Yellow, ResourceRarity.Common);
        resources.Add(yellow);
        ResourceData white = new ResourceData(ResourceType.White, ResourceRarity.Common);
        resources.Add(white);
        ResourceData ladan = new ResourceData(ResourceType.Ladan, ResourceRarity.Rare);
        resources.Add(ladan);
        ResourceData eye = new ResourceData(ResourceType.Eye, ResourceRarity.Rare);
        resources.Add(eye);
        ResourceData stone = new ResourceData(ResourceType.Stone, ResourceRarity.Rare);
        resources.Add(stone);
        ResourceData sand = new ResourceData(ResourceType.Sand, ResourceRarity.Rare);
        resources.Add(sand);
    }

    private void Start()
    {
        if (GetAmount(ResourceType.Red) == 0)
            platformRed.GetComponent<SpriteRenderer>().sprite = redEmpty;
        if (GetAmount(ResourceType.Blue) == 0)
            platformBlue.GetComponent<SpriteRenderer>().sprite = blueEmpty;
        if (GetAmount(ResourceType.Yellow) == 0)
            platformYellow.GetComponent<SpriteRenderer>().sprite = yellowEmpty;
        if (GetAmount(ResourceType.White) == 0)
            platformWhite.GetComponent<SpriteRenderer>().sprite = whiteEmpty;
        if (GetAmount(ResourceType.Ladan) == 0)
            platformLadan.GetComponent<SpriteRenderer>().sprite = ladanEmpty;
        if (GetAmount(ResourceType.Eye) == 0)
            platformEye.GetComponent<SpriteRenderer>().sprite = eyeEmpty;
        if (GetAmount(ResourceType.Stone) == 0)
            platformStone.GetComponent<SpriteRenderer>().sprite = stoneEmpty;
        if (GetAmount(ResourceType.Sand) == 0)
            platformSand.GetComponent<SpriteRenderer>().sprite = sandEmpty;
    }

    public void AddResource(ResourceType resourceType, int amount)
    {
        foreach (ResourceData res in resources)
        {
            if (res.resourceName == resourceType)
            {
                res.amount += amount;

                switch (res.resourceName)
                {
                    case ResourceType.Red:
                        platformRed.GetComponent<SpriteRenderer>().sprite = red;
                        textRed.text = $"{GetAmount(ResourceType.Red)}";
                        break;

                    case ResourceType.Blue:
                        platformBlue.GetComponent<SpriteRenderer>().sprite = blue;
                        textBlue.text = $"{GetAmount(ResourceType.Blue)}";
                        break;

                    case ResourceType.Yellow:
                        platformYellow.GetComponent<SpriteRenderer>().sprite = yellow;
                        textYellow.text = $"{GetAmount(ResourceType.Yellow)}";
                        break;

                    case ResourceType.White:
                        platformWhite.GetComponent<SpriteRenderer>().sprite = white;
                        textWhite.text = $"{GetAmount(ResourceType.White)}";
                        break;

                    case ResourceType.Ladan:
                        platformLadan.GetComponent<SpriteRenderer>().sprite = ladan;
                        textLadan.text = $"{GetAmount(ResourceType.Ladan)}";
                        break;

                    case ResourceType.Eye:
                        platformEye.GetComponent<SpriteRenderer>().sprite = eye;
                        textEye.text = $"{GetAmount(ResourceType.Eye)}";
                        break;

                    case ResourceType.Stone:
                        platformStone.GetComponent<SpriteRenderer>().sprite = stone;
                        textStone.text = $"{GetAmount(ResourceType.Stone)}";
                        break;

                    case ResourceType.Sand:
                        platformSand.GetComponent<SpriteRenderer>().sprite = sand;
                        textSand.text = $"{GetAmount(ResourceType.Sand)}";
                        break;

                    default:
                        break;
                }
            }
        }
    }

    public void RemoveResource(ResourceType resourceType, int amount)
    {
        foreach (ResourceData res in resources)
        {
            if (res.resourceName == resourceType)
            {
                res.amount -= amount;

                switch (res.resourceName)
                {
                    case ResourceType.Red:
                        if (GetAmount(ResourceType.Red) == 0)
                            platformRed.GetComponent<SpriteRenderer>().sprite = redEmpty;
                        textRed.text = $"{GetAmount(ResourceType.Red)}";
                        break;

                    case ResourceType.Blue:
                        if (GetAmount(ResourceType.Blue) == 0)
                            platformBlue.GetComponent<SpriteRenderer>().sprite = blueEmpty;
                        textBlue.text = $"{GetAmount(ResourceType.Blue)}";
                        break;

                    case ResourceType.Yellow:
                        if (GetAmount(ResourceType.Yellow) == 0)
                            platformYellow.GetComponent<SpriteRenderer>().sprite = yellowEmpty;
                        textYellow.text = $"{GetAmount(ResourceType.Yellow)}";
                        break;

                    case ResourceType.White:
                        if (GetAmount(ResourceType.White) == 0)
                            platformWhite.GetComponent<SpriteRenderer>().sprite = whiteEmpty;
                        textWhite.text = $"{GetAmount(ResourceType.White)}";
                        break;

                    case ResourceType.Ladan:
                        if (GetAmount(ResourceType.Ladan) == 0)
                            platformLadan.GetComponent<SpriteRenderer>().sprite = ladanEmpty;
                        textLadan.text = $"{GetAmount(ResourceType.Ladan)}";
                        break;

                    case ResourceType.Eye:
                        if (GetAmount(ResourceType.Eye) == 0)
                            platformEye.GetComponent<SpriteRenderer>().sprite = eyeEmpty;
                        textEye.text = $"{GetAmount(ResourceType.Eye)}";
                        break;

                    case ResourceType.Stone:
                        if (GetAmount(ResourceType.Stone) == 0)
                            platformStone.GetComponent<SpriteRenderer>().sprite = stoneEmpty;
                        textStone.text = $"{GetAmount(ResourceType.Stone)}";
                        break;

                    case ResourceType.Sand:
                        if (GetAmount(ResourceType.Sand) == 0)
                            platformSand.GetComponent<SpriteRenderer>().sprite = sandEmpty;
                        textSand.text = $"{GetAmount(ResourceType.Sand)}";
                        break;

                    default:
                        break;
                }
            }
        }
    }

    public int GetAmount(ResourceType resourceType)
    {
        int returnAmount = 0;

        foreach (ResourceData res in resources)
        {
            if (res.resourceName == resourceType)
                returnAmount = res.amount;
        }
        return returnAmount;
    }

    public ResourceRarity GetRarity(ResourceType resourceType)
    {
        ResourceRarity returnRarity = ResourceRarity.Common;

        foreach (ResourceData res in resources)
        {
            if (res.resourceName == resourceType)
                returnRarity = res.resourceRarity;
        }
        return returnRarity;
    }
}
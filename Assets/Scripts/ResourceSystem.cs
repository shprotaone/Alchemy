using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum Resource
{
    Red,
    Blue,
    Yellow,
    White,
    Ladan,
    Eye,
    Stone,
    Sand
};

public enum Rarity
{
    Common,
    Rare,
};

public class ResourceData
{
    public Resource resourceName;
    public int amount;
    public Rarity resourceRarity;

    public ResourceData(Resource _resourceName, Rarity _resourceRarity)
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
        ResourceData red = new ResourceData(Resource.Red, Rarity.Common);
        resources.Add(red);
        ResourceData blue = new ResourceData(Resource.Blue, Rarity.Common);
        resources.Add(blue);
        ResourceData yellow = new ResourceData(Resource.Yellow, Rarity.Common);
        resources.Add(yellow);
        ResourceData white = new ResourceData(Resource.White, Rarity.Common);
        resources.Add(white);
        ResourceData ladan = new ResourceData(Resource.Ladan, Rarity.Rare);
        resources.Add(ladan);
        ResourceData eye = new ResourceData(Resource.Eye, Rarity.Rare);
        resources.Add(eye);
        ResourceData stone = new ResourceData(Resource.Stone, Rarity.Rare);
        resources.Add(stone);
        ResourceData sand = new ResourceData(Resource.Sand, Rarity.Rare);
        resources.Add(sand);
    }

    private void Start()
    {
        if (GetAmount(Resource.Red) == 0)
            platformRed.GetComponent<SpriteRenderer>().sprite = redEmpty;
        if (GetAmount(Resource.Blue) == 0)
            platformBlue.GetComponent<SpriteRenderer>().sprite = blueEmpty;
        if (GetAmount(Resource.Yellow) == 0)
            platformYellow.GetComponent<SpriteRenderer>().sprite = yellowEmpty;
        if (GetAmount(Resource.White) == 0)
            platformWhite.GetComponent<SpriteRenderer>().sprite = whiteEmpty;
        if (GetAmount(Resource.Ladan) == 0)
            platformLadan.GetComponent<SpriteRenderer>().sprite = ladanEmpty;
        if (GetAmount(Resource.Eye) == 0)
            platformEye.GetComponent<SpriteRenderer>().sprite = eyeEmpty;
        if (GetAmount(Resource.Stone) == 0)
            platformStone.GetComponent<SpriteRenderer>().sprite = stoneEmpty;
        if (GetAmount(Resource.Sand) == 0)
            platformSand.GetComponent<SpriteRenderer>().sprite = sandEmpty;
    }

    public void AddResource(Resource resourceType, int amount)
    {
        foreach (ResourceData res in resources)
        {
            if (res.resourceName == resourceType)
            {
                res.amount += amount;

                switch (res.resourceName)
                {
                    case Resource.Red:
                        platformRed.GetComponent<SpriteRenderer>().sprite = red;
                        textRed.text = $"{GetAmount(Resource.Red)}";
                        break;

                    case Resource.Blue:
                        platformBlue.GetComponent<SpriteRenderer>().sprite = blue;
                        textBlue.text = $"{GetAmount(Resource.Blue)}";
                        break;

                    case Resource.Yellow:
                        platformYellow.GetComponent<SpriteRenderer>().sprite = yellow;
                        textYellow.text = $"{GetAmount(Resource.Yellow)}";
                        break;

                    case Resource.White:
                        platformWhite.GetComponent<SpriteRenderer>().sprite = white;
                        textWhite.text = $"{GetAmount(Resource.White)}";
                        break;

                    case Resource.Ladan:
                        platformLadan.GetComponent<SpriteRenderer>().sprite = ladan;
                        textLadan.text = $"{GetAmount(Resource.Ladan)}";
                        break;

                    case Resource.Eye:
                        platformEye.GetComponent<SpriteRenderer>().sprite = eye;
                        textEye.text = $"{GetAmount(Resource.Eye)}";
                        break;

                    case Resource.Stone:
                        platformStone.GetComponent<SpriteRenderer>().sprite = stone;
                        textStone.text = $"{GetAmount(Resource.Stone)}";
                        break;

                    case Resource.Sand:
                        platformSand.GetComponent<SpriteRenderer>().sprite = sand;
                        textSand.text = $"{GetAmount(Resource.Sand)}";
                        break;

                    default:
                        break;
                }
            }
        }
    }

    public void RemoveResource(Resource resourceType, int amount)
    {
        foreach (ResourceData res in resources)
        {
            if (res.resourceName == resourceType)
            {
                res.amount -= amount;

                switch (res.resourceName)
                {
                    case Resource.Red:
                        if (GetAmount(Resource.Red) == 0)
                            platformRed.GetComponent<SpriteRenderer>().sprite = redEmpty;
                        textRed.text = $"{GetAmount(Resource.Red)}";
                        break;

                    case Resource.Blue:
                        if (GetAmount(Resource.Blue) == 0)
                            platformBlue.GetComponent<SpriteRenderer>().sprite = blueEmpty;
                        textBlue.text = $"{GetAmount(Resource.Blue)}";
                        break;

                    case Resource.Yellow:
                        if (GetAmount(Resource.Yellow) == 0)
                            platformYellow.GetComponent<SpriteRenderer>().sprite = yellowEmpty;
                        textYellow.text = $"{GetAmount(Resource.Yellow)}";
                        break;

                    case Resource.White:
                        if (GetAmount(Resource.White) == 0)
                            platformWhite.GetComponent<SpriteRenderer>().sprite = whiteEmpty;
                        textWhite.text = $"{GetAmount(Resource.White)}";
                        break;

                    case Resource.Ladan:
                        if (GetAmount(Resource.Ladan) == 0)
                            platformLadan.GetComponent<SpriteRenderer>().sprite = ladanEmpty;
                        textLadan.text = $"{GetAmount(Resource.Ladan)}";
                        break;

                    case Resource.Eye:
                        if (GetAmount(Resource.Eye) == 0)
                            platformEye.GetComponent<SpriteRenderer>().sprite = eyeEmpty;
                        textEye.text = $"{GetAmount(Resource.Eye)}";
                        break;

                    case Resource.Stone:
                        if (GetAmount(Resource.Stone) == 0)
                            platformStone.GetComponent<SpriteRenderer>().sprite = stoneEmpty;
                        textStone.text = $"{GetAmount(Resource.Stone)}";
                        break;

                    case Resource.Sand:
                        if (GetAmount(Resource.Sand) == 0)
                            platformSand.GetComponent<SpriteRenderer>().sprite = sandEmpty;
                        textSand.text = $"{GetAmount(Resource.Sand)}";
                        break;

                    default:
                        break;
                }
            }
        }
    }

    public int GetAmount(Resource resourceType)
    {
        int returnAmount = 0;

        foreach (ResourceData res in resources)
        {
            if (res.resourceName == resourceType)
                returnAmount = res.amount;
        }
        return returnAmount;
    }

    public Rarity GetRarity(Resource resourceType)
    {
        Rarity returnRarity = Rarity.Common;

        foreach (ResourceData res in resources)
        {
            if (res.resourceName == resourceType)
                returnRarity = res.resourceRarity;
        }
        return returnRarity;
    }
}
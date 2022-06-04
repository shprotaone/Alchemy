using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PotionColor
{
    Empty,
    Purple,
    Orange,
    Green,
    Pink,
    Gold,
    LightBlue,
    Black,
    Gray,
    Peach,
    Violet,
    Lime
}

public enum PotionEffect
{
    Empty,
    Normal,
    Glowing,
    Boiling,
    Burning,
    Smoking
}

public class BottlesData
{
    public int id;
    public PotionColor potionColor;
    public PotionEffect potionEffect;

    public BottlesData(int _id, PotionColor _potionColor, PotionEffect _potionEffect)
    {
        id = _id;
        potionColor = _potionColor;
        potionEffect = _potionEffect;
    }
}

public class PotionSystem : MonoBehaviour
{
    public int id;
    public PotionEffect bottleColor;
    public PotionColor bottleEffect;

    public List<BottlesData> bottles = new List<BottlesData>();

    private void Awake()
    {
        BottlesData bottle1 = new BottlesData(1, PotionColor.Empty, PotionEffect.Empty);
        bottles.Add(bottle1);
        BottlesData bottle2 = new BottlesData(2, PotionColor.Empty, PotionEffect.Empty);
        bottles.Add(bottle2);
        BottlesData bottle3 = new BottlesData(3, PotionColor.Empty, PotionEffect.Empty);
        bottles.Add(bottle3);
        BottlesData bottle4 = new BottlesData(4, PotionColor.Empty, PotionEffect.Empty);
        bottles.Add(bottle4);
        BottlesData bottle5 = new BottlesData(5, PotionColor.Empty, PotionEffect.Empty);
        bottles.Add(bottle5);
        BottlesData bottle6 = new BottlesData(6, PotionColor.Empty, PotionEffect.Empty);
        bottles.Add(bottle6);
        BottlesData bottle7 = new BottlesData(7, PotionColor.Empty, PotionEffect.Empty);
        bottles.Add(bottle7);
        BottlesData bottle8 = new BottlesData(8, PotionColor.Empty, PotionEffect.Empty);
        bottles.Add(bottle8);
    }

    public void ChangePotion(PotionColor color, PotionEffect effect, UnityEngine.GameObject bottle)
    {
        #region сверка с текущей бутылкой? 
        switch (bottle.tag)
        {
            case "Bottle1":
                id = 1;
                break;
            case "Bottle2":
                id = 2;
                break;
            case "Bottle3":
                id = 3;
                break;
            case "Bottle4":
                id = 4;
                break;
            case "Bottle5":
                id = 5;
                break;
            case "Bottle6":
                id = 6;
                break;
            case "Bottle7":
                id = 7;
                break;
            case "Bottle8":
                id = 8;
                break;
            default:
                break;
        }
        #endregion

        foreach (var item in bottles)
        {
            if (id == item.id)
            {
                #region сверка текущей бутылки с цветом
                switch (color)
                {
                    case PotionColor.Empty:
                        item.potionColor = PotionColor.Empty;
                        break;
                    case PotionColor.Purple:
                        item.potionColor = PotionColor.Purple;
                        break;
                    case PotionColor.Orange:
                        item.potionColor = PotionColor.Orange;
                        break;
                    case PotionColor.Green:
                        item.potionColor = PotionColor.Green;
                        break;
                    case PotionColor.Pink:
                        item.potionColor = PotionColor.Pink;
                        break;
                    case PotionColor.Gold:
                        item.potionColor = PotionColor.Gold;
                        break;
                    case PotionColor.LightBlue:
                        item.potionColor = PotionColor.LightBlue;
                        break;
                    case PotionColor.Black:
                        item.potionColor = PotionColor.Black;
                        break;
                    case PotionColor.Gray:
                        item.potionColor = PotionColor.Gray;
                        break;
                    case PotionColor.Peach:
                        item.potionColor = PotionColor.Peach;
                        break;
                    case PotionColor.Violet:
                        item.potionColor = PotionColor.Violet;
                        break;
                    case PotionColor.Lime:
                        item.potionColor = PotionColor.Lime;
                        break;
                    default:
                        break;
                }
                #endregion

                #region сверка с текущим эффектом?
                switch (effect)
                {
                    case PotionEffect.Empty:
                        item.potionEffect = PotionEffect.Empty;
                        break;
                    case PotionEffect.Normal:
                        item.potionEffect = PotionEffect.Normal;
                        break;
                    case PotionEffect.Glowing:
                        item.potionEffect = PotionEffect.Glowing;
                        break;
                    case PotionEffect.Boiling:
                        item.potionEffect = PotionEffect.Boiling;
                        break;
                    case PotionEffect.Burning:
                        item.potionEffect = PotionEffect.Burning;
                        break;
                    case PotionEffect.Smoking:
                        item.potionEffect = PotionEffect.Smoking;
                        break;
                    default:
                        break;
                }
                #endregion
            }
        }
    }

    public int GetColor(int id)
    {
        return (int)bottles[id].potionColor;
    }

    public int GetEffect(int id)
    {
        return (int)bottles[id].potionEffect;
    }

    public void SetColor(int id, PotionColor color)
    {
        bottles[id].potionColor = color;
    }

    public void SetEffect(int id, PotionEffect effect)
    {
        bottles[id].potionEffect = effect;
    }
}
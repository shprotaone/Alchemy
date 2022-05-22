using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RecipesMenu : MonoBehaviour
{
    public GameObject potionData;
    public GameObject content;
    public GameObject UIControls;
    public Settings settings;
    public Potionv1[] potions;

    public Sprite burning;
    public Sprite smoking;
    public Sprite glowing;
    public Sprite boiling;

    public bool[] addedArr = new bool[55];
    public bool pass = false;

    public void AddPotionData(Potionv1 potion)
    {
        if (!addedArr[potion.id] || pass)
        {
            addedArr[potion.id] = true;

            RectTransform rt = content.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y + 250);

            GameObject data = Instantiate(potionData);
            data.transform.Find("Bottle").GetComponent<Image>().sprite = potion.imageBottle;
            data.transform.Find("Water").GetComponent<Image>().sprite = potion.imageWater;
            switch (potion.color)
            {
                case PotionColor.Purple:
                    data.transform.Find("Water").GetComponent<Image>().color = settings.colors[0];
                    data.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = settings.colorNames[0];
                    break;
                case PotionColor.Orange:
                    data.transform.Find("Water").GetComponent<Image>().color = settings.colors[1];
                    data.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = settings.colorNames[1];
                    break;
                case PotionColor.Green:
                    data.transform.Find("Water").GetComponent<Image>().color = settings.colors[2];
                    data.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = settings.colorNames[2];
                    break;
                case PotionColor.Pink:
                    data.transform.Find("Water").GetComponent<Image>().color = settings.colors[3];
                    data.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = settings.colorNames[3];
                    break;
                case PotionColor.Gold:
                    data.transform.Find("Water").GetComponent<Image>().color = settings.colors[4];
                    data.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = settings.colorNames[4];
                    break;
                case PotionColor.LightBlue:
                    data.transform.Find("Water").GetComponent<Image>().color = settings.colors[5];
                    data.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = settings.colorNames[5];
                    break;
                case PotionColor.Black:
                    data.transform.Find("Water").GetComponent<Image>().color = settings.colors[6];
                    data.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = settings.colorNames[6];
                    break;
                case PotionColor.Gray:
                    data.transform.Find("Water").GetComponent<Image>().color = settings.colors[7];
                    data.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = settings.colorNames[7];
                    break;
                case PotionColor.Peach:
                    data.transform.Find("Water").GetComponent<Image>().color = settings.colors[8];
                    data.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = settings.colorNames[8];
                    break;
                case PotionColor.Violet:
                    data.transform.Find("Water").GetComponent<Image>().color = settings.colors[9];
                    data.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = settings.colorNames[9];
                    break;
                case PotionColor.Lime:
                    data.transform.Find("Water").GetComponent<Image>().color = settings.colors[10];
                    data.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = settings.colorNames[10];
                    break;
                default:
                    break;
            }

            if (potion.rare)
            {
                switch (potion.effect)
                {
                    case PotionEffect.Glowing:
                        data.transform.Find("Name").GetComponent<TextMeshProUGUI>().text += " " + settings.effectNames[0] + " зелье";
                        data.transform.Find("Effect").GetComponent<Image>().sprite = glowing;
                        break;
                    case PotionEffect.Boiling:
                        data.transform.Find("Name").GetComponent<TextMeshProUGUI>().text += " " + settings.effectNames[1] + " зелье";
                        data.transform.Find("Effect").GetComponent<Image>().sprite = boiling;
                        break;
                    case PotionEffect.Burning:
                        data.transform.Find("Name").GetComponent<TextMeshProUGUI>().text += " " + settings.effectNames[2] + " зелье";
                        data.transform.Find("Effect").GetComponent<Image>().sprite = burning;
                        break;
                    case PotionEffect.Smoking:
                        data.transform.Find("Name").GetComponent<TextMeshProUGUI>().text += " " + settings.effectNames[3] + " зелье";
                        data.transform.Find("Effect").GetComponent<Image>().sprite = smoking;
                        break;
                    default:
                        break;
                }
                data.transform.Find("Rare").gameObject.SetActive(true);
            }
            else
            {
                data.transform.Find("Name").GetComponent<TextMeshProUGUI>().text += " зелье";
                data.transform.Find("Effect").gameObject.SetActive(false);
            }
            data.transform.Find("Rare").GetComponent<Image>().sprite = potion.rare;

            switch (potion.colored.Length)
            {
                case 2:
                    data.transform.Find("Duo1").gameObject.SetActive(true);
                    data.transform.Find("Duo1").GetComponent<Image>().sprite = potion.colored[0];
                    data.transform.Find("Duo2").gameObject.SetActive(true);
                    data.transform.Find("Duo2").GetComponent<Image>().sprite = potion.colored[1];
                    break;

                case 3:
                    data.transform.Find("Triple1").gameObject.SetActive(true);
                    data.transform.Find("Triple1").GetComponent<Image>().sprite = potion.colored[0];
                    data.transform.Find("Triple2").gameObject.SetActive(true);
                    data.transform.Find("Triple2").GetComponent<Image>().sprite = potion.colored[1];
                    data.transform.Find("Triple3").gameObject.SetActive(true);
                    data.transform.Find("Triple3").GetComponent<Image>().sprite = potion.colored[2];
                    break;

                case 4:
                    data.transform.Find("Quad1").gameObject.SetActive(true);
                    data.transform.Find("Quad1").GetComponent<Image>().sprite = potion.colored[0];
                    data.transform.Find("Quad2").gameObject.SetActive(true);
                    data.transform.Find("Quad2").GetComponent<Image>().sprite = potion.colored[1];
                    data.transform.Find("Quad3").gameObject.SetActive(true);
                    data.transform.Find("Quad3").GetComponent<Image>().sprite = potion.colored[2];
                    data.transform.Find("Quad4").gameObject.SetActive(true);
                    data.transform.Find("Quad4").GetComponent<Image>().sprite = potion.colored[3];
                    break;

                default:
                    break;
            }

            data.transform.SetParent(content.transform);
            data.transform.localScale = new Vector3(UIControls.GetComponent<Scaling>().tableBG.transform.localScale.x / 1.736f, 1, 1);
        }
    }
}
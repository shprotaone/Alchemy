using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottlePotion : MonoBehaviour
{
    public UnityEngine.GameObject potionSystem;
    public UnityEngine.GameObject[] potionPos;

    public Settings settings;

    public PotionColor potionColor = PotionColor.Empty;
    public PotionEffect potionEffect = PotionEffect.Empty;

    public int takenSpace = -1;

    public bool justDrained = false;
    public bool justGiven = false;

    private void Start()
    {
        int id;
        #region //выбор индекса бутылки по его наименованию? 
        switch (tag)        
        {
            case "Bottle1":
                id = 0;
                break;
            case "Bottle2":
                id = 1;
                break;
            case "Bottle3":
                id = 2;
                break;
            case "Bottle4":
                id = 3;
                break;
            case "Bottle5":
                id = 4;
                break;
            case "Bottle6":
                id = 5;
                break;
            case "Bottle7":
                id = 6;
                break;
            case "Bottle8":
                id = 7;
                break;
            default:
                id = 0;
                break;
        }
        #endregion


        for (int i = 0; i < potionPos.Length; i++)      // присвоение id взятой бутылки, зачем? 
        {
            if (transform.position == potionPos[i].transform.position)
            {
                takenSpace = i;
                break;
            }
        }
        #region Окрас бутылки? и его свечение
        potionColor = (PotionColor)potionSystem.GetComponent<PotionSystem>().GetColor(id);
        potionEffect = (PotionEffect)potionSystem.GetComponent<PotionSystem>().GetEffect(id);

        if (potionColor != PotionColor.Empty)
        {
            transform.Find("Water").gameObject.SetActive(true);
            transform.Find("Water").GetComponent<SpriteRenderer>().color = settings.colors[potionSystem.GetComponent<PotionSystem>().GetColor(id) - 1];
        }

        if (potionEffect == PotionEffect.Burning)
            transform.Find("PSMask").Find("FirePS").GetComponent<ParticleSystem>().Play();
        if (potionEffect == PotionEffect.Boiling)
            transform.Find("PSMask").Find("BoilPS").GetComponent<ParticleSystem>().Play();
        if (potionEffect == PotionEffect.Smoking)
            transform.Find("PSMask").Find("SmokePS").GetComponent<ParticleSystem>().Play();
        if (potionEffect == PotionEffect.Glowing)
            transform.Find("PSMask").Find("GlowPS").GetComponent<ParticleSystem>().Play();
        #endregion
    }

    public void AddPotion(List<ResourceType> inCauldron, List<ResourceType> inCauldronColored, bool isRare, int space)
    {
        takenSpace = space;

        if (isRare)
        {
            foreach (var item in inCauldron)
            {
                if (item == ResourceType.Ladan)
                    potionEffect = PotionEffect.Glowing;
                if (item == ResourceType.Eye)
                    potionEffect = PotionEffect.Boiling;
                if (item == ResourceType.Stone)
                    potionEffect = PotionEffect.Burning;
                if (item == ResourceType.Sand)
                    potionEffect = PotionEffect.Smoking;
            }
        }
        else
            potionEffect = PotionEffect.Normal;

        switch (inCauldronColored.Count)
        {
            case 2:
                if (inCauldronColored.Contains(ResourceType.Red) && inCauldronColored.Contains(ResourceType.Blue))
                    potionColor = PotionColor.Purple;

                if (inCauldronColored.Contains(ResourceType.Red) && inCauldronColored.Contains(ResourceType.Yellow))
                    potionColor = PotionColor.Orange;

                if (inCauldronColored.Contains(ResourceType.Blue) && inCauldronColored.Contains(ResourceType.Yellow))
                    potionColor = PotionColor.Green;

                if (inCauldronColored.Contains(ResourceType.Red) && inCauldronColored.Contains(ResourceType.White))
                    potionColor = PotionColor.Pink;

                if (inCauldronColored.Contains(ResourceType.Blue) && inCauldronColored.Contains(ResourceType.White))
                    potionColor = PotionColor.LightBlue;

                if (inCauldronColored.Contains(ResourceType.Yellow) && inCauldronColored.Contains(ResourceType.White))
                    potionColor = PotionColor.Gold;
                break;
            case 3:
                if (inCauldronColored.Contains(ResourceType.Red) && inCauldronColored.Contains(ResourceType.Blue) && inCauldronColored.Contains(ResourceType.White))
                    potionColor = PotionColor.Violet;

                if (inCauldronColored.Contains(ResourceType.Red) && inCauldronColored.Contains(ResourceType.Yellow) && inCauldronColored.Contains(ResourceType.White))
                    potionColor = PotionColor.Peach;

                if (inCauldronColored.Contains(ResourceType.Blue) && inCauldronColored.Contains(ResourceType.Yellow) && inCauldronColored.Contains(ResourceType.White))
                    potionColor = PotionColor.Lime;

                if (inCauldronColored.Contains(ResourceType.Red) && inCauldronColored.Contains(ResourceType.Blue) && inCauldronColored.Contains(ResourceType.Yellow))
                    potionColor = PotionColor.Black;
                break;
            case 4:
                potionColor = PotionColor.Gray;
                break;
            default:
                break;
        }
        potionSystem.GetComponent<PotionSystem>().ChangePotion(potionColor, potionEffect, this.gameObject);
    }
}
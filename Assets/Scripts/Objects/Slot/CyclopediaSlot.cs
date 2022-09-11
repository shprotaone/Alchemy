﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CyclopediaSlot : MonoBehaviour
{
    [SerializeField] private TMP_Text _potionName;
    [SerializeField] private TMP_Text _potionRarity;
    [SerializeField] private TMP_Text _potionGuild;
    [SerializeField] private Image[] _ingredientImages;

    public string PotionName => _potionName.text;

    public void FillSlot(Potion potion, Sprite[] ingredientSprite)
    {
        _potionName.text = potion.PotionName;
        _potionRarity.text = potion.Rarity.ToString();
        _potionGuild.text = potion.GuildsType.ToString();

        for (int i = 0; i < ingredientSprite.Length; i++)
        {
            _ingredientImages[i].sprite = ingredientSprite[i];

            if(_ingredientImages[i].sprite == null)
            {
                _ingredientImages[i].enabled = false;
            }
        }
    }
}

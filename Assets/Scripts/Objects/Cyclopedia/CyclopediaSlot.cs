using System.Collections;
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
    [SerializeField] private Toggle _toggle;

    public Potion PotionInSlot { get; private set; }
    public bool Complete { get; private set; }
    public void SetComplete()
    {
        Complete = true;
        _toggle.isOn = Complete;
    }

    public void FillSlot(Potion potion, Sprite[] ingredientSprite)
    {
        PotionInSlot = potion;
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

    public void FillSlot(PotionData potion, Sprite[] ingredientSprite)
    {
        PotionInSlot = new Potion(potion);

        _potionName.text = potion.name;
        _potionRarity.text = potion.rarity;
        _potionGuild.text = potion.guild;

        for (int i = 0; i < ingredientSprite.Length; i++)
        {
            _ingredientImages[i].enabled = true;
            _ingredientImages[i].sprite = ingredientSprite[i];
        }
    }
}

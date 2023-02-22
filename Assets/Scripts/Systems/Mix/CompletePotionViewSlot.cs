using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CompletePotionViewSlot : MonoBehaviour
{
    [SerializeField] private List<Image> _images;
    [SerializeField] private TMP_Text _countText;

    public bool IsFree { get; private set; }

    public void SetSlot(BottleModel bottle)
    {
        for (int i = 0; i < _images.Count; i++)
        {
            if (i < bottle.PotionInBottle.Labels.Count)
            {
                _images[i].enabled = true;
                _images[i].sprite = bottle.View.LabelSprites[i].sprite;
            }
            else
            {
                _images[i].enabled = false;
            }
        }
    }

    public void SetSlot(FullBottleSlot slot)
    {
        IsFree = false;
        
        if (slot.IsFree)
        {
            ClearSlot();
        }
        else
        {
            gameObject.SetActive(true);

            for (int i = 0; i < _images.Count; i++)
            {
                if (i < slot.BottleInSlot.PotionInBottle.Labels.Count)
                {
                    _images[i].enabled = true;
                    _images[i].sprite = slot.BottleInSlot.View.LabelSprites[i].sprite;
                }
                else
                {
                    _images[i].enabled = false;
                }
            }
        }
        

        _countText.text = slot.Count.ToString();
    }

    public void ClearSlot()
    {
        IsFree = true;

        foreach (var image in _images)
        {
            image.enabled = false;
        }
    }
}

#region OldVersion

//[SerializeField] private TMP_Text _potionName;
//[SerializeField] private TMP_Text _potionRarity;
//[SerializeField] private TMP_Text _potionGuild;

//public void FillSlot(Potion potion, Sprite[] ingredientSprite)
//{
//    PotionInSlot = potion;
//    _potionName.text = potion.PotionName;
//    _potionRarity.text = potion.Rarity.ToString();
//    _potionGuild.text = potion.GuildsType.ToString();

//    for (int i = 0; i < ingredientSprite.Length; i++)
//    {
//        _ingredientImages[i].sprite = ingredientSprite[i];

//        if(_ingredientImages[i].sprite == null)
//        {
//            _ingredientImages[i].enabled = false;
//        }
//    }
//}

//public void FillSlot(PotionData potion, Sprite[] ingredientSprite)
//{
//    PotionInSlot = new Potion(potion);

//    _potionName.text = potion.name;
//    _potionRarity.text = potion.rarity;
//    _potionGuild.text = potion.guild;

//    for (int i = 0; i < ingredientSprite.Length; i++)
//    {
//        _ingredientImages[i].enabled = true;
//        _ingredientImages[i].sprite = ingredientSprite[i];
//    }
//}
#endregion

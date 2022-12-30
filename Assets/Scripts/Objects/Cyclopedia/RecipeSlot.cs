using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeSlot : MonoBehaviour
{
    [SerializeField] private Image _firstIngredient;
    [SerializeField] private Image _secondIngredient;
    [SerializeField] private Image _resultImage;
    [SerializeField] private Toggle _toggle;

    public Potion PotionInSlot { get; private set; }
    public bool Complete { get; private set; }
    public void SetComplete()
    {
        Complete = true;
        _toggle.isOn = Complete;
    }

   public void SetSlot(Sprite firstImage,Sprite secondImage,Sprite result)
    {
        _firstIngredient.sprite = firstImage;
        _secondIngredient.sprite = secondImage;
        _resultImage.sprite = result;
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

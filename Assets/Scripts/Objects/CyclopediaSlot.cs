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

    private bool _isOpen =false;

    public bool IsOpen => _isOpen;
    public string PotionName => _potionName.text;
    private void Start()
    {
        _ingredientImages = GetComponentsInChildren<Image>();       
    }

    public void FillSlot(string potionName,ResourceRarity rarity,GuildsType guild,Sprite[] ingredientSprite)
    {
        _potionName.text = potionName;
        _potionRarity.text = rarity.ToString();
        _potionGuild.text = guild.ToString();

        for (int i = 0; i < ingredientSprite.Length; i++)
        {
            _ingredientImages[i].sprite = ingredientSprite[i];

            if(_ingredientImages[i].sprite == null)
            {
                _ingredientImages[i].enabled = false;
            }
        }

        CheckOpenSlot();
    }

    public void OpenSlot()
    {
        _isOpen = true;
        this.gameObject.SetActive(_isOpen);
    }

    private void CheckOpenSlot()
    {
        if (!_isOpen)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }
}

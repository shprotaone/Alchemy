using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionCyclopedia : MonoBehaviour
{
    [SerializeField] private JSONReader _jsonReader;
    [SerializeField] private InGameTimeController _gameTimeController;
    [SerializeField] private StringToSprite _imageConverter;
    [SerializeField] private Transform _cyclopediaTransform;

    private CyclopediaSlot[] _cyclopedia;
    private PotionSizer _potionSizer;
    private GuildChecker _guildChecker;
    private RarityChecker _rarityChecker;

    private string[] _ingredients;

    private void Start()
    {
        _potionSizer = _jsonReader.PotionSizer;
        _cyclopedia = _cyclopediaTransform.GetComponentsInChildren<CyclopediaSlot>();
        _guildChecker = new GuildChecker();
        _rarityChecker = new RarityChecker();

        if(_cyclopedia.Length != 0)
        FillCyclopedia();
    }

    private void FillCyclopedia()
    {
        for (int i = 0; i < _potionSizer.Potions.Length; i++)
        {
            _ingredients = _imageConverter.GetNameIngredients(_potionSizer.Potions[i].firstIngredient,
                                                              _potionSizer.Potions[i].secondIngredient,
                                                              _potionSizer.Potions[i].threeIngredient,
                                                              _potionSizer.Potions[i].fourIngredient,
                                                              _potionSizer.Potions[i].fiveIngredient);

            _cyclopedia[i].FillSlot(_potionSizer.Potions[i].name,
                                    _rarityChecker.RarityCheck(_potionSizer.Potions[i].rarity),
                                    _guildChecker.GuildCheck(_potionSizer.Potions[i].guild),
                                    GetImages(_ingredients));
        }
    }

    private Sprite[] GetImages(string[] value)
    {
        Sprite[] result = new Sprite[5];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = _imageConverter.ParseStringToSprite(value[i]);
        }

        return result;
    }

    public void FindPotion(string name)
    {
        for (int i = 0; i < _cyclopedia.Length; i++)
        {
            if(_cyclopedia[i].PotionName == name)
            {
                _cyclopedia[i].OpenSlot();
            }
        }
    }
}

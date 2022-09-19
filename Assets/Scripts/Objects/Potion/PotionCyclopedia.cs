using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionCyclopedia : MonoBehaviour
{
    public static Action<Potion> OnPotionCyclopediaAdded;

    [SerializeField] private JSONReader _jsonReader;
    [SerializeField] private StringToSprite _imageConverter;
    [SerializeField] private Transform _cyclopediaTransform;
    [SerializeField] private List<CyclopediaSlot> _cyclopediaSlots;
    [SerializeField] private GameObject _cyclopediaSlotPrefab;

    private PotionSizer _potionSizer;
    private GuildChecker _guildChecker;
    private RarityChecker _rarityChecker;

    private List<string> _ingredients;

    private void Start()
    {
        _potionSizer = _jsonReader.PotionSizer;
        _guildChecker = new GuildChecker();
        _rarityChecker = new RarityChecker();

        _cyclopediaSlots = new List<CyclopediaSlot>();
    }

    public void AddNewPotion(Potion potion)
    {
        GameObject slotGO = Instantiate(_cyclopediaSlotPrefab, _cyclopediaTransform);
        CyclopediaSlot slot = slotGO.GetComponent<CyclopediaSlot>();
        
        _cyclopediaSlots.Add(slot);
        _ingredients = potion.Ingredients;

        slot.FillSlot(potion,GetImages(_ingredients));
    }


    //private void FillCyclopedia()
    //{
    //    for (int i = 0; i < _potionSizer.Potions.Length; i++)
    //    {
    //        _ingredients = _imageConverter.GetNameIngredients(_potionSizer.Potions[i].firstIngredient,
    //                                                          _potionSizer.Potions[i].secondIngredient,
    //                                                          _potionSizer.Potions[i].threeIngredient,
    //                                                          _potionSizer.Potions[i].fourIngredient,
    //                                                          _potionSizer.Potions[i].fiveIngredient);

    //        _cyclopediaSlots[i].FillSlot(_potionSizer.Potions[i].name,
    //                                _rarityChecker.RarityCheck(_potionSizer.Potions[i].rarity),
    //                                _guildChecker.GuildCheck(_potionSizer.Potions[i].guild),
    //                                GetImages(_ingredients));
    //    }
    //}

    private Sprite[] GetImages(List<string> value)
    {
        Sprite[] result = new Sprite[value.Count];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = _imageConverter.ParseStringToSprite(value[i]);
        }

        return result;
    }

    //public void FindPotion(string name)
    //{
    //    for (int i = 0; i < _cyclopediaSlots.Length; i++)
    //    {
    //        if (_cyclopediaSlots[i].PotionName == name)
    //        {
    //            _cyclopediaSlots[i].OpenSlot();
    //        }
    //    }
    //}
}

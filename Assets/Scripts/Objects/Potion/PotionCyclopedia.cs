using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionCyclopedia : MonoBehaviour
{
    [SerializeField] private JSONReader _jsonReader;
    [SerializeField] private StringToSprite _imageConverter;
    [SerializeField] private Transform _cyclopediaTransform;
    [SerializeField] private PotionTaskSystem _potionTaskSystem;
    [SerializeField] private List<CyclopediaSlot> _cyclopediaSlots;
    [SerializeField] private GameObject _cyclopediaSlotPrefab;

    private PotionSizer _potionSizer;
    private GuildChecker _guildChecker;
    private RarityChecker _rarityChecker;

    private List<string> _ingredients;

    public void InitPotionCyclopedia()
    {
        _potionSizer = _potionTaskSystem.PotionSizer;
        _guildChecker = new GuildChecker();
        _rarityChecker = new RarityChecker();

        FillCyclopediaInStart();
    }

    /// <summary>
    /// для добавления новых Зелий
    /// </summary>
    /// <param name="potion"></param>
    public void AddNewPotion(Potion potion)
    {
        GameObject slotGO = Instantiate(_cyclopediaSlotPrefab, _cyclopediaTransform);
        CyclopediaSlot slot = slotGO.GetComponent<CyclopediaSlot>();
        
        _cyclopediaSlots.Add(slot);
        _ingredients = potion.Ingredients;

        slot.FillSlot(potion,GetImages(_ingredients));
    }


    private void FillCyclopediaInStart()
    {
        for (int i = 0; i < _potionSizer.Potions.Length; i++)
        {
            _ingredients = _potionSizer.Potions[i].ingredients;

            _cyclopediaSlots[i].gameObject.SetActive(true);

            _cyclopediaSlots[i].FillSlot(_potionSizer.Potions[i],
                                    GetImages(_ingredients));
        }
    }

    private Sprite[] GetImages(List<string> value)
    {
        Sprite[] result = new Sprite[value.Count];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = _imageConverter.ParseStringToSprite(value[i]);
        }

        return result;
    }
}

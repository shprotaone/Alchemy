﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class PotionTaskList : MonoBehaviour
{
    [SerializeField] private PotionTaskSystem _potionTaskSystem;
    [SerializeField] private BottleInventory _bottleInventory;

    [SerializeField] private JSONReader _jsonReader;
    [SerializeField] private StringToSprite _imageConverter;
    
    [SerializeField] private List<CompletePotionViewSlot> _cyclopediaSlots;
    [SerializeField] private GameObject _cyclopediaSlotPrefab;

    private List<CompletePotionViewSlot> _activeSlots;
    private PotionSizer _taskPotionSizer;
    private GuildChecker _guildChecker;
    private RarityChecker _rarityChecker;

    private List<string> _ingredients;
    public bool CyclopediaComplete { get; set; }

    public void InitPotionCyclopedia()
    {
        //_taskPotionSizer = _potionTaskSystem.PotionSizer;
        _guildChecker = new GuildChecker();
        _rarityChecker = new RarityChecker();

        SetActiveSlots();
    }

    private void SetActiveSlots()
    {
        _activeSlots = new List<CompletePotionViewSlot>();

        foreach (var slot in _cyclopediaSlots)
        {
            if (slot.gameObject.activeSelf)
            {
                _activeSlots.Add(slot);
            }
        }
    }

    private void SearchCompleteSlot(Potion potion)
    {
        //foreach (var slot in _activeSlots)
        //{
        //    if(slot.PotionInSlot.PotionName == potion.PotionName)
        //    {
        //        slot.SetComplete();
        //        CheckCyclopediaComplete();
        //        return;
        //    }
        //}
    }

    //private void FillCyclopediaInStart()
    //{
    //    for (int i = 0; i < _taskPotionSizer.Potions.Length; i++)
    //    {
    //        _ingredients = _taskPotionSizer.Potions[i].ingredients;

    //        _cyclopediaSlots[i].gameObject.SetActive(true);

    //        _cyclopediaSlots[i].FillSlot(_taskPotionSizer.Potions[i],
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
}
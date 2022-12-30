using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeSystem : MonoBehaviour
{
    [SerializeField] private ResourceToSprite _resourceToSprite;
    [SerializeField] private RecipeSlot[] _slots;

    private LabelSetter _labelSetter;

    private void Start()
    {
        FillRecipes();
        //_labelSetter = new LabelSetter();
    }

    private void FillRecipes()
    {
       // int count = 0;
       //foreach (var slot in _slots)
       // {
       //     slot.SetSlot(GetSprites(count));
       //     count++;
       // }
    }

    //private Sprite[] GetSprites(int count)
    //{
    //    Sprite[] sprites = new Sprite[3];

    //    sprites[0] = 
    //}
}

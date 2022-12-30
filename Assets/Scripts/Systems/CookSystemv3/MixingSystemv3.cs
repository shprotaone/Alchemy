using System;
using System.Collections.Generic;
using UnityEngine;

public class MixingSystemv3 : MonoBehaviour
{
    private const int maxMixColor = 4;

    public delegate void RefreshCountIngredient();
    public RefreshCountIngredient ActiveButtonBrewDelegate;

    public delegate void FilledBottle();
    public FilledBottle FilledBottleDelegateForTutorial;

    public static event Action OnIngredientAdded;

    [SerializeField] private BottleInventory _bottleInventory;
    [SerializeField] private List<Ingredient> _ingredients;
    [SerializeField] private ClaudronSystem _claudronSystem;
    [SerializeField] private ClaudronLabelView _claudronLabelView;
    [SerializeField] private WaterColorv2 _waterColor;
    [SerializeField] private Cookv2 _cook;

    private Potion _potionInClaudron;
    private LabelSetter _labelSetter;
    public List<Ingredient> IngredientsInClaudron { get; private set; }

    private void Start()
    {
        IngredientsInClaudron = new List<Ingredient>();  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Ingredient ingredient))
        {
            AddIngredientToClaudron(ingredient);
        }
        else if(collision.TryGetComponent(out Bottle bottle))
        {
            FillBottle(bottle);
        }
    }

    private void FillBottle(Bottle bottle)
    {
        if (!bottle.IsFull && IngredientsInClaudron.Count != 0 && _cook.CanFillBottle)
        {
            bottle.SetWaterColor(_waterColor.ResultColor);          
            bottle.SetPotion(_potionInClaudron);
            bottle.SetPosition(_bottleInventory.GetFreeSlot().transform);
            
            ClearMixSystem();
            
        }
        else
        {
            Debug.LogError("зелье не сварено");
        }
    }

    public void FillPotion()
    {
        _labelSetter = new LabelSetter(IngredientsInClaudron);
        _potionInClaudron = new Potion(_labelSetter.GetCurrentLabels());
    }

    private void AddIngredientToClaudron(Ingredient ingredient)
    {
        if(IngredientsInClaudron.Count < 4)
        {
            IngredientsInClaudron.Add(ingredient);
            ingredient.SetInClaudron(true);
            ingredient.DisableIngredient();

            OnIngredientAdded?.Invoke();
            ActiveButtonBrewDelegate.Invoke();
            _waterColor.AddColor(ingredient.IngredienColor);
        }
        else
        {
            Debug.LogError("Котел переполнен, сбрасываю все");
            IngredientsInClaudron.Clear();
            _waterColor.ResetWaterColor(Color.white);
        }
        
    }

    public void ClearMixSystem()
    {
        foreach (Ingredient item in IngredientsInClaudron)
        {        
            ObjectPool.SharedInstance.DestroyObject(item.gameObject);
        }

        _claudronSystem.ClearClaudron();
        IngredientsInClaudron.Clear();
        ActiveButtonBrewDelegate.Invoke();
    }
}

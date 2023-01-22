using System;
using System.Collections.Generic;
using UnityEngine;

public class MixingSystemv3 : MonoBehaviour
{
    public static event Action OnIngredientAdded;

    public delegate void RefreshCountIngredient();
    public RefreshCountIngredient ActiveButtonBrewDelegate;

    public Action OnBottleFilled;

    [SerializeField] private BottleInventory _bottleInventory;
    [SerializeField] private ClaudronSystem _claudronSystem;
    [SerializeField] private ClaudronLabelView _claudronLabelView;
    [SerializeField] private WaterColorv2 _waterColor;
    [SerializeField] private CompletePotionViewer _potionViewer;
    [SerializeField] private Cookv2 _cook;

    [SerializeField] private AudioManager _audioManager;

    private Potion _potionInClaudron;
    private LabelSetter _labelSetter;

    public List<Ingredient> IngredientsInClaudron; //get; private set;

    private void Start()
    {
        IngredientsInClaudron = new List<Ingredient>();
        _labelSetter = new LabelSetter();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Ingredient ingredient))
        {
            if(IngredientsInClaudron.Count<4)
            AddIngredientToClaudron(ingredient);
        }
    }

    public void FillBottle(BottleModel bottle)
    {      
        bool checkFilling = _labelSetter.Labels.Count != 0 && _cook.CanFillBottle;

        if (checkFilling)
        {
            CreatePotionForBottle();
            bottle.FillBottle(_potionInClaudron, _waterColor.ResultColor);

            SetPositionBottle(bottle);
            _potionViewer.AddLastPotion(bottle);

            OnBottleFilled?.Invoke();
            _claudronSystem.ClearClaudron();
            ClearMixSystem();
            _audioManager.PlaySFX(_audioManager.Data.CreatePotionClip);
        }
        else
        {
            Debug.LogError("зелье не сварено");
        }
    }

    private void SetPositionBottle(BottleModel bottle)
    {
        FullBottleSlot fullSlot;

        fullSlot = _bottleInventory.GetSlot(bottle.Data.PotionInBottle);

        if (fullSlot != null)
        {
            bottle.SetPosition(fullSlot.transform);
            _bottleInventory.AddPotionInInventory(_potionInClaudron);
        }
        else
        {
            Debug.Log("Свободных мест нет, бутылка уничтожается");
            bottle.DestroyBottle();
        }
    }

    private void CreatePotionForBottle()
    {
        _labelSetter.SetTypeFromIngredient(IngredientsInClaudron);
        _potionInClaudron = new Potion(_labelSetter.Labels);
    }

    private void AddIngredientToClaudron(Ingredient ingredient)
    {
        if(IngredientsInClaudron.Count < 4)
        {
            IngredientsInClaudron.Add(ingredient);
            ingredient.SetInClaudron(true);

            OnIngredientAdded?.Invoke();
            ActiveButtonBrewDelegate.Invoke();

            _waterColor.AddColor(ingredient.IngredienColor);
            CheckLabelInClaudron();

            _audioManager.PlaySFX(_audioManager.Data.WaterDrop);

        }
        else
        {
            Debug.LogError("Котел переполнен");  //TODO - ингредиенты должны возвращаться в свои ячейки

        }
        
    }

    private void CheckLabelInClaudron()
    {
        if(IngredientsInClaudron.Count > 1)
        {
            _labelSetter.SetTypeFromIngredient(IngredientsInClaudron);
            _claudronLabelView.SetLabel(_labelSetter.GetCurrentLabels());
        }        
    }

    public void ClearMixSystem()
    {
        foreach (Ingredient item in IngredientsInClaudron)
        {        
            ObjectPool.SharedInstance.DestroyObject(item.gameObject);
        }

        _claudronLabelView.Reset();
        _labelSetter.Clear();
        IngredientsInClaudron.Clear();
        ActiveButtonBrewDelegate.Invoke();
    }
}

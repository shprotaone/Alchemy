using System;
using System.Collections.Generic;
using UnityEngine;

public class MixingSystem : MonoBehaviour
{
    public static event Action OnIngredientAdded;

    public delegate void RefreshCountIngredient();
    public RefreshCountIngredient ActiveButtonBrewDelegate;

    public Action OnBottleFilled;

    [SerializeField] private BottleInventory _bottleInventory;
    [SerializeField] private ClaudronSystem _claudronSystem;
    [SerializeField] private ClaudronLabelView _claudronLabelView;
    [SerializeField] private WaterColoring _waterColor;
    [SerializeField] private CompletePotionViewer _potionViewer;
    [SerializeField] private CookHandler _cook;

    [SerializeField] private AudioManager _audioManager;

    private Potion _potionInClaudron;
    private LabelSetter _labelSetter;

    public List<Ingredient> IngredientsInClaudron; //get; private set;
    public LabelSetter LabelSetter => _labelSetter;

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
        bool checkFilling = _labelSetter.Labels.Count != 0 && _cook.CanFillBottle && !bottle.IsFull;

        if (checkFilling)
        {
            CreatePotionForBottle();
            bottle.FillBottle(_potionInClaudron, _waterColor.ResultColor);

            SetSlotInInventory(bottle);
            _potionViewer.AddLastPotion(bottle);

            OnBottleFilled?.Invoke();
            _claudronSystem.ClearClaudron();
            ClearMixSystem();
            _audioManager.PlaySFX(_audioManager.Data.CreatePotionClip);
        }
        else
        {
            Debug.LogError("����� �� �������");
        }
    }

    private void SetSlotInInventory(BottleModel bottle)
    {
        FullBottleSlot fullSlot;

        fullSlot = _bottleInventory.GetSlot(bottle.PotionInBottle);

        if (fullSlot != null)
        {
            //fullSlot.SetSlot(bottle);
            bottle.SetPosition(fullSlot.transform);
            _bottleInventory.AddPotionInInventory(_potionInClaudron);
        }
        else
        {
            Debug.Log("��������� ���� ���, ������� ������������");
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
            Debug.LogError("����� ����������");  //TODO - ����������� ������ ������������ � ���� ������

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

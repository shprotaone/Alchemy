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
    [SerializeField] private Cookv2 _cook;

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
            AddIngredientToClaudron(ingredient);
        }
        //else if(collision.TryGetComponent(out Bottle bottle))
        //{
        //    FillBottle(bottle);
        //}
    }

    public void FillBottle(Bottle bottle)
    {
        FullBottleSlot fullSlot;

        if (!bottle.IsFull && _labelSetter.Labels.Count != 0 && _cook.CanFillBottle)
        {        
            bottle.FillBottle(_potionInClaudron, _waterColor.ResultColor);

            fullSlot = _bottleInventory.GetSlot(bottle.PotionInBottle);

            if(fullSlot!= null)
            {
                bottle.SetPosition(fullSlot.transform);
                _bottleInventory.AddPotionInInventory(_potionInClaudron);               
            }
            else
            {
                Debug.Log("��������� ���� ���, ������� ������������");
                bottle.DestroyBottle();
            }

            OnBottleFilled?.Invoke();

            _claudronSystem.ClearClaudron();
            _labelSetter.Clear();
            ClearMixSystem();            
        }
        else
        {
            Debug.LogError("����� �� �������");
        }
    }

    public void FillPotion()
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

        }
        else
        {
            Debug.LogError("����� ����������, ��������� ���");  //TODO - ����������� ������ ������������ � ���� ������
            _labelSetter.Clear();
            _claudronLabelView.Reset();
            IngredientsInClaudron.Clear();
            _waterColor.ResetWaterColor(Color.white);
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MixingSystemv2 : MonoBehaviour, IDropHandler
{
    private const int maxMixColor = 5;
    private const string bottleTag = "Bottle";
    private const string ingredientTag = "Ingredient";

    public delegate void RefreshCountIngredient();
    public RefreshCountIngredient _refreshDelegate;

    [SerializeField] private PotionDetector _potionDetector;
    [SerializeField] private List<Ingredient> _ingredients;

    private WaterColorv2 _waterColor;    
    private Potion _resultPotion;
    private Cook _cookSystem;

    private bool _bottleFilled;

    public List<Ingredient> Ingredients => _ingredients;
    public Potion ResultPotion => _resultPotion;
    public bool BottleFilled => _bottleFilled;

    private void Start()
    {
        _waterColor = GetComponentInChildren<WaterColorv2>();
        _cookSystem = GetComponent<Cook>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (CheckIngredientIn(eventData.pointerDrag.gameObject))
        {
            
        }
        else if (CheckBottle(eventData.pointerDrag.gameObject))
        {
            
        }
    }

    private List<Color> MixColor()
    {
        List<Color> colors = new List<Color>();

        foreach (var item in _ingredients)
        {
            colors.Add(item.IngredienColor);
        }

        return colors;
    }

    private void CheckFullColorCapacity(Ingredient ingredient)  
    {
        if (_ingredients.Count < maxMixColor)
        {
            _ingredients.Add(ingredient);
            _refreshDelegate.Invoke();
            _waterColor.ColorWater(MixColor());
        }
        else
        {
            ClearMixSystem();            
        }
    }

    public bool CheckIngredientIn(UnityEngine.GameObject curentObject)
    {
        if (curentObject.CompareTag(ingredientTag))
        {
            Ingredient currentIngredient = curentObject.GetComponent<Ingredient>();

            CheckFullColorCapacity(currentIngredient);

            currentIngredient.DisableIngredient();
            
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckBottle(UnityEngine.GameObject currentObject)
    {
        if (currentObject.CompareTag(bottleTag))
        {
            if (_cookSystem.CanFillBottle)
            {
                Bottle bottle = currentObject.GetComponent<Bottle>();

                bottle.FillBottle(_waterColor.ResultColor);

                _resultPotion = _potionDetector.FillCurrentPotion(_ingredients);

                bottle.FillPotionInBottle(_resultPotion);
                _bottleFilled = true;
            }
            else
            {
                print("NotComplete");
                _bottleFilled = false;
            }

            return true;
        }
        else
        {
            _bottleFilled = false;
            return false;
        }
    }

    public void ClearMixSystem()
    {
        _ingredients.Clear();
        _refreshDelegate.Invoke();
        _waterColor.SetColor(Color.white);
    }
}

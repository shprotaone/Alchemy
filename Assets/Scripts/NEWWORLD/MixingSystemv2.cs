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

    [SerializeField] private List<Ingredient> _ingredients;

    private WaterColorv2 _waterColor;
    private Cook _cookSystem;

    public List<Ingredient> Ingredients => _ingredients;

    private void Start()
    {
        _waterColor = GetComponentInChildren<WaterColorv2>();
        _cookSystem = GetComponent<Cook>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (CheckIngredientIn(eventData.pointerDrag.gameObject))
        {
            print("its a ingredient");
        }
        else if (CheckBottle(eventData.pointerDrag.gameObject))
        {
            print("its a bottle");
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

    private void CheckFullColorCapacity(Ingredient ingredient)  //возможно разбить? 
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


    public bool CheckIngredientIn(GameObject curentObject)
    {
        if (curentObject.CompareTag(ingredientTag))
        {
            Ingredient currentIngredient = curentObject.GetComponent<Ingredient>();

            CheckFullColorCapacity(currentIngredient);

            currentIngredient.DestroyIngredient();
            
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckBottle(GameObject currentObject)
    {
        if (currentObject.CompareTag(bottleTag))
        {
            if(_cookSystem.CanFillBottle)
            currentObject.GetComponent<Bottle>().FillBottle(_waterColor.ResultColor);
            else { print("NotComplete"); }

            return true;
        }
        else
        {
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

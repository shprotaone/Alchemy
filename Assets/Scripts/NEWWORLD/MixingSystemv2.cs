using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MixingSystemv2 : MonoBehaviour, IDropHandler
{
    private const int maxMixColor = 5;
    private const string bottleTag = "Bottle";
    private const string ingredientTag = "Ingredient";

    [SerializeField] private WaterColorv2 _waterColor;
    [SerializeField] private List<Ingredient> _ingredients;

    private void Start()
    {
        _waterColor = GetComponentInChildren<WaterColorv2>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (CheckIngredient(eventData.pointerDrag.gameObject))
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

    private void CheckFullColorCapacity(Ingredient ingredient)
    {
        if (_ingredients.Count < maxMixColor)
        {
            _ingredients.Add(ingredient);
            _waterColor.ColorWater(MixColor());
        }
        else
        {
            _ingredients.Clear();
            _waterColor.SetColor(Color.white);
        }
    }
    private bool CheckIngredient(GameObject curentObject)
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
            currentObject.GetComponent<Bottle>().FillBottle(_waterColor.ResultColor);
            return true;
        }
        else
        {
            return false;
        }
    }
}

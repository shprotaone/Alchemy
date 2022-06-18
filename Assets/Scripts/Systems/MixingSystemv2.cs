using System.Collections.Generic;
using UnityEngine;

public class MixingSystemv2 : MonoBehaviour
{
    private const int maxMixColor = 5;
    private const string bottleTag = "Bottle";
    private const string ingredientTag = "Ingredient";

    public delegate void RefreshCountIngredient();
    public RefreshCountIngredient _refreshDelegate;

    [SerializeField] private PotionDetector _potionDetector;
    [SerializeField] private List<Ingredient> _ingredients;
    [SerializeField] private TableManager _tableManager;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ingredientTag))
        {
            CheckIngredientIn(collision.gameObject);
        }
        else if (collision.CompareTag(bottleTag))
        {
            CheckBottle(collision.gameObject);
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

    public bool CheckIngredientIn(GameObject curentObject)
    {
        Ingredient currentIngredient = curentObject.GetComponent<Ingredient>(); ;

        if (curentObject.CompareTag(ingredientTag))
        {
            CheckFullColorCapacity(currentIngredient);
            currentIngredient.SetInClaudron(true);
            currentIngredient.DisableIngredient();
            
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
            if (_cookSystem.CanFillBottle)
            {
                Bottle bottle = currentObject.GetComponent<Bottle>();

                bottle.FillWaterInBottle(_waterColor.ResultColor);

                _potionDetector.FillCurrentPotion(_ingredients);
                bottle.FillPotionInBottle(_potionDetector.CurrentPotion);

                bottle.transform.SetParent(_tableManager.FullPotionTable.transform);               
                bottle.Movement();

                _bottleFilled = true;
            }
            else
            {                
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
        foreach (Ingredient item in _ingredients)
        {
            Destroy(item.gameObject);
        }

        _ingredients.Clear();
        _refreshDelegate.Invoke();
        _waterColor.SetColor(Color.white);
    }
}

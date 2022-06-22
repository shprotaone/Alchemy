using System.Collections.Generic;
using UnityEngine;

public class MixingSystemv2 : MonoBehaviour
{
    private const int maxMixColor = 5;
    public const string bottleTag = "Bottle";
    private const string ingredientTag = "Ingredient";

    public delegate void RefreshCountIngredient();
    public RefreshCountIngredient _refreshDelegate;

    [SerializeField] private PotionDetector _potionDetector;
    [SerializeField] private List<Ingredient> _ingredients;
    [SerializeField] private TableManager _tableManager;

    private AudioSource _audioSource;
    private WaterColorv2 _waterColor;    
    private Cook _cookSystem;

    private bool _bottleFilled;

    public List<Ingredient> Ingredients => _ingredients;
    public bool BottleFilled => _bottleFilled;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
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
            MixingIngredient(collision.gameObject);
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
            print(_audioSource.gameObject.activeInHierarchy);
            print(_audioSource.enabled);

            _ingredients.Add(ingredient);
            _audioSource.Play();
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

    private void MixingIngredient(GameObject currentObject)
    {        
        if (_cookSystem.CanFillBottle)
        {
            Bottle bottle = currentObject.GetComponent<Bottle>();

            SetPotionInBottle(bottle);                                          
        }
    }

    public void CheckPotion()
    {
        _potionDetector.FillCurrentPotion(_ingredients);

        if (_potionDetector.CurrentPotion.PotionName == "")
        {
            _bottleFilled = false;
            print("BadPotion");
        }
        else
        {
            _bottleFilled = true;
            print("GoodPotion");
        }
    }

    private void SetPotionInBottle(Bottle bottle)
    {
        bottle.FillWaterInBottle(_waterColor.ResultColor);        
        bottle.FillPotionInBottle(_potionDetector.CurrentPotion);
        bottle.transform.SetParent(_tableManager.FullPotionTable.transform);
        bottle.Movement();
    }

    public void ClearMixSystem()
    {
        foreach (Ingredient item in _ingredients)
        {
            Destroy(item.gameObject);
        }

        _cookSystem.CleanClaudron();
        _ingredients.Clear();
        _refreshDelegate.Invoke();
        _waterColor.SetColor(Color.white);
    }
}

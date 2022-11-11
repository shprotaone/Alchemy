using System;
using System.Collections.Generic;
using UnityEngine;

public class MixingSystemv2 : MonoBehaviour
{
    private const int maxMixColor = 5;
    public const string bottleTag = "Bottle";
    private const string ingredientTag = "Ingredient";

    public delegate void RefreshCountIngredient();
    public RefreshCountIngredient ActiveButtonBrewDelegate;

    public delegate void FilledBottle();
    public FilledBottle FilledBottleDelegateForTutorial;

    public Action OnIngredientAdded;

    [SerializeField] private PotionDetector _potionDetector;
    [SerializeField] private ContrabandPotionSystem _contrabandPotionSystem;
    [SerializeField] private List<Ingredient> _ingredients;
    [SerializeField] private TableManager _tableManager;
    [SerializeField] private Claudron _claudron;
    [SerializeField] private ClaudronSystem _claudronSystem;

    private AudioSource _audioSource;
    private WaterColorv2 _waterColor;
    private Potion _potionOnClaudron;
    private Cookv2 _cookSystem;
    //private Cook _cookSystem;

    private bool _isPotionApproved;

    public List<Ingredient> Ingredients => _ingredients;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _waterColor = GetComponentInChildren<WaterColorv2>();
        _cookSystem = GetComponent<Cookv2>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ingredientTag))
        {
            CheckIngredientIn(collision.gameObject);
        }

        if (_isPotionApproved) 
        {
            if (collision.CompareTag(bottleTag) && _potionOnClaudron != null)
            {
                Bottle bottle = collision.GetComponent<Bottle>();
                _potionOnClaudron.SetColor(_waterColor.ResultColor);

                CheckOnContraband(_potionOnClaudron);
                bottle.SetPotion(_potionOnClaudron);
                
                FilledBottleDelegateForTutorial?.Invoke();

                _claudronSystem.ClearClaudron();
            }
            else
            {
                Debug.LogWarning("Котел пустой");
            }
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

            _audioSource.Play();
            ActiveButtonBrewDelegate.Invoke();
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
            OnIngredientAdded?.Invoke();
            
            return true;
        }
        else
        {           
            return false;
        }
    }

    public void CheckPotion()
    {
        _potionDetector.FillCurrentPotion(_ingredients);

        if (_potionDetector.CurrentPotion.PotionName == "")
        {
            _isPotionApproved = false;
            _claudronSystem.CrunchClaudron(true);
            _claudronSystem.ClearClaudron();
            Debug.LogWarning("Такого зелья не существует");
        }
        else
        {
            _potionOnClaudron = _potionDetector.FillCurrentPotion(_ingredients);
            _isPotionApproved = true;
            Debug.Log("Вы сварили " + _potionDetector.CurrentPotion.PotionName);
        }
    } 

    private void CheckOnContraband(Potion potion)
    {
        if(_contrabandPotionSystem.ContrabandPotion != null)
        {
            if (_contrabandPotionSystem.ContrabandPotion.PotionName == potion.PotionName)
            {
                potion.SetContraband(true);
            }

            else potion.SetContraband(false);
        }
    }

    public void ClearMixSystem()
    {
        foreach (Ingredient item in _ingredients)
        {
            item.Collider.enabled = false;
            ObjectPool.SharedInstance.DestroyObject(item.gameObject);
        }

        _potionOnClaudron = null;
        _ingredients.Clear();
        ActiveButtonBrewDelegate.Invoke();
    }
}
 
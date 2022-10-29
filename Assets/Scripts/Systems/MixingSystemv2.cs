using System.Collections.Generic;
using UnityEngine;

public class MixingSystemv2 : MonoBehaviour
{
    private const int maxMixColor = 5;
    public const string bottleTag = "Bottle";
    private const string ingredientTag = "Ingredient";

    public delegate void RefreshCountIngredient();
    public RefreshCountIngredient RefreshDelegate;

    public delegate void FilledBottle();
    public FilledBottle FilledBottleDelegete;

    [SerializeField] private PotionDetector _potionDetector;
    [SerializeField] private ContrabandPotionSystem _contrabandPotionSystem;
    [SerializeField] private List<Ingredient> _ingredients;
    [SerializeField] private TableManager _tableManager;
    [SerializeField] private EffectHandler _effectHandler;

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
            _ingredients.Add(ingredient);           

            _audioSource.Play();
            RefreshDelegate.Invoke();
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
            _bottleFilled = false;  //ответсвенность заполнения перенести в бутылку
        }
        else
        {
            _bottleFilled = true;
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

    private void SetPotionInBottle(Bottle bottle)
    {
        if (!bottle.IsFull)
        {
            Potion potion = _potionDetector.CurrentPotion;

            CheckOnContraband(potion);        

            bottle.FillPotionInBottle(potion, _waterColor.ResultColor, _effectHandler.GetEffect(potion.ResourceType));       
            bottle.transform.SetParent(_tableManager.FullPotionTable.transform);

            bottle.Movement();

            FilledBottleDelegete?.Invoke();

            ClearMixSystem();
        }
    }

    public void ClearMixSystem()
    {
        foreach (Ingredient item in _ingredients)
        {
            Destroy(item.gameObject);
        }

        _cookSystem.CleanClaudron();
        _ingredients.Clear();
        RefreshDelegate.Invoke();
        _waterColor.SetColor(Color.white);
    }
}

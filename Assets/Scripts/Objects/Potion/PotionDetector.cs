using System.Collections.Generic;
using UnityEngine;

public class PotionDetector : MonoBehaviour
{
    private const int maxIngredients = 5;

    [SerializeField] private MixingSystemv2 _mixingSystem;
    [SerializeField] private JSONReader _jsonReader;

    private Potion _currentPotion;
    private PotionSizer _potionSizer;    
    private Ingredient[] _currentIngredients;

    public Potion CurrentPotion => _currentPotion;

    private void Start()
    {
        _potionSizer = _jsonReader.PotionSizer;
        _currentPotion = new Potion();
    }

    public void FillCurrentPotion(List<Ingredient> ingredients)
    {
        _currentIngredients = new Ingredient[maxIngredients];

        for (int i = 0; i < ingredients.Count; i++)
        {
            _currentIngredients[i] = ingredients[i];
        }
        
        _currentPotion.FillPotion(_currentIngredients);
        FindPotion();
        _currentPotion.SetEffect(_currentIngredients);  //перенести в Potion

    }

    private void FindPotion()
    {
        for (int i = 0; i < _potionSizer.Potions.Length; i++)
        {
            if (_currentPotion.PotionEquals(_potionSizer.Potions[i]))
            {
                _currentPotion.SetNamePotion(_potionSizer.Potions[i].name);
                _currentPotion.SetRarity(_potionSizer.Potions[i].rarity);
                _currentPotion.SetGuild(_potionSizer.Potions[i].guild);

                break;
            }
            else
            {
                _currentPotion.SetNamePotion("");
            }            
        }
    }           
}

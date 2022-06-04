using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionDetector : MonoBehaviour
{
    private const int maxIngredients = 5;

    [SerializeField] private MixingSystemv2 _mixingSystem;
    [SerializeField] private JSONReader _jsonReader;
    private Potion _currentPotion;

    private string _potionName;
    private PotionSizer _potionSizer;    
    private Ingredient[] _currentIngredients;

    private void Start()
    {
        _potionSizer = _jsonReader.PotionSizer;
        _currentPotion = GetComponent<Potion>();
    }

    public Potion FillCurrentPotion(List<Ingredient> ingredients)
    {
        _currentIngredients = new Ingredient[maxIngredients];

        for (int i = 0; i < ingredients.Count; i++)
        {
            _currentIngredients[i] = ingredients[i];
        }

        
        _currentPotion.FillPotion(_currentIngredients);
        FindPotion();       

        return _currentPotion;
    }



    private void FindPotion() //открыт вопрос с универсальными цветами
    {
        for (int i = 0; i < _potionSizer.Potions.Length; i++)
        {
            if(_currentPotion.Ingredients[0] == _potionSizer.Potions[i].firstIngredient)
            {
                if(_currentPotion.Ingredients[1] == _potionSizer.Potions[i].secondIngredient)
                {
                    if(_currentPotion.Ingredients[2] == _potionSizer.Potions[i].threeIngredient)
                    {
                        if(_currentPotion.Ingredients[3] == _potionSizer.Potions[i].fourIngredient)
                        {
                            if(_currentPotion.Ingredients[4] == _potionSizer.Potions[i].fiveIngredient)
                            {
                                _currentPotion.SetNamePotion(_potionSizer.Potions[i].name);
                                _currentPotion.SetRarity(_potionSizer.Potions[i].rarity);
                                _currentPotion.SetGuild(_potionSizer.Potions[i].guild);

                                print("FromFind" + _currentPotion.name);
                                break;
                            }
                            else
                            {
                                print("Такого зелья не существует");
                            }
                        }
                    }
                }
            }

        }
    }           
}

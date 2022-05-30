using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionDetector : MonoBehaviour
{
    private const int maxIngredients = 5;

    [SerializeField] private MixingSystemv2 _mixingSystem;
    [SerializeField] private JSONReader _jsonReader;
    [SerializeField] private PotionData _currentPotion;

    private PotionSizer _potionSizer;    
    private Ingredient[] _currentIngredients;

    private void Start()
    {
        _potionSizer = _jsonReader._potionSizer;       
    }

    public PotionData FillCurrentPotion(List<Ingredient> ingredients)
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
            if(_currentPotion.firstIngredient == _potionSizer.Potions[i].firstIngredient)
            {
                if(_currentPotion.secondIngredient == _potionSizer.Potions[i].secondIngredient)
                {
                    if(_currentPotion.threeIngredient == _potionSizer.Potions[i].threeIngredient)
                    {
                        if(_currentPotion.fourIngredient == _potionSizer.Potions[i].fourIngredient)
                        {
                            if(_currentPotion.fiveIngredient == _potionSizer.Potions[i].fiveIngredient)
                            {
                                _currentPotion = _potionSizer.Potions[i];
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionDetector : MonoBehaviour
{
    private const int maxIngredients = 5;

    [SerializeField] private MixingSystemv2 _mixingSystem;
    [SerializeField] private JSONReader _jsonReader;
    [SerializeField] private Potion _currentPotion;

    private PotionSizer _potionSizer;    
    private Ingredient[] _currentIngredients;

    private void Start()
    {
        _potionSizer = _jsonReader._potionSizer;       
    }

    public void FindActivator()
    {
        FillCurrentPotion();
        FindPotion();
    }

    private void FillCurrentPotion()
    {
        _currentIngredients = new Ingredient[maxIngredients];

        for (int i = 0; i < _mixingSystem.Ingredients.Count; i++)
        {
            _currentIngredients[i] = _mixingSystem.Ingredients[i];
        }

        _currentPotion.FillPotion(_currentIngredients);
    }

    private void FindPotion() //открыт вопрос с универсальными цветами
    {
        for (int i = 0; i < _potionSizer.Potion.Length; i++)
        {
            if(_currentPotion.firstIngredient == _potionSizer.Potion[i].firstIngredient)
            {
                if(_currentPotion.secondIngredient == _potionSizer.Potion[i].secondIngredient)
                {
                    if(_currentPotion.threeIngredient == _potionSizer.Potion[i].threeIngredient)
                    {
                        if(_currentPotion.fourIngredient == _potionSizer.Potion[i].fourIngredient)
                        {
                            if(_currentPotion.fiveIngredient == _potionSizer.Potion[i].fiveIngredient)
                            {
                                print(_potionSizer.Potion[i].name);
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

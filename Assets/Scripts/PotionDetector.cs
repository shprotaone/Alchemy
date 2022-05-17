using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionDetector : MonoBehaviour
{
    [SerializeField] private MixingSystemv2 _mixingSystem;

    private List<Ingredient> _currentIngredient;
    public void GetIngredientList(List<Ingredient> currentIngredients)
    {
        _currentIngredient = currentIngredients;
        for (int i = 0; i < currentIngredients.Count; i++)
        {
            
        }
    }


}

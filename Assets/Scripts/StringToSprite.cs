using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringToSprite : MonoBehaviour
{
    [SerializeField] private IngredientData[] _ingredients;
    public Sprite ParseStringToSprite(string name)
    {
        switch (name)
        {
            case "Red":
                return _ingredients[0].mainSprite;
            case "Blue":
                return _ingredients[1].mainSprite;
            case "Yellow":
                return _ingredients[2].mainSprite;
            case "White":
                return _ingredients[3].mainSprite;
            case "Eye":
                return _ingredients[4].mainSprite;
            case "Ladan":
                return _ingredients[5].mainSprite;
            case "Sand":
                return _ingredients[6].mainSprite;
            case "Stone":
                return _ingredients[7].mainSprite;
        }
        return null;
    }
}

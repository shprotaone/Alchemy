using System.Collections.Generic;
using UnityEngine;

public class PotionDetector : MonoBehaviour
{
    [SerializeField] private MixingSystemv2 _mixingSystem;
    [SerializeField] private JSONReader _jsonReader;

    private Potion _currentPotion;
    private PotionSizer _potionSizer;    

    public Potion CurrentPotion => _currentPotion;

    private void Start()
    {
        _potionSizer = _jsonReader.PotionSizer;
        _currentPotion = new Potion();
    }

    public Potion FillCurrentPotion(List<Ingredient> ingredients)
    {        
        _currentPotion.FillPotion(ingredients);
        FindPotion();
        _currentPotion.SetEffect(ingredients);
        return _currentPotion;
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

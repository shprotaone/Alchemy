using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskSystem : MonoBehaviour
{
    [SerializeField] private JSONReader _jsonReader;
    [SerializeField] private StringToSprite _stringToSprite;
    [SerializeField] private bool _trainingTask;

    private PotionSizer _potionSizer;

    private int _numberTask;
    
    private void Start()
    {
        _potionSizer = _jsonReader._potionSizer;
    }

    public void TakeTask(Task task)
    {
        Potion currentPotion = SetTaskPotion();
        if (_trainingTask)
        {
            Sprite firstIngredient = _stringToSprite.ParseStringToSprite(currentPotion.firstIngredient);
            Sprite secondIngredient = _stringToSprite.ParseStringToSprite(currentPotion.secondIngredient);

            task.FillTask(firstIngredient, secondIngredient, 500);
        }
        else
        {
            task.FillTask(currentPotion.name, 500);
        }
    }

    private Potion SetTaskPotion()
    {
        _numberTask = Random.Range(0, _potionSizer.Potion.Length);
        return _potionSizer.Potion[_numberTask];
    }
}

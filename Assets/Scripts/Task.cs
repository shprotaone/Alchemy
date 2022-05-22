using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Task : MonoBehaviour
{
    [SerializeField] private TaskSystem _taskSystem;
    [SerializeField] private TMP_Text _potionNameText;
    [SerializeField] private TMP_Text _rewardText;
    [SerializeField] private Image _firstIngredient;
    [SerializeField] private Image _secondIngredient;

    private int _reward;

    public void InitTask()
    {
        _taskSystem.TakeTask(this);
    }

    public void FillTask(string potionName, int reward)
    {
        _potionNameText.gameObject.SetActive(true);
        _potionNameText.text = potionName;
        _reward = reward;
        _rewardText.text = reward.ToString();
    }

    public void FillTask(Sprite firstIngredient,Sprite secondIngredient, int reward)
    {
        _potionNameText.gameObject.SetActive(false);
        _firstIngredient.sprite = firstIngredient;
        _secondIngredient.sprite = secondIngredient;
        _reward = reward;
        _rewardText.text = reward.ToString();
    }

    public void ChekResult()
    {

    }
    
}

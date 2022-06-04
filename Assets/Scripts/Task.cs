using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Task : MonoBehaviour
{
    [SerializeField] private TaskSystem _taskSystem;
    [SerializeField] private TMP_Text _potionNameText;
    [SerializeField] private TMP_Text _rewardText;
    [SerializeField] private Image _firstIngredient;
    [SerializeField] private Image _secondIngredient;
    [SerializeField] private Image _thirdIngredient;

    private Potion _currentPotion;
    private Visitor _visitor;
    private int _rewardCoin;
    private float _reward;
    private float _penalty;

    public Visitor Visitor => _visitor;
    public void InitTask()
    {
        _taskSystem.TakeTask(this);
        _visitor = GetComponentInParent<Visitor>();
        _currentPotion = _taskSystem.CurrentPotion;

        SetGuild();
        SetRewardAndPenalty();

        print("Need " + _currentPotion.PotionName);
    }

    public void FillTask(string potionName, int reward)
    {
        _potionNameText.gameObject.SetActive(true);
        _potionNameText.text = potionName;
        _rewardCoin = reward;
        _rewardText.text = reward.ToString();
    }

    public void FillTask(Sprite firstIngredient,Sprite secondIngredient,Sprite thirdIngredient, int reward)
    {        
        _potionNameText.gameObject.SetActive(false);        //лучше переделать
        _thirdIngredient.enabled = false;

        _firstIngredient.sprite = firstIngredient;
        _secondIngredient.sprite = secondIngredient;

        if(thirdIngredient != null)
        {
            _thirdIngredient.enabled = true;
            _thirdIngredient.sprite = thirdIngredient;
        }
        
        _rewardCoin = reward;
        _rewardText.text = reward.ToString();
    }

    public void SetGuild()
    {
        _currentPotion.SetGuild(_visitor.Guild);
    }

    public void ChekResult(Potion potion)
    {
        if (_currentPotion.PotionName == potion.PotionName)
        {            
            print("In Bottle " + potion.PotionName);
            _taskSystem.TaskComplete(_rewardCoin,_reward);
            _visitor.Fading();

            GameObject curCoins = Instantiate(_taskSystem.CoinPrefab, transform.position, Quaternion.identity);
            
            Coin coin = curCoins.GetComponent<Coin>();
            coin.Movement(_taskSystem.JarTransform.position);
        }
        else
        {
            print("Wrong");
            print("Need " + _currentPotion.PotionName);
            print("In Bottle " + potion.PotionName);
        }
    }

    public void TaskCanceled()
    {
        _taskSystem.TaskCanceled(_penalty);
    }

    private void SetRewardAndPenalty()
    {
        GuildRepCalculator calculator = new GuildRepCalculator();
        _reward = calculator.CalculateReward(_currentPotion.Rarity);
        _penalty = calculator.CalculatePenalty(_currentPotion.Rarity);
    }
    
}

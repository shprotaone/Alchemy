using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PotionTask : MonoBehaviour
{
    private const float timeAlpha = 1;

    [SerializeField] private PotionTaskSystem _taskSystem;
    [SerializeField] private TMP_Text _potionNameText;
    [SerializeField] private TMP_Text _rewardText;
    [SerializeField] private Transform _imagesObj;    
    [SerializeField] private List<Image> _ingredientImages;
    [SerializeField] private Image[] _taskImages;

    private Potion _currentPotion;
    private Visitor _visitor;
    private int _rewardCoin;
    private float _reward;
    private float _penalty;

    public Visitor Visitor => _visitor;
    public void InitTask()
    {
        ResetIngredientImages();

        _taskSystem.TakeTask(this);
        _currentPotion = _taskSystem.CurrentPotion;

        _visitor = GetComponentInParent<Visitor>();
        
        RisingTask();

        SetGuild();
        SetRewardAndPenalty();

        print("Need " + _currentPotion.PotionName);
    }

    public void FillTask(string potionName, int reward)
    {
        _potionNameText.gameObject.SetActive(true);
        _imagesObj.gameObject.SetActive(false);

        _potionNameText.text = potionName;
        _rewardCoin = reward;
        _rewardText.text = reward.ToString();
    }

    public void FillTask(Sprite[] ingredientSprites, int reward)
    {        
        _potionNameText.gameObject.SetActive(false);        //лучше переделать

        for (int i = 0; i < ingredientSprites.Length; i++)
        {
            _ingredientImages[i].sprite = ingredientSprites[i];

            if (ingredientSprites[i] == null)
            {
                _ingredientImages[i].enabled = false;
            }
        }

        _rewardCoin = reward;
        _rewardText.text = reward.ToString();
    }

    private void ResetIngredientImages()
    {
        foreach (var item in _ingredientImages)
        {
            item.enabled = true;
            item.sprite = null;
        }
    }

    public void SetCustomTask()
    {

    }

    public void SetGuild()
    {
        _currentPotion.SetGuild(_visitor.Guild);
    }

    public bool ChekResult(Potion potion)
    {
        if (_currentPotion.PotionName == potion.PotionName)
        {            
           _taskSystem.TaskComplete(_rewardCoin,_reward);

            GameObject curCoins = Instantiate(_taskSystem.CoinPrefab, transform.position, Quaternion.identity);
            
            Coin coin = curCoins.GetComponent<Coin>();
            coin.Movement(_taskSystem.JarTransform.position);

            FadingTask();
            return true;
        }
        else
        {
            print("Need " + _currentPotion.PotionName);
            print("In Bottle " + potion.PotionName);
            return false;
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

    private void RisingTask()
    {
        foreach (var item in _taskImages)
        {
            DOTween.ToAlpha(() => item.color, x => item.color = x, 1, timeAlpha);   
        }

        foreach (var item in _ingredientImages)
        {
            DOTween.ToAlpha(() => item.color, x => item.color = x, 1, timeAlpha);
        }

        DOTween.ToAlpha(() => _potionNameText.color, x => _potionNameText.color = x, 1, timeAlpha);
        DOTween.ToAlpha(() => _rewardText.color, x => _rewardText.color = x, 1, timeAlpha);
    }

    public void FadingTask()
    {
        foreach (var item in _ingredientImages)
        {
            DOTween.ToAlpha(() => item.color, x => item.color = x, 0, timeAlpha);
        }

        foreach (var item in _taskImages)
        {
            DOTween.ToAlpha(() => item.color, x => item.color = x, 0, timeAlpha);
        }

        DOTween.ToAlpha(() => _potionNameText.color, x => _potionNameText.color = x, 0, timeAlpha);
        DOTween.ToAlpha(() => _rewardText.color, x => _rewardText.color = x, 0, timeAlpha);
    }
}

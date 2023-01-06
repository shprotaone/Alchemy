using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotionTaskView : MonoBehaviour
{
    private const float timeAlpha = 0.5f;
    
    [SerializeField] private TMP_Text _potionNameText;
    [SerializeField] private TMP_Text _rewardText;
    [SerializeField] private Transform _imagesObj;
    [SerializeField] private List<Image> _ingredientImages;
    [SerializeField] private List<Image> _UIImages;
    [SerializeField] private string _potionInTask;

    private PotionTask _task;

    public void InitTask(PotionTask task)
    {
        ResetIngredientImages();
        _task = task;
        FillTaskView(task);
 

         RisingTask();
    }

    /// <summary>
    /// Текстовая версия задания
    /// </summary>
    /// <param name="potionName"></param>
    /// <param name="reward"></param>
    public void FillTaskView(PotionTask task)
    {     
        _potionNameText.gameObject.SetActive(false);
        _imagesObj.gameObject.SetActive(true);

        FillImageTask(task.Images);      
    }

    public void SetRewardText(PotionTask task)
    {
        _rewardText.text = task.RewardCoin.ToString();
    }

    /// <summary>
    /// Картиночная версия задания
    /// </summary>
    /// <param name="ingredientSprites"></param>
    /// <param name="reward"></param>
    private void FillImageTask(Sprite[] ingredientSprites)
    {
        for (int i = 0; i < ingredientSprites.Length; i++)
        {
            _ingredientImages[i].enabled = true;
            _ingredientImages[i].sprite = ingredientSprites[i];            
        }
    }

    private void ResetIngredientImages()
    {
        foreach (var item in _ingredientImages)
        {
            item.enabled = false;
            item.sprite = null;
        }
    }

    private void RisingTask()
    {
        foreach (var item in _UIImages)
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
        foreach (var item in _UIImages)
        {
            DOTween.ToAlpha(() => item.color, x => item.color = x, 0, timeAlpha);
        }

        foreach (var item in _ingredientImages)
        {
            DOTween.ToAlpha(() => item.color, x => item.color = x, 0, timeAlpha);
        }

        DOTween.ToAlpha(() => _potionNameText.color, x => _potionNameText.color = x, 0, timeAlpha);
        DOTween.ToAlpha(() => _rewardText.color, x => _rewardText.color = x, 0, timeAlpha);
    }

    private void OnDisable()
    {
        ResetIngredientImages();
    }
}

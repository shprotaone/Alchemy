using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotionTaskView : MonoBehaviour
{
    private const float timeAlpha = 1;
    
    [SerializeField] private TMP_Text _potionNameText;
    [SerializeField] private TMP_Text _rewardText;
    [SerializeField] private Transform _imagesObj;
    [SerializeField] private List<Image> _ingredientImages;
    [SerializeField] private List<Image> _UIImages;
    [SerializeField] private string _potionInTask;

    private PotionTask _task;

    public void InitTask(PotionTask task, bool isImageType)
    {
        _task = task;
        if (isImageType)
        {
            FillTaskView(task, isImageType);
        }
        else
        {
            _potionInTask = _task.CurrentPotion.PotionName;
        }       

        RisingTask();
    }

    /// <summary>
    /// Текстовая версия задания
    /// </summary>
    /// <param name="potionName"></param>
    /// <param name="reward"></param>
    public void FillTaskView(PotionTask task,bool isImageType)
    {
        _potionNameText.gameObject.SetActive(true);
        _imagesObj.gameObject.SetActive(false);

        _potionNameText.text = task.CurrentPotion.PotionName;
        _rewardText.text = task.RewardCoin.ToString();

        if (isImageType)
        {
            _potionNameText.gameObject.SetActive(false);
            _imagesObj.gameObject.SetActive(true);

            FillImageTask(task.Images);
        }
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
            _ingredientImages[i].sprite = ingredientSprites[i];

            if (ingredientSprites[i] == null)
            {
                _ingredientImages[i].enabled = false;
            }
        }
    }

    private void ResetIngredientImages()
    {
        foreach (var item in _ingredientImages)
        {
            item.enabled = true;
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

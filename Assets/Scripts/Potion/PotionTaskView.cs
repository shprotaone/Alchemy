using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotionTaskView : MonoBehaviour
{
    private const float timeAlpha = 1;

    [SerializeField] private PotionTaskSystem _taskSystem;
    
    [SerializeField] private TMP_Text _potionNameText;
    [SerializeField] private TMP_Text _rewardText;
    [SerializeField] private Transform _imagesObj;
    [SerializeField] private List<Image> _ingredientImages;
    [SerializeField] private List<Image> _UIImages;
    [SerializeField] private string _potionInTask;

    private PotionTask _task;    
    private bool isImage;

    public void InitTask(PotionTask task)
    {
        _task = new PotionTask(task);
        FillTaskView(task);
        _potionInTask = _task.CurrentPotion.PotionName;

        RisingTask();
    }

    /// <summary>
    /// Текстовая версия задания
    /// </summary>
    /// <param name="potionName"></param>
    /// <param name="reward"></param>
    public void FillTaskView(PotionTask task)
    {
        _potionNameText.gameObject.SetActive(true);
        _imagesObj.gameObject.SetActive(false);

        _potionNameText.text = task.CurrentPotion.PotionName;
        _rewardText.text = task.RewardCoin.ToString();

        if (_taskSystem.ImageTask)
        {
            _potionNameText.gameObject.SetActive(false);
            _imagesObj.gameObject.SetActive(true);

            FillImageTask(_taskSystem.GetIngredientSprites(_task));
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

    public void TaskComplete(bool complete)
    {
        if (complete)
        {
            _taskSystem.TaskComplete(_task.RewardRep);

            GameObject curCoins = Instantiate(_taskSystem.CoinPrefab, transform.position, Quaternion.identity);

            Coin coin = curCoins.GetComponent<Coin>();
            coin.Movement(_taskSystem.JarTransform.position);

            FadingTask();
        }
        else
        {
            print("Need " + _task.CurrentPotion.PotionName);
        }
    }

    public void TaskCanceled()
    {
        _taskSystem.TaskCanceled(_task.PenaltyRep);
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

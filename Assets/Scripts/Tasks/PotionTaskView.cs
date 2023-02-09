using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotionTaskView : MonoBehaviour
{
    private const float timeAlpha = 1f;

    [SerializeField] private EmojiController _emojiController;
    [SerializeField] private Transform _imagesObj;
    [SerializeField] private List<Image> _ingredientImages;
    [SerializeField] private List<Image> _UIImages;

    private PotionTask _task;
    public EmojiController EmojiController => _emojiController;

    private void OnEnable()
    {
        StartCoroutine(SleepRoutine());
    }
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
        _imagesObj.gameObject.SetActive(true);

        FillImageTask(task.Images);      
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

    public void RisingTask()
    {       
        foreach (var item in _UIImages)
        {
            DOTween.ToAlpha(() => item.color, x => item.color = x, 1, timeAlpha).SetEase(Ease.InExpo);
        }

        foreach (var item in _ingredientImages)
        {
            DOTween.ToAlpha(() => item.color, x => item.color = x, 1, timeAlpha).SetEase(Ease.InExpo);
        }
    }

    public void FadingTask()
    {
        //выключаем лейблы задания
        DisableLables(true);
        _emojiController.ShowEmoji(); // показываем Emoji
        _emojiController.FadeEmoji(timeAlpha);

        foreach (var item in _UIImages)
        {
            DOTween.ToAlpha(() => item.color, x => item.color = x, 0, timeAlpha).OnComplete(FadingShowBox);
        }


        StopCoroutine(SleepRoutine());
    }

    public IEnumerator SleepRoutine()
    {
        int timeToSleep = 15;

        while (timeToSleep > 0)
        {
            timeToSleep--;
            yield return new WaitForSeconds(1);
        }

        DisableLables(true);
        _emojiController.SetSleepEmoji();

        yield return new WaitForSeconds(3);

        _emojiController.FadeEmoji(timeAlpha);
        DisableLables(false);
    }

    public void DisableLables(bool flag)
    {
        if (flag)
        {
            _imagesObj.gameObject.SetActive(false);

            foreach (var item in _ingredientImages)
            {
                item.color = new Color(1, 1, 1, 0);
            }
        } 
        else
        {
            _imagesObj.gameObject.SetActive(true);

            foreach (var item in _ingredientImages)
            {
                item.color = new Color(1, 1, 1, 1);     //после сна
            }
        }
    }
    private void FadingShowBox()
    {
        foreach (var item in _UIImages)
        {
            item.color = new Color(1, 1, 1, 0);
        }
    }
}

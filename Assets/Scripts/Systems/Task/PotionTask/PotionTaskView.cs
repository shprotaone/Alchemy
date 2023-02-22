using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotionTaskView : MonoBehaviour
{
    private const float risingTime = 0.2f;
    private const float fadingTime = 0.5f;

    [SerializeField] private EmojiController _emojiController;
    [SerializeField] private Transform _imagesObj;
    [SerializeField] private List<Image> _ingredientImages;
    [SerializeField] private List<Image> _UIImages;

    private Visitor _visitor;
    public EmojiController EmojiController => _emojiController;

    public void InitTask(PotionTask task,Visitor visitor)
    {
        _visitor = visitor;
        _visitor.OnVisitorSleep += DisableLables;
        _visitor.OnVisitorSleep += _emojiController.SetSleepEmoji;

        ResetIngredientImages();
        FillImageTask(task.Images);
    }

    /// <summary>
    /// Картиночная версия задания
    /// </summary>
    /// <param name="ingredientSprites"></param>
    /// <param name="reward"></param>
    public void FillImageTask(Sprite[] ingredientSprites)
    {
        for (int i = 0; i < ingredientSprites.Length; i++)
        {
            _ingredientImages[i].enabled = true;
            _ingredientImages[i].sprite = ingredientSprites[i];            
        }
    }

    private void ResetIngredientImages()
    {
        _imagesObj.gameObject.SetActive(true);
        foreach (var item in _ingredientImages)
        {
            item.enabled = false;
            item.sprite = null;
        }
    }

    public void Rising()
    {       
        foreach (var item in _UIImages)
        {
            DOTween.ToAlpha(() => item.color, x => item.color = x, 1, risingTime).SetEase(Ease.InExpo);
        }

        foreach (var item in _ingredientImages)
        {
            DOTween.ToAlpha(() => item.color, x => item.color = x, 1, risingTime).SetEase(Ease.InExpo);
        }
    }

    public void Fading()
    {
        //выключаем лейблы задания
        DisableLables(true);
        _emojiController.ShowEmoji(); // показываем Emoji
        _emojiController.FadeEmoji(fadingTime);

        foreach (var item in _UIImages)
        {
            DOTween.ToAlpha(() => item.color, x => item.color = x, 0, fadingTime).OnComplete(FadingShowBox);
        }
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

    private void OnDisable()
    {
        _visitor.OnVisitorSleep -= DisableLables;
        _visitor.OnVisitorSleep -= _emojiController.SetSleepEmoji;
    }
}

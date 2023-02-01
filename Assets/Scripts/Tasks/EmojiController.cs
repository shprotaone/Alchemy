using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EmojiController : MonoBehaviour
{
    private const float fadeTime = 0.5f;
    [SerializeField] private EmojiContainer _emojiContainer;
    [SerializeField] private Image _emoji;

    private float _index;
    public void SetEmoji(float index)
    {
        _index = index;

        if(_index == 0)
        {
            _emoji.sprite = _emojiContainer.badEmoji;
        }
        else if(_index == 2)
        {
            _emoji.sprite = _emojiContainer.superEmoji;
        }
        else
        {
            _emoji.sprite = _emojiContainer.goodEmoji;
        }
    }

    public void ShowEmoji()
    {
        _emoji.gameObject.SetActive(true);
        _emoji.DOFade(1, fadeTime);
    }

    public void SetSleepEmoji()
    {
        _emoji.sprite = _emojiContainer.sleepEmoji;
        ShowEmoji();
    }
   

    public void FadeEmoji(float fadeTime)
    {
        _emoji.DOFade(0,fadeTime);     
    }
}

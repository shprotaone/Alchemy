using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CookButton : MonoBehaviour
{
    [SerializeField] private Image _buttonImage;

    public void ResetButtonImage(float resetTime)
    {
        float amount = 0;
        DOTween.To(() => amount, x => amount = x, 1, resetTime).OnUpdate(() => _buttonImage.fillAmount = amount);
    }
}

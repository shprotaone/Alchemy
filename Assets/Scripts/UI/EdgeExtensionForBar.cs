using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// –асширение дл€ отображени€ бутылки в конце слайдера при приготовлении
/// </summary>
public class EdgeExtensionForBar : MonoBehaviour
{
    [SerializeField] private RectTransform _bottleContainer;
    [SerializeField] private RectTransform _bottle;
    [SerializeField] private Image _bar;

    [SerializeField] private float _amountMultiply = 2.5f;

    public void MoveImage(float value)
    {
        float amount = value / _amountMultiply;
        _bar.fillAmount = amount;
        float bottleAngle = amount * 360;
        _bottleContainer.localEulerAngles = new Vector3(0, 0, -bottleAngle);

        _bottle.localEulerAngles = new Vector3(0, 0, bottleAngle);
    }

}

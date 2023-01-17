using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EdgeExtensionForBar : MonoBehaviour
{
    [SerializeField] private RectTransform _bottleContainer;
    [SerializeField] private RectTransform _bottle;
    [SerializeField] private Image _bar;

    public void MoveImage(float value)
    {
        float amount = value / 2.5f;
        _bar.fillAmount = amount;
        float bottleAngle = amount * 360;
        _bottleContainer.localEulerAngles = new Vector3(0, 0, -bottleAngle);

        _bottle.localEulerAngles = new Vector3(0, 0, bottleAngle);
    }

}

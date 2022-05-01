using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterColorv2 : MonoBehaviour
{
    [SerializeField] private Image _waterImage;

    private Color _resultColor;

    public Color ResultColor => _resultColor;

    public void ColorWater(List<Color> color)
    {
        Color resultSumColor = new Color();

        foreach (var item in color)
        {
            resultSumColor += item;
        }

        resultSumColor = resultSumColor / color.Count;

        _resultColor = resultSumColor;
        _waterImage.color = resultSumColor;

    }

    public void SetColor(Color color)
    {
        _waterImage.color = color;
    }
}

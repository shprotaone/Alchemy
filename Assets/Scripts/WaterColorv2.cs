using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WaterColorv2 : MonoBehaviour
{
    [SerializeField] private Image _waterImage;
    [SerializeField] private Material _boilMaterial;

    [SerializeField] private Sprite _waterSprite;
    [SerializeField] private Sprite _boiledWaterSprite;
    
    private Color _resultColor;

    public Color ResultColor => _resultColor;

    private void Start()
    {
        _waterImage.material.color = Color.white;
        _waterImage.sprite = _waterSprite;
    }
    public void ColorWater(List<Color> color)
    {
        Color resultSumColor = new Color();

        foreach (var item in color)
        {
            resultSumColor += item;
        }

        resultSumColor = resultSumColor / color.Count;

        _resultColor = resultSumColor;
        _waterImage.material.DOColor(_resultColor, 1);
    }

    public void Boiled()
    {
        _waterImage.color = _resultColor;
        _waterImage.sprite = _boiledWaterSprite;
        _waterImage.material = _boilMaterial;        
    }

    public void StopBoiled()
    {
        _waterImage.sprite = _waterSprite;
        _waterImage.material = null;
    }

    public void SetColor(Color color)
    {
        _waterImage.color = color;
    }    
}

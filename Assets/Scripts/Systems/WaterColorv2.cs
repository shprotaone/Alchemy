using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WaterColorv2 : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _waterImage;
    [SerializeField] private Material _boilMaterial;

    [SerializeField] private Sprite _waterSprite;
    [SerializeField] private Sprite _boiledWaterSprite;

    private List<Color> _colors;
    private Color _resultColor;

    public Color ResultColor => _resultColor;

    private void Start()
    {
        _colors = new List<Color>();
        _waterImage.material.color = Color.white;
        _waterImage.sprite = _waterSprite;
    }

    public void AddColor(Color color)
    {
        Color resultColor = new Color();
        _colors.Add(color);

        foreach (var col in _colors)
        {
            resultColor += col;
        }
        _resultColor = resultColor / _colors.Count;

        _waterImage.DOColor(_resultColor, 1);
    }

    public void SetColorWater(List<Color> color)
    {
        Color resultSumColor = new Color();

        foreach (var item in color)
        {
            resultSumColor += item;
        }

        resultSumColor = resultSumColor / color.Count;

        _resultColor = resultSumColor;
        _waterImage.DOColor(_resultColor, 1);
    }

    public void StopBoiled()
    {
        _waterImage.sprite = _waterSprite;
        //_waterImage.material = null;
    }

    public void ResetWaterColor(Color color)
    {
        _waterImage.DOColor(color, 1);    
        _colors.Clear();
        _resultColor = Color.white;
    }    
}

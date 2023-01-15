using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WaterColorv2 : MonoBehaviour
{
    [SerializeField] private ClaudronEffectController _effectController;
    [SerializeField] private SpriteRenderer _waterUpSprite;
    [SerializeField] private SpriteRenderer _waterDownSprite;
    [SerializeField] private Color _resetColor;

    private List<Color> _colors;
    private Color _resultColor;

    public Color ResultColor => _resultColor;

    private void Start()
    {
        _colors = new List<Color>();
        _waterUpSprite.material.color = Color.white;
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

        _waterUpSprite.DOColor(_resultColor, 1);
        _waterDownSprite.DOColor(_resultColor, 1);

        _effectController.IngredientBulk(_resultColor);
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
        _waterUpSprite.DOColor(_resultColor, 1);
        _waterDownSprite.DOColor(_resultColor, 1);
    }

    public void ResetWaterColor()
    {
        _waterUpSprite.DOColor(_resetColor, 1);
        _waterDownSprite.DOColor(_resetColor, 1);
        _colors.Clear();
        _resultColor = Color.white;
    }    
}

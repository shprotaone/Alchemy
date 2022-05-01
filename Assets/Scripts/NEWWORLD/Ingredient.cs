using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ingredient : MonoBehaviour
{
    [SerializeField] private Image _dragableImage;
    [SerializeField] private Color _color;

    public IngredientData IngredientData { get; set; }

    public Color IngredienColor => _color;

    private void Start()
    {
        _dragableImage = GetComponent<Image>();
        _dragableImage.sprite = IngredientData.dragableSprite;
        _color = IngredientData.color;
    }

    public void DestroyIngredient()
    {
        this.gameObject.SetActive(false);
    }
}

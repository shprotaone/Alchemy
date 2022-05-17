using UnityEngine;
using UnityEngine.UI;

public class Ingredient : MonoBehaviour
{   
    [SerializeField] private Image _dragableImage;
    [SerializeField] private Color _color;

    private bool _isRareIngredient;
    private CanvasGroup _canvasGroup;
    public IngredientData IngredientData { get; set; }  //осторожно!

    public Color IngredienColor => _color;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.blocksRaycasts = false;

        _dragableImage = GetComponent<Image>();
        _dragableImage.sprite = IngredientData.dragableSprite;
        _color = IngredientData.color;
    }

    public void DestroyIngredient()
    {
        this.gameObject.SetActive(false);
    }
}

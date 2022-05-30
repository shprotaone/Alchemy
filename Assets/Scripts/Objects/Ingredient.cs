using UnityEngine;
using UnityEngine.UI;

public class Ingredient : MonoBehaviour
{   
    [SerializeField] private Image _dragableImage;
    [SerializeField] private Color _color;

    private Slot _slot;
    private bool _isRareIngredient;
    private CanvasGroup _canvasGroup;
    public IngredientData IngredientData { get; set; }  //осторожно!

    public Color IngredienColor => _color;
    public Slot Slot => _slot;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.blocksRaycasts = false;

        _dragableImage = GetComponent<Image>();
        _dragableImage.sprite = IngredientData.dragableSprite;
        _color = IngredientData.color;
    }

    public void SetSlot(Slot slot)
    {
        _slot = slot;
    }
    public void DestroyIngredient()
    {
        this.gameObject.SetActive(false);
    }
}

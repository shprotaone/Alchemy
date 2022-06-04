using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Ingredient : MonoBehaviour
{
    private const float moveSpeed = 1;

    [SerializeField] private Image _dragableImage;
    [SerializeField] private Color _color;

    private Slot _slot;
    private IngredientData _ingredientData;
    private CanvasGroup _canvasGroup;

    public IngredientData IngredientData => _ingredientData; 
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

    public void Movement()
    {
        transform.DOMove(_slot.transform.position, moveSpeed, false).OnComplete(DestroyIngredient);
    }

    public void SetSlot(Slot slot)
    {
        _slot = slot;
    }

    public void SetIngredientData(IngredientData ingredient)
    {
        _ingredientData = ingredient;
    }

    public void DisableIngredient()
    {
        this.gameObject.SetActive(false);
    }

    private void DestroyIngredient()
    {
        Destroy(this.gameObject);
    }
}

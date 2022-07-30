using UnityEngine;
using TMPro;

public class Slot : MonoBehaviour,IAction
{
    [SerializeField] private BoxCollider2D _boxCollider;
    [SerializeField] private SpriteRenderer _slotImage;
    [SerializeField] private TMP_Text _amountText;

    private Inventory _inventory;
    private IngredientData _ingredientData;   
         
    private GameObject _draggableIngredientPrefab;
    private float _amountInSlot;

    public SpriteRenderer SlotImage => _slotImage;
    public TMP_Text AmountText => _amountText;
    public BoxCollider2D SlotCollider => _boxCollider;

    private void OnEnable()
    {
        _inventory = GetComponentInParent<Inventory>();        
    }

    public void FillSlot(IngredientData ingredientData,int value)
    {       
        _ingredientData = ingredientData;
        _draggableIngredientPrefab = _inventory.CurrentPrefab;
        _slotImage.sprite = ingredientData.mainSprite;
        RefreshAmount();
    }

    public void OnBeginDrag()
    {           
        if (_inventory.DragFromInventory(_ingredientData))
        {                    
            GameObject ingredientGO = Instantiate(_draggableIngredientPrefab, this.transform);
            Ingredient ingredient = ingredientGO.GetComponent<Ingredient>();

            ingredient.SetIngredientData(_ingredientData);

            ingredient.SetSlot(this);

            DecreaseAmount();
        }
    }

    public void IncreaseAmount()
    {
        _inventory.AddIngredient(_ingredientData);        
        RefreshAmount();
    }

    public void DecreaseAmount()
    {
        _inventory.DecreaseIngredient(_ingredientData);
        RefreshAmount();
    }

    private void RefreshAmount()
    {
        _amountInSlot = _inventory.ShowIngredientValue(_ingredientData);

        if (_amountInSlot > 0) _boxCollider.enabled = true;
        _amountText.text = _amountInSlot.ToString();
        
    }

    public void Action()
    {
        OnBeginDrag();
    }

    public void Movement() { }
}


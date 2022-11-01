using UnityEngine;
using TMPro;

public class Slot : MonoBehaviour,IAction,IInterract
{
    [SerializeField] private BoxCollider2D _boxCollider;
    [SerializeField] private SpriteRenderer _slotImage;
    [SerializeField] private TMP_Text _amountText;
    [SerializeField] private FloatTimer _timer;
    [SerializeField] private float _delay;
    [SerializeField] private bool _interactive;

    private Inventory _inventory;
    private IngredientData _ingredientData;
         
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
        _slotImage.sprite = ingredientData.mainSprite;
        RefreshAmount();
    }

    public void StartDrag()
    {
        if (_inventory.DragFromInventory(_ingredientData) && !_timer.TimerIsRunning && _interactive)
        {
            GameObject ingredientGO = ObjectPool.SharedInstance.GetObject(ObjectType.INGREDIENT);
            ingredientGO.transform.SetParent(this.transform);
            Ingredient ingredient = ingredientGO.GetComponent<Ingredient>();

            _timer.InitTimer(_delay);

            ingredient.SetIngredientData(_ingredientData);
            ingredient.SetSlot(this);

            DecreaseAmount();
        }

        RefreshAmount();
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
        StartDrag();
    }

    public void Movement() { print("Hey"); }

    public void SetInterract(bool value)
    {
        _interactive = value;
    }
}


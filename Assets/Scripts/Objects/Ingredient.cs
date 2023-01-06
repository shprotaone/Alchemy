using DG.Tweening;
using UnityEngine;

public class Ingredient : MonoBehaviour,IAction,IPooledObject
{
    private const float moveSpeed = 1;

    [SerializeField] private SpriteRenderer _dragableImage;
    [SerializeField] private AudioClip _backSound;
    [SerializeField] private ObjectType _type;
    [SerializeField] private AudioSource _audioSource;
 
    private Slot _slot;
    private IngredientData _ingredientData;
    
    private bool _inClaudron;

    public ResourceType ResourceType { get; private set; }
    public IngredientData IngredientData => _ingredientData; 
    public Color IngredienColor { get; private set; }
    public ObjectType Type => _type;

    public void Drop()
    {
        if(_slot != null)
        {
          transform.DOMove(_slot.transform.position, moveSpeed, false).OnComplete(ReturnToSlot).SetEase(Ease.Unset);
        }    
        _audioSource?.PlayOneShot(_backSound);
    }

    public void SetSlot(Slot slot)
    {
        _slot = slot;
    }

    public void SetInClaudron(bool flag)
    {
        _inClaudron = flag;
        DisableIngredient();
    }

    public void SetIngredientData(IngredientData ingredient)
    {
        _ingredientData = ingredient;
        ResourceType = ingredient.resourceType;
        DrawIngredient();
        _audioSource.PlayOneShot(_ingredientData.dragSound);
    }

    private void DrawIngredient()
    {
        _dragableImage.sprite = IngredientData.dragableSprite;
        IngredienColor = IngredientData.color;
    }

    public void DisableIngredient()
    {
        this.gameObject.SetActive(false);
    }

    private void ReturnToSlot()
    {
        if (!_inClaudron)
        {
            _slot.IncreaseAmount();

            ObjectPool.SharedInstance.DestroyObject(gameObject);
        }
    }

    public void Action()
    {
        print("Iam DRAG");
    }

    private void OnDisable()
    {
        _slot = null;
        _inClaudron = false;
    }
}

using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Ingredient : MonoBehaviour
{
    private const float moveSpeed = 1;

    [SerializeField] private SpriteRenderer _dragableImage;
    [SerializeField] private Color _color;
    [SerializeField] private AudioClip _backSound;

    private GameObject _effectPrefab;
 
    private Slot _slot;
    private IngredientData _ingredientData;

    private AudioSource _audioSource;
    
    private bool _inClaudron;

    public IngredientData IngredientData => _ingredientData; 
    public Color IngredienColor => _color;
    public Slot Slot => _slot;
    public GameObject EffectPrefab => _effectPrefab;

    private void Start()
    {
        _dragableImage = GetComponent<SpriteRenderer>();
        _dragableImage.sprite = IngredientData.dragableSprite;
        _color = IngredientData.color;
        //_audioSource = GetComponent<AudioSource>();

        //_audioSource.PlayOneShot(_ingredientData.dragSound);
    }

    public void Movement()
    {
        transform.DOMove(_slot.transform.position, moveSpeed, false).OnComplete(IngredientInClaudron);
        //_audioSource.PlayOneShot(_backSound);
    }

    public void SetSlot(Slot slot)
    {
        _slot = slot;
    }

    public void SetInClaudron(bool flag)
    {
        _inClaudron = flag;
    }

    public void SetIngredientData(IngredientData ingredient)
    {
        _ingredientData = ingredient;

        if(ingredient.effect != null)
        {
            _effectPrefab = ingredient.effect;
        }
    }

    public void DisableIngredient()
    {
        this.gameObject.SetActive(false);
    }

    private void IngredientInClaudron()
    {
        if (!_inClaudron)
        {
            _slot.IncreaseAmount();           
        }

        DisableIngredient();
    }
}

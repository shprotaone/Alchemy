using DG.Tweening;
using TMPro;
using UnityEngine;

public class Bottle : MonoBehaviour,IAction,IPooledObject
{
    private const float moveSpeed = 1;

    [SerializeField] protected TMP_Text _timerText;

    [SerializeField] private SpriteRenderer _waterInBottle;
    [SerializeField] private SpriteRenderer _bottle;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private Wobble _wobble;
    [SerializeField] private ObjectType _type;
    [SerializeField] private string _namePotionInBottle;

    private Transform _destination;

    private GameObject _effectInBottle;
    private Potion _potionInBottle;
    private Color _potionColor;
    private Color _contrabandBottleColor = Color.black;

    private TableManager _tableManager;
    private BottleStorage _bottleStorage;

    private bool _isFull;

    private int _contrabandTime;
    public bool IsFull => _isFull;
    public Potion PotionInBottle => _potionInBottle;
    public ObjectType Type => _type;

    public void InitBottle(BottleStorage storage,TableManager tableManager, int contrabandTime)
    {             
        _bottleStorage = storage;
        _contrabandTime = contrabandTime;
        _tableManager = tableManager;
    }

    public void SetPotion(Potion potion)
    {
        if (!_isFull)
        {
            _potionInBottle = new Potion(potion);
            _isFull = true;

            FillColorWater(potion.ColorPotion);

            AddEffect(ObjectPool.SharedInstance.GetObject(potion.EffectType));
            CheckContraband();

            print("From Bottle" + _potionInBottle.PotionName);

            //transform.SetParent(_tableManager.FullPotionTable.transform);
            _namePotionInBottle = _potionInBottle.PotionName;
        }
        else
        {
            Debug.LogWarning("Бутылка уже заполнена");
        }
    }
    public void SetPosition(Transform slotTransform)
    {
        _destination = slotTransform;
        transform.SetParent(slotTransform);
    }

    public void Drop()
    {
        GetTable();
        transform.DOMove(_destination.position, moveSpeed, false).OnComplete(ReturnBottleToBox);             
    }

    public void DropFromGarbage()   //без повторной ативации OnComplete, спорное решение
    {
        GetTable();
        transform.DOMove(_destination.position, moveSpeed, false);
    }

    private void FillColorWater(Color color)
    {
        _potionColor = color;
        _waterInBottle.enabled = true;
        _wobble.ChangeColor(_potionColor);
    }

    private void AddEffect(GameObject effect)
    {
        if (_potionInBottle.Rarity == ResourceRarity.rare)
        {
            _effectInBottle = effect;
            _effectInBottle.transform.position = transform.position;
            _effectInBottle.transform.SetParent(transform);
            _effectInBottle.transform.localScale = new Vector3(1, 1, 0);

            _effectInBottle.GetComponentInChildren<Effect>().ChangeParticleColor(_potionColor);
        }
    }

    private void CheckContraband()
    {
        if (_potionInBottle.Contraband)      
        {
            _bottle.color = _contrabandBottleColor;
            _timerText.gameObject.SetActive(true);
            //StartTimer();                         //вынести контрабадные зелья и наполнения в отдельные классы? 
        }
        else
        {
            _bottle.color = Color.white; 
            _timerText.gameObject.SetActive(false);
        }
    }

    private void GetTable()
    {        
        if(_tableManager != null)
        {
            if (!_isFull)
                _destination = _tableManager.EmptyPotionTable.transform;         
        }
        else
        {
            Debug.Log("Стол не указан");
        }
   
    }

    private void SortInFullTable()
    {
        _tableManager.FullPotionTable.SortBottlePosition();
        transform.SetParent(_destination);              
    }

    private void ReturnBottleToBox()
    {       
        _collider.enabled = true;

        if (!IsFull)
        {
            _bottleStorage.ReturnBottle();
            DestroyBottle();
        }
    }

    public void DestroyBottle()
    {
        DOTween.Kill(true);
        ObjectPool.SharedInstance.DestroyObject(gameObject);

        ReturnEffect();
    }

    public void ResetBottle()
    {
        ReturnEffect();

        _waterInBottle.enabled = false;
        _isFull = false;
        _collider.enabled = false;
    }

    private void ReturnEffect()
    {
        if(_effectInBottle != null)
        {
            ObjectPool.SharedInstance.DestroyObject(_effectInBottle);
        }
    }

    public void Action()
    {
       
    }
}


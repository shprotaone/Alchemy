using TMPro;
using UnityEngine;

public class BottleStorage : MonoBehaviour,IAction
{
    private const int standartBottleCount = 20;

    [SerializeField] private Transform _parentBottle;
    [SerializeField] private BoxCollider2D _boxCollider;
    [SerializeField] private TMP_Text _amountText;

    [SerializeField] private GameObject _bottlePrefab;
    [SerializeField] private Table _table;

    private int _amount;

    private void OnEnable()
    {
        _amount = standartBottleCount;
        RefreshAmount();
    }

    private void StartDrag()
    {
        if (_amount > 0)
        {
            DecreaseAmount();
            GameObject bottleGO = Instantiate(_bottlePrefab,this.transform);
            Bottle bottle = bottleGO.GetComponent<Bottle>();

            bottle.InitBottle(this);
            //bottle.SetTable();            
        }
    }

    public void AddBottle(int value)
    {
        _amount += value;
        RefreshAmount();
    }

    private void DecreaseAmount()
    {
        _amount--;
        RefreshAmount();
    }

    public void IncreaseAmount()
    {
        _amount++;
        //_bottlePrefab.SetActive(true);
        _boxCollider.enabled = true;

        RefreshAmount();
    }

    private void RefreshAmount()
    {
        _amountText.text = _amount.ToString();
    }

    public void Action()
    {
        StartDrag();
    }

    public void Movement()
    {
        
    }
}

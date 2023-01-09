using TMPro;
using UnityEngine;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyText;   

    public void Init()
    {
        Money.OnChangeMoney += RefreshMoneyText;
    }
    public void RefreshMoneyText(int value)
    {
        _moneyText.text = value.ToString();       
    }
}

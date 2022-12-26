using TMPro;
using UnityEngine;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private TMP_Text _moneyTextInShop;

    public void Init()
    {
        Money.OnChangeMoney += RefreshMoneyText;
    }
    private void RefreshMoneyText(int value)
    {
        _moneyText.text = value.ToString();
        _moneyTextInShop.text = value.ToString();
    }
}

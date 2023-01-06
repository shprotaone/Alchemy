using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TradeSystemView : MonoBehaviour
{
    [SerializeField] private Button _tradeButton;
    [SerializeField] private Button _declineButton;
    [SerializeField] private TMP_Text _rewardText;
    [SerializeField] private TMP_Text _multiplyText;

    public void Init(TradeSystem tradeSystem)
    {
        _tradeButton.onClick.AddListener(tradeSystem.Trade);
        _declineButton.onClick.AddListener(tradeSystem.DeclineTrade);
    }

    public void Refresh(int reward)
    {
        _rewardText.text = reward.ToString();
    }

    public void RefreshMultiply(float multiply)
    {
        _multiplyText.text = multiply.ToString();
    }
}

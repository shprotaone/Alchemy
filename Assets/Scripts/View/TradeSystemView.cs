using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TradeSystemView : MonoBehaviour
{
    [SerializeField] private Button _tradeButton;
    [SerializeField] private Button _declineButton;
    [SerializeField] private TMP_Text _multiplyText;
    [SerializeField] private TMP_Text _rewardText;

    public void Init(TradeSystem tradeSystem)
    {    
        _tradeButton.interactable = false;
        _tradeButton.onClick.AddListener(tradeSystem.Trade);
        _declineButton.onClick.AddListener(tradeSystem.DeclineTrade);
    }

    public void TradeButtoneControl(int reward)
    {
        if(reward == 0)
        {
            _tradeButton.interactable = false;
        }
        else
        {
            _tradeButton.interactable = true;
        }
    }

    public void Refresh(int reward)
    {
        _rewardText.text = reward.ToString();
    }

    public void RefreshMultiply(float multiply)
    {
        _multiplyText.text = Math.Round((double)multiply, 2).ToString(); ;
    }

    public void Disable()
    {
        _tradeButton.onClick.RemoveAllListeners();
        _declineButton.onClick.RemoveAllListeners();
    }
}

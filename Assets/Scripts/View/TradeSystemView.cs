using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TradeSystemView : MonoBehaviour
{
    [SerializeField] private Button _tradeButton;
    [SerializeField] private Button _declineButton;
    [SerializeField] private TMP_Text _multiplyText;
    [SerializeField] private TMP_Text _rewardText;

    [SerializeField] private Transform _payTransform;
    [SerializeField] private Transform _jarTransform;
    public void Init(TradeSystem tradeSystem)
    {    
        _tradeButton.interactable = false;
        _tradeButton.onClick.AddListener(tradeSystem.Trade);
        _declineButton.onClick.AddListener(tradeSystem.DeclineTrade);
    }

    public void DeclineButtonDelay()
    {
        _declineButton.interactable = false;
        DOVirtual.DelayedCall(3f, () => _declineButton.interactable = true);
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
        int result = 0;
        DOTween.To(() => result, x => result = x, reward, 0.3f)
            .OnUpdate(() => _rewardText.text = result.ToString());
    }

    public void RestartMultiply(float multiply)
    {
        _multiplyText.text = Math.Round((double)multiply, 2).ToString(); ;
    }

    public void StartCoinAnimation()
    {
        StartCoroutine(CoinAnimRoutine());
    }

    private IEnumerator CoinAnimRoutine()
    {
        int count = 5;
        while (count >= 0)
        {
            GameObject curCoin = ObjectPool.SharedInstance.GetObject(ObjectType.COINDROP);
            curCoin.transform.position = _payTransform.position;
            Coin coin = curCoin.GetComponent<Coin>();
            coin.Movement(_jarTransform.position);

            yield return new WaitForSeconds(0.1f);
            count--;
        }
    }

    public void Disable()
    {
        _tradeButton.onClick.RemoveAllListeners();
        _declineButton.onClick.RemoveAllListeners();
    }
}

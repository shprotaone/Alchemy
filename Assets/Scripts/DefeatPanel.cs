using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DefeatPanel : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private TMP_Text _rewardText;
    [SerializeField] private RewardGem _rewardGem;
    [SerializeField] private Money _money;
    [SerializeField] private ContrabandPotionSystem _contrabandPotionSystem;
    [SerializeField] private LevelInitializator _levelInitializator;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(Restart);
        _exitButton.onClick.AddListener(Exit);

        if (_contrabandPotionSystem.IsActive)
        {
            _rewardGem = new RewardGem();
            _rewardGem.CalculateReward(_contrabandPotionSystem.CompleteCounter);
            _rewardGem.CalculateFromMoney(_money.CurrentMoney);
            _rewardGem.Penalty(_money.CurrentMoney);

            _rewardText.text = "Вы заработали " + _rewardGem.RewardCounter;
        }
    }

    private void Restart()
    {
        //_levelInitializator.SetRestartLevel(true);
        SceneManager.LoadScene(1);
    }

    private void Exit()
    {
        //_levelInitializator.SetRestartLevel(false);
        SceneManager.LoadScene(0);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(Restart);
        _exitButton.onClick.RemoveListener(Exit);
    }
}

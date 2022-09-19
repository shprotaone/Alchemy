using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public static Action<float> OnSecondChange;

    [SerializeField] private GameManager _gameManager;
    [SerializeField] private RentShop _rentShop;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private Money _money;
    
    private float _gameTime;
    private float _seconds;

    public float Seconds => _seconds;

    /// <summary>
    /// Задает таймер
    /// </summary>
    /// <param name="seconds">общее время в секундах</param>
    /// <param name="flag">включать или нет?</param>
    public void InitTimer(float seconds, bool flag)
    {
        if (flag)
        {
            _gameTime = seconds;
            StartCoroutine(StartTimer());
        }
        else
        {
            _timerText.gameObject.SetActive(false);
        }
    }

    private IEnumerator StartTimer()
    {
        while(_gameTime > 0)
        {
           _gameTime--;
            UpdateTimeText();

            if(_gameTime % 5 == 0)
            OnSecondChange?.Invoke(_gameTime);

            yield return new WaitForSeconds(1);
        }

        _gameManager.DefeatLevel();
    }

    private void UpdateTimeText()
    {
        float minutes = Mathf.FloorToInt(_gameTime / 60);
        float seconds = Mathf.FloorToInt(_gameTime % 60);

        _timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    private void OnDisable()
    {
        StopCoroutine(StartTimer());
    }
}

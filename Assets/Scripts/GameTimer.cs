using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;
    
    private float _gameTime;

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
           _gameTime -=Time.deltaTime;
            UpdateTimeText();
            yield return null;
        }

        Debug.LogWarning("LOOSE");
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

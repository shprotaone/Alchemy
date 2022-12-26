using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTimeView : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;

    public void UpdateTimeText(int gameTime)
    {
        int minutes = Mathf.FloorToInt(gameTime / 60);
        int seconds = Mathf.FloorToInt(gameTime % 60);

        _timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}

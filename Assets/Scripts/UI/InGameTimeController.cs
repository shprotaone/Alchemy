using System;
using UnityEngine;

public class InGameTimeController : MonoBehaviour
{
    private void Awake()
    {
        ResumeGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0.001f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}

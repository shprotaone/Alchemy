using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameTimeController : MonoBehaviour
{
    public void PauseGame()
    {
        Time.timeScale = 0.001f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}

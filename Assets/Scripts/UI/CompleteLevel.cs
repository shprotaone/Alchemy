using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CompleteLevel : MonoBehaviour,IMenu
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _restartButton;

    private void Start()
    {
        _exitButton?.onClick.AddListener(Exit);
        _restartButton?.onClick.AddListener(Restart);
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{       
    //����� ������������ �� int, � ���������, �� �� ������ ��� ���� ��������.
    public void LoadLevel(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TempStuff : MonoBehaviour
{
    private void Start()
    {
        //ClearSave();
    }

    public void ClearSave()
    {
        Time.timeScale = 0;
        File.Delete(Path.Combine(Application.persistentDataPath, "data.save"));
        Application.Quit();
    }
}
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UniTaskTextTest : MonoBehaviour
{
    [SerializeField] private Text _text;
    private int i;

    void Update()
    {
        GetClick();
    }

    async private void GetClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            await UniTask.Delay(TimeSpan.FromSeconds(2f));
            IncrementText();
        }
    }

    private void IncrementText()
    {
        i++;
        _text.text = i.ToString();
    }
}

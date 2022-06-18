using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestOnClick : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private void OnMouseDown()
    {
        print(this.gameObject.name);
        _text.text = this.gameObject.name;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonController : MonoBehaviour
{
    [SerializeField] private Button[] _buttons;

    public Button[] Buttons => _buttons;

    private void Start()
    {
        _buttons = GetComponentsInChildren<Button>();
    }
}

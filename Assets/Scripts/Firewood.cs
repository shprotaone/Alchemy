using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Firewood : MonoBehaviour
{
    [SerializeField] private int _fireWoodCount = 0;
    [SerializeField] private Button _addFireWoodButton;
    [SerializeField] private TMP_Text _count;
    [SerializeField] private CircularProgressBar _activeTimeSlider;

    private float _actionTime = 10;
    private float _speedMultiply = 0.1f;
    private bool _activated;

    public float SpeedMultiply => _speedMultiply;
    public bool Activated => _activated;
    private void Start()
    {
        _addFireWoodButton.onClick.AddListener(AddFireWood);
        UpdateText(_fireWoodCount);

        if (_fireWoodCount == 0)
            _addFireWoodButton.interactable = false;
    }

    public void AddFireWood()
    {
        _fireWoodCount++;           //добавить списание денег
        UpdateText(_fireWoodCount);
    }

    public void ActivateFirewood()
    {
        if(_fireWoodCount != 0)
        {
            _fireWoodCount--;
            UpdateText(_fireWoodCount);
            _activated = true;
            StartCoroutine(Timer());
        }
        else
        {
            print("Firewood Empty");
        }        
    }

    private IEnumerator Timer()
    {
        float startTimer = 0;

        while(startTimer < _actionTime)
        {
            startTimer += Time.deltaTime;
            _activeTimeSlider.SetValue(startTimer / 10);
            
            yield return new WaitForFixedUpdate();
        }

        _activated = false;
    }

    private void UpdateText(int value)
    {
        _count.text = value.ToString();
    }
}

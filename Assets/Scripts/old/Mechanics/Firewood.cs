using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Firewood : MonoBehaviour
{    
    [SerializeField] private Button _addFireWoodButton;
    [SerializeField] private TMP_Text _count;
    [SerializeField] private CircularProgressBar _activeTimeSlider;

    private int _fireWoodCount;
    private float _actionTime = 10;
    private float _speedMultiply = 0.1f;
    private bool _activated;

    public float SpeedMultiply => _speedMultiply;
    public bool Activated => _activated;
    private void Start()
    {
        _activeTimeSlider.gameObject.SetActive(false);
        UpdateState();      
    }

    public void AddFireWood()
    {
        _fireWoodCount++;           //добавить списание денег
        UpdateState();
    }

    public void ActivateFirewood()
    {
        if(_fireWoodCount != 0)
        {
            _fireWoodCount--;
            UpdateState();
            _activated = true;

            _addFireWoodButton.interactable = false;
            _activeTimeSlider.gameObject.SetActive(true);

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

        _activeTimeSlider.SetValue(0);
        _activeTimeSlider.gameObject.SetActive(false);
        UpdateState();

        _activated = false;
    }

    private void UpdateState()
    {
        if (_fireWoodCount == 0)
            _addFireWoodButton.interactable = false;
        else
            _addFireWoodButton.interactable = true;

        _count.text = _fireWoodCount.ToString(); ;
    }
}

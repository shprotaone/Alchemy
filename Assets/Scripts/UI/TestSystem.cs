using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text _prevVisitor;
    [SerializeField] private TMP_Text _prevResource;
    [SerializeField] private TMP_InputField _visitorField;
    [SerializeField] private TMP_InputField _resourceField;

    [SerializeField] private TMP_Text _prevChance1;
    [SerializeField] private TMP_Text _prevChance2;
    [SerializeField] private TMP_Text _prevChance3;
    [SerializeField] private TMP_InputField _chance1Field;
    [SerializeField] private TMP_InputField _chance2Field;
    [SerializeField] private TMP_InputField _chance3Field;


    [SerializeField] private TMP_Text _prevWaterChance;
    [SerializeField] private TMP_Text _prevFireChance;
    [SerializeField] private TMP_Text _prevStoneChance;
    [SerializeField] private TMP_InputField _chanceWaterField;
    [SerializeField] private TMP_InputField _chanceFireField;
    [SerializeField] private TMP_InputField _chanceStoneField;

    [SerializeField] private LevelPreset _preset;
    [SerializeField] private LevelInitializator _levelLoad;

    [SerializeField] private Button _testButton;
    [SerializeField] private Button _enableButton;
    [SerializeField] private Toggle _withEvent;

    private void Start()
    {
        _testButton.onClick.AddListener(SetNewSettings);

        FillOldValue();
    }

    private void ActivatePanel(bool value)
    {
        gameObject.SetActive(value);
    }
    private void FillOldValue()
    {
        _prevVisitor.text = _preset.visitorCount.ToString();
        _prevResource.text = _preset.addCommonResourceCount.ToString();

        _prevChance1.text = _preset.chance1Label.ToString();
        _prevChance2.text = _preset.chance2Label.ToString();
        _prevChance3.text = _preset.chance3Label.ToString();

        _prevWaterChance.text = _preset.chanceWater.ToString();
        _prevFireChance.text = _preset.chanceFire.ToString();
        _prevStoneChance.text = _preset.chanceStone.ToString();

        _withEvent.isOn = _preset.withEvent;

    }
    private void SetNewSettings()
    {
        bool checkFill = _visitorField.text != null &&
                         _resourceField.text != null &&
                         _chance1Field.text != null &&
                         _chance2Field.text != null &&
                         _chance3Field.text != null &&
                         _chanceWaterField.text != null &&
                         _chanceFireField.text != null &&                         
                         _chanceStoneField.text != null;


        if (checkFill)
        {
            _preset.visitorCount = int.Parse(_visitorField.text);
            _preset.addCommonResourceCount = int.Parse(_resourceField.text);
            _preset.chance1Label = int.Parse(_chance1Field.text);
            _preset.chance2Label = int.Parse(_chance2Field.text);
            _preset.chance3Label = int.Parse(_chance3Field.text);

            _preset.chanceWater = int.Parse(_chanceWaterField.text);
            _preset.chanceFire = int.Parse(_chanceFireField.text);
            _preset.chanceStone = int.Parse(_chanceStoneField.text);

            _preset.withEvent = _withEvent.isOn;

            _levelLoad.DisableLevel();
            
            DOVirtual.DelayedCall(1, () => _levelLoad.LoadNextLevel(_preset));
            ActivatePanel(false);

        }
        else
        {
            Debug.LogError("Что то не заполнено");
        }       
    }
}

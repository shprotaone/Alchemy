using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestSystem : MonoBehaviour
{
    [SerializeField] private TMP_InputField _visitorField;
    [SerializeField] private TMP_InputField _resourceField;
    [SerializeField] private TMP_InputField _chance1Field;
    [SerializeField] private TMP_InputField _chance2Field;
    [SerializeField] private TMP_InputField _chance3Field;

    [SerializeField] private TMP_Text _prevVisitor;
    [SerializeField] private TMP_Text _prevResource;
    [SerializeField] private TMP_Text _prevChance1;
    [SerializeField] private TMP_Text _prevChance2;
    [SerializeField] private TMP_Text _prevChance3;

    [SerializeField] private TMP_Text _prevWeight;

    [SerializeField] private LevelPreset _preset;
    [SerializeField] private CompleteLevel _loader;

    [SerializeField] private Button _button;
    private void Start()
    {
        _button.onClick.AddListener(SetNewSettings);

        FillOldValue();

    }

    private void FillOldValue()
    {
        _prevVisitor.text = _preset.visitorCount.ToString();
        _prevResource.text = _preset.addCommonResourceCount.ToString();
        _prevChance1.text = _preset.chance1Label.ToString();
        _prevChance2.text = _preset.chance2Label.ToString();
        _prevChance3.text = _preset.chance3Label.ToString();

        _prevWeight.text = (_preset.chance1Label + _preset.chance2Label + _preset.chance3Label).ToString();
    }
    private void SetNewSettings()
    {
        bool checkFill = _visitorField.text != null &&
                         _resourceField.text != null &&
                         _chance1Field.text != null &&
                         _chance2Field.text != null &&
                         _chance3Field.text != null;


        if (checkFill)
        {
            _preset.visitorCount = int.Parse(_visitorField.text);
            _preset.addCommonResourceCount = int.Parse(_resourceField.text);
            _preset.chance1Label = int.Parse(_chance1Field.text);
            _preset.chance2Label = int.Parse(_chance2Field.text);
            _preset.chance3Label = int.Parse(_chance3Field.text);

            _loader.Restart();
        }
        else
        {
            Debug.LogError("Что то не заполнено");
        }       
    }
}

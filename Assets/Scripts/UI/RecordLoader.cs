using TMPro;
using UnityEngine;
using YG;

/// <summary>
/// Загрузчик рекорда на главной странице
/// </summary>
public class RecordLoader : MonoBehaviour
{
    public static readonly string RecordName = "Record";

    [SerializeField] private TMP_Text _recordText;

    private int _record;

    private void OnEnable() => YandexGame.GetDataEvent += LoadRecord;
    private void OnDisable() => YandexGame.GetDataEvent -= LoadRecord;

    private void Start()
    {
        if (YandexGame.SDKEnabled) {
            LoadRecord();
        }
    }
    private void LoadRecord()
    {
        _record = YandexGame.savesData.moneyRecord;
        UpdateRecord();
    }

    private void UpdateRecord()
    {
        _recordText.text = _record.ToString();
    }
}

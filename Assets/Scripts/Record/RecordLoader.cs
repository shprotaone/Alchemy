using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecordLoader : MonoBehaviour
{
    public static readonly string RecordName = "Record";

    [SerializeField] private TMP_Text _recordText;

    private int _record;
    void Start()
    {
        LoadRecordFromPrefs();
        _recordText.text = _record.ToString();
    }

    private void LoadRecordFromPrefs()
    {
        _record = PlayerPrefs.GetInt(RecordName, 0);
    }
}

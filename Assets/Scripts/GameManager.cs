using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InGameTimeController _gameTimeController;
    [SerializeField] private CompleteLevel _completeLevelPanel;

    private Money _money;
    public void Init(Money money)
    {
        _money = money;
    }

    public void CompleteLevel()
    {
        _completeLevelPanel.Activated();
        SaveRecord();
    }

    private void SaveRecord()
    {
        int currentRecord = PlayerPrefs.GetInt(RecordLoader.RecordName, 0);

        if(currentRecord < _money.CurrentMoney)
        {
            PlayerPrefs.SetInt(RecordLoader.RecordName, _money.CurrentMoney);
            PlayerPrefs.Save();
        }
    }
}

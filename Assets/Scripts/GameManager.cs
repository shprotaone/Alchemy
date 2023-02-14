using UnityEngine;
using YG;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InGameTimeController _gameTimeController;
    [SerializeField] private CompleteLevel _completeLevelPanel;

    private Money _money;
    private GameProgressSaver _gameSaver;
    public void Init(Money money, GameProgressSaver gameSaver)
    {
        _gameSaver = gameSaver;
        _money = money;
    }

    public void CompleteLevel()
    {
        _completeLevelPanel.Activated();
        _gameSaver.SaveRecord(_money.CurrentMoney);
    }
}

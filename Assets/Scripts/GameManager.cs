using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InGameTimeController _gameTimeController;
    [SerializeField] private CompleteLevel _completeLevelPanel;
    [SerializeField] private GameObject _defeatLevelPanel;

    private void Start()
    {
        _gameTimeController.ResumeGame();
    }

    public void CompleteLevel()
    {
        _completeLevelPanel.Activated();
    }

    public void DefeatLevel()
    {
        _defeatLevelPanel.SetActive(true);
    }

}

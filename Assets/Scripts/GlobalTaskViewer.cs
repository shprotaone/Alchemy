using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GlobalTaskViewer : MonoBehaviour
{
    [SerializeField] private InGameTimeController _gameTimeController;
    [SerializeField] private TMP_Text _globalTaskText;
    [SerializeField] private Button _acceptButton;

    private void OnEnable()
    {
        _acceptButton.onClick.AddListener(DisableViewer);
    }

    public void SetGlobalTaskText(string text)
    {
        _gameTimeController.PauseGame();
        _globalTaskText.text = text;
    }

    private void DisableViewer()
    {
        _gameTimeController.ResumeGame();
        this.gameObject.SetActive(false);
    }
}

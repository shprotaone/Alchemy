using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GlobalTaskViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text _globalTaskText;
    [SerializeField] private Button _acceptButton;

    private void OnEnable()
    {
        _acceptButton.onClick.AddListener(DisableViewer);
    }

    public void SetGlobalTaskText(string text)
    {
        _globalTaskText.text = text;
    }

    private void DisableViewer()
    {
        this.gameObject.SetActive(false);
    }
}

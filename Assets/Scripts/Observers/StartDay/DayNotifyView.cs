using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayNotifyView : MonoBehaviour
{
    [SerializeField] private Button _okButton;
    [SerializeField] private TMP_Text _notifyText;

    public void Init()
    {
        gameObject.SetActive(true);
        _okButton.onClick.AddListener(DisableNotify);

        Activate();
    }

    public void Show(string text)
    {
        _notifyText.text = text;
    }

    private void Activate()
    {
        gameObject.SetActive(true);
    }

    private void DisableNotify()
    {
        gameObject.SetActive(false);
    }
}

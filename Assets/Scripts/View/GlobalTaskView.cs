using TMPro;
using UnityEngine;

public class GlobalTaskView : MonoBehaviour
{
    [SerializeField] private TMP_Text _taskText;

    public void SetLevelTaskText(string text)
    {
        _taskText.text = text;
    }

    public void AddContrabandText(string addText)
    {
        string text = "\n\n ŒÕ“–¿¡¿ÕƒÕŒ≈ «≈À‹≈ - " + addText;

        _taskText.text += text;
    }
}

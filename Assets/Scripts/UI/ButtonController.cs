using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private Button[] _UIButtons;

    private void Start()
    {
        TutorialSystem.OnUIInterract += SetInterractButtons;
    }

    public void SetInterractButtons(bool flag)
    {
        foreach (var item in _UIButtons)
        {
            item.interactable = flag;
        }
    }

    private void OnDisable()
    {
        TutorialSystem.OnUIInterract -= SetInterractButtons;
    }
}

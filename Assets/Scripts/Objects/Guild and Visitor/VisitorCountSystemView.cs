using TMPro;
using UnityEngine;

public class VisitorCountSystemView : MonoBehaviour
{
    [SerializeField] private TMP_Text _countText;

    public void RefreshText(int count)
    {
        _countText.text = count.ToString();
    }
}

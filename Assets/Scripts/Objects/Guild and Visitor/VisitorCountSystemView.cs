using TMPro;
using UnityEngine;

public class VisitorCountSystemView : MonoBehaviour
{
    [SerializeField] private TMP_Text _countText;
    [SerializeField] private TMP_Text _startCountText;

    public void SetStartCountText(int count)
    {
        _startCountText.text = count.ToString();
    }

    public void RefreshText(int count)
    {
        _countText.text = count.ToString();
    }
}

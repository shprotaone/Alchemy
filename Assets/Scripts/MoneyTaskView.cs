using TMPro;
using UnityEngine;

public class MoneyTaskView : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyText;
    
    public void SetTaskText(int value)
    {
        _moneyText.text = value.ToString();
    }
}

using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static event Action<bool> OnShopSlotDisabled;

    [SerializeField] private Button[] _UIButtons;


    public void SetInterractAllButtons(bool flag)
    {
        foreach (var item in _UIButtons)
        {
            item.interactable = flag;
        }

        Debug.LogWarning("UI " + flag);
    }

    public void ShopSlotController(bool flag)
    {
        OnShopSlotDisabled?.Invoke(flag);
    }
}

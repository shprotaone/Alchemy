using System;
using TMPro;
using UnityEngine;

public class SlotView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _slotImage;
    [SerializeField] private TMP_Text _amountText;

    public void InitView(Sprite mainSprite)
    {
        _slotImage.sprite = mainSprite;
    }

    public void RefreshAmount(int value)
    {        
        _amountText.text = value.ToString();
    }

    public void HideSlotView()
    {
        _slotImage.color = new Color(0, 0, 0, 0);
        _amountText.color = new Color(0, 0, 0, 0);
    }
}

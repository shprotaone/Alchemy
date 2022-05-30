using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bottle : MonoBehaviour
{
    [SerializeField] private Image _fullBottle;

    private bool _isFull;
    private CanvasGroup _canvasGroup;

    private PotionData _potionInBottle;

    public bool IsFull => _isFull;
    public PotionData PotionInBottle => _potionInBottle;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.blocksRaycasts = true;
    }

    public void FillBottle(Color color)
    {
        _fullBottle.enabled = true;
        _fullBottle.color = color;
        _isFull = true;

    }

    public void FillPotionInBottle(PotionData potion)
    {
        _potionInBottle = potion;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step13 : Step
{
    [SerializeField] private Inventory _inventory;
    public override void StepAction()
    {
        _dialogView.EnableView(true);
        _inventory.StartGameFilling(true);
        _dialogView.NextButton.gameObject.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateController : MonoBehaviour
{
    [SerializeField] private PotionTaskList _potionTaskList;
    [SerializeField] private VisitorController _visitorController;
    [SerializeField] private Button _cameraMovementButton;

    public void Init()
    {
        PotionTaskList.OnPotionTaskListChanged += CheckState;
        _cameraMovementButton.interactable = false;
    }

    public void CheckState()
    {
        if (_potionTaskList.CyclopediaComplete)
        {
            _cameraMovementButton.interactable = true;
            _visitorController.Activate();
        }
        else
        {
            _cameraMovementButton.interactable = false;
            _visitorController.Deactivate();
        }
    }

    private void OnDisable()
    {
        PotionTaskList.OnPotionTaskListChanged -= CheckState;
    }
}

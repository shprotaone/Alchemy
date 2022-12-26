using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateController : MonoBehaviour
{
    [SerializeField] private PotionCyclopedia _potionCyclopedia;
    [SerializeField] private VisitorController _visitorController;
    [SerializeField] private Button _cameraMovementButton;

    private LevelStateType levelState;

    public void CheckState()
    {
        if (_potionCyclopedia.CyclopediaComplete)
        {
            levelState = LevelStateType.SELL;
        }
        else
        {
            
        }
    }
}

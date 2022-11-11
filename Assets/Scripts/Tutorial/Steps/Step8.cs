using UnityEngine;
using UnityEngine.UI;

public class Step8 : Step
{
    private int _count;

    [SerializeField] private Button _boilButton;
    [SerializeField] private ClaudronSystem _claudronSystem;
    [SerializeField] private MixingSystemv2 _mixingSystem;

    public void OnEnable()
    {
        _mixingSystem.OnIngredientAdded += IngredientCount;
    }

    private void IngredientCount()
    {
        _count ++;
        if(_count >= 2)
        {
            _tutorialManager.NextStep();
        }
    }
    public override void StepAction()
    {
        _claudronSystem.SetTutorial(true);
    }

    private void OnDisable()
    {       
        _mixingSystem.OnIngredientAdded -= IngredientCount;
    }
}

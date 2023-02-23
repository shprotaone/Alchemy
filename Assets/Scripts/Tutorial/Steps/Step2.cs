using UnityEngine;

public class Step2 : MonoBehaviour,IStepTutorial {
    [SerializeField] private GameObject _tutorialObject;
    private Tutorial _tutorial;
    public void ShowText(string text) {
        
    }

    public void Activate(Tutorial tutorial) {
        MixingSystem.OnIngredientAdded += Next;
        this._tutorial = tutorial;
        _tutorialObject.SetActive(true);
    }

    public void Deactivate() {
        MixingSystem.OnIngredientAdded -= Next;
        _tutorialObject.SetActive(false);
    }

    public void Next() {
        _tutorial.CallNextStep();
    }
}

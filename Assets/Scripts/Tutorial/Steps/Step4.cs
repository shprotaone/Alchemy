using UnityEngine;

public class Step4 : MonoBehaviour,IStepTutorial {

    [SerializeField] private GameObject _tutorialObject;
    private Tutorial _tutorial;
    public void ShowText(string text) {
        
    }

    public void Activate(Tutorial tutorial) {
        
        this._tutorial = tutorial;
        _tutorialObject.SetActive(true);
    }

    public void Deactivate() {
        
        _tutorialObject.SetActive(false);
    }

    public void Next() {
        _tutorial?.CallNextStep();
    }
}

using UnityEngine;

public class Step3 : MonoBehaviour,IStepTutorial {
    [SerializeField] private ClickController _clickController;
    [SerializeField] private GameObject _tutorialObject;
    private Tutorial _tutorial;
    public void ShowText(string text) {
        
    }

    public void Activate(Tutorial tutorial) {

        _clickController.OnGoodPotion += Next;
        this._tutorial = tutorial;
        _tutorialObject.SetActive(true);

    }

    public void Deactivate() {

        _clickController.OnGoodPotion -= Next;
        _tutorialObject.SetActive(false);
    }

    public void Next() {
        _tutorial.CallNextStep();
    }
}

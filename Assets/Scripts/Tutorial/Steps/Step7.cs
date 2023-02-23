using System.Collections;
using UnityEngine;

public class Step7 : MonoBehaviour,IStepTutorial {

    [SerializeField] private GameObject _tutorialObject;
    private Tutorial _tutorial;
    public void ShowText(string text) {
        
    }

    public void Activate(Tutorial tutorial) {
        StartCoroutine(MessageDelay());
        this._tutorial = tutorial;
        _tutorialObject.SetActive(true);
    }

    public void Deactivate() {
        
        _tutorialObject.SetActive(false);
    }

    public void Next() {
        _tutorial.CallNextStep();
    }

    private IEnumerator MessageDelay() {

        yield return new WaitForSeconds(3f);
        Deactivate();
    }
}

public interface IStepTutorial {
    void ShowText(string text);
    void Activate(Tutorial tutorial);
    void Deactivate();
    void Next();
}

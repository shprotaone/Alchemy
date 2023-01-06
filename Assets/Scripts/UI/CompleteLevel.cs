using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CompleteLevel : MonoBehaviour,IMenu
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private TMP_Text _coinResult;

    private Money _money;
    public void Init(Money money)
    {
        _exitButton?.onClick.AddListener(Exit);
        _restartButton?.onClick.AddListener(Restart);

        _money = money;
    }

    private void OnEnable()
    {
        _coinResult.text = _money.CurrentMoney.ToString();
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

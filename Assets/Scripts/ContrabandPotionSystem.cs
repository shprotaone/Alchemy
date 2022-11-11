using TMPro;
using UnityEngine;

public class ContrabandPotionSystem : MonoBehaviour
{
    [SerializeField] private PotionTaskSystem _potionTaskSystem;
    [SerializeField] private TMP_Text _counterCompleteText;

    private int _contrabandTimer;
    private int _completeCounter;
    private int _conrabandPotionChance;
    private Potion _contrabandPotion;
    private bool _isActive;

    public int ContrabandTimer => _contrabandTimer;
    public int CompleteCounter => _completeCounter;
    public int ContrabandPotionChance => _conrabandPotionChance;
    public Potion ContrabandPotion => _contrabandPotion;
    public bool IsActive => _isActive;

    public void InitContrabandPotion(int contrabandTimer, int contrabandPotionChance)
    {
        _contrabandPotion = new Potion(SetPotion());
        _contrabandPotion.SetContraband(true);
        _conrabandPotionChance = contrabandPotionChance;

        _contrabandTimer = contrabandTimer;
        _counterCompleteText.gameObject.SetActive(true);

        _isActive = true;
        print("Контрабанда " + _contrabandPotion.PotionName);
    }

    public void AddCounter()
    {
        _completeCounter++;
        RefreshCounterText();
    }

    private PotionData SetPotion()
    {
        int potionIndex = Random.Range(0, _potionTaskSystem.PotionSizer.Potions.Length - 1);
        PotionData potion = _potionTaskSystem.PotionSizer.Potions[potionIndex];

        return potion;
    }

    private void RefreshCounterText()
    {
        _counterCompleteText.text = " " + _completeCounter;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Gem : MonoBehaviour
{
    public static UnityEvent OnGemChanged = new UnityEvent();

    public static UnityEvent<int> OnAddGem = new UnityEvent<int>();
    public static UnityEvent<int> OnRemoveGem = new UnityEvent<int>();

    [SerializeField] private TMP_Text _gemView;
    private int _gem;

    public int GemCounter => _gem;
    private void Start()
    {
        OnGemChanged.AddListener(RefreshText);
        OnAddGem.AddListener(AddGem);
        OnRemoveGem.AddListener(RemoveGem);

        RefreshText();
    }

    public void AddGem(int value)
    {
        _gem += value;
        OnGemChanged?.Invoke();
    }

    public void RemoveGem(int value)
    {
        _gem -= value;
        OnGemChanged?.Invoke();
    }

    private void RefreshText()
    {
        _gemView.text = _gem.ToString();
    }

    
}

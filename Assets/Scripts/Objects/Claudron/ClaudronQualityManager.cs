using UnityEngine;

public class ClaudronQualityManager : MonoBehaviour
{
    [SerializeField] private Claudron[] _claudrons;
    [SerializeField] private ClaudronSystem _claudronSystem;
    [SerializeField] private int _currentClaudronValue;

    private ClaudronType _type;
    private Claudron _currentClaudron;

    private void Start()
    {
        CheckClaudronQuality();
        _claudronSystem.SetClaudron(_currentClaudron);
    }

    private void CheckClaudronQuality()
    {
        _currentClaudron = _claudrons[_currentClaudronValue];
    }

    public void UpgradeClaudron()
    {
        if (_currentClaudronValue < _claudrons.Length - 1)
        {
            _currentClaudronValue++;
        }
        
        _claudronSystem.SetClaudron(_currentClaudron);
    }
}

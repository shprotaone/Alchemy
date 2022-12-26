using UnityEngine;

public class LevelPresetLoader : MonoBehaviour
{
    public static LevelPresetLoader instance = null;

    [SerializeField] private LevelPreset _currentPreset;

    public LevelPreset LevelPreset => _currentPreset;
    public bool MapIsOpen { get; set; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);
    }

    public void SetLevelPreset(LevelPreset currentPreset)
    {
        _currentPreset = currentPreset;
    }

    public void ResetPreset()
    {
        _currentPreset = null;
    }
}

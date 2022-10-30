using UnityEngine;

/// <summary>
/// Вообще синглтоны не очень хорошая вещь.
/// Но в разумных пределах - найс
/// </summary>
public class Singleton<T> : MonoBehaviour where T:Component
{
    private static T _instance;
    public static T Instance
    {
        get 
        { 
            if(_instance == null)
            {
                Init();
            }
            return _instance; 
        }
		
        private set 
        { 
            _instance = value; 
        }
    }
	
    private static void Init()
    {
        _instance = FindObjectOfType<T>();
        if(_instance == null)
        {
            Debug.LogError($"{typeof(T).ToString()} не существует в текщем контексте!");
        }
    }
}
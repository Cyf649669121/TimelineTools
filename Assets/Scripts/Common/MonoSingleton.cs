using UnityEngine;

/// <summary>
/// 单例类；
/// </summary>
/// <typeparam name="T"></typeparam>
public class MonoSingleton<T> : MonoBehaviour where T : Component
{
    private static T _Instance;
    public static T Instance
    {
        get
        {
            if (_Instance == null)
            {
                GameObject go = new GameObject();
                _Instance = go.AddComponent<T>();
                go.name = _Instance.GetType().ToString();
            }
            return _Instance;
        }
    }

    private void Awake() { _Instance = this as T; }

}

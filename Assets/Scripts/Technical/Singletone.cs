using UnityEngine;

public class Singletone<T> : MonoBehaviour
    where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance {
        get
        {
            if (_instance is null)
            {
                var instances = FindObjectsOfType<T>();
                if (instances.Length > 0)
                {
                    _instance = instances[0];
                    throw new System.Exception($"More than one exist {typeof(T)}, Singletone");
                }
                else if (instances.Length == 0)
                {
                    var obj = new GameObject();
                    _instance = obj.AddComponent<T>();
                    throw new System.Exception($"No singletone exit, creating one {typeof(T)}, Singletone");
                }
            }
            return _instance;
        }
    }
}

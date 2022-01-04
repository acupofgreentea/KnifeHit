using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    public static T Instance;
    protected virtual void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        
        Instance = this as T;
    }
}

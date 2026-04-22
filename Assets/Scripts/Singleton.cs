using System;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

    private static readonly Lazy<T> lazy = new(CreateInstance);

    private static bool isQuitting = false;
    private static T instance;

    public static T Instance
    {
        get 
        {
            if (isQuitting) return null;
            instance = lazy.Value;
            return instance; 
        }
    }

    private static T CreateInstance()
    {
        if (!Application.isPlaying || isQuitting) return null;

        T[] instances = FindObjectsByType<T>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        if (instances.Length > 0)
        {
            if (instances.Length > 1)
                Debug.LogWarning($"<color=yellow>Warning! More than one instance of {typeof(T)} found!</color>");

            return instances[0];
        }
        else
        {
            GameObject go = new();
            T instance = go.AddComponent<T>();

            go.name = typeof(T).ToString() + " (Singleton)";
            DontDestroyOnLoad(go);

            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (isQuitting) return;

        if (instance == null)
        {
            instance = (T)(object)this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnApplicationQuit()
    {
        isQuitting = true;
    }
}
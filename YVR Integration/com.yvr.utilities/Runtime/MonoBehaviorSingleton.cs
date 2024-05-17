using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoBehaviorSingleton<T> : MonoBehaviour where T : MonoBehaviorSingleton<T>
{
    private bool initialized = false;

    private static object locker = new object();
    private static T _instance;
    public static T instance
    {
        get
        {
            if (!_instance)
            {
                lock (locker) // Ensure Thread Safety
                {
                    if (!_instance)
                    {
                        _instance = FindObjectOfType<T>();
                        if (_instance != null && !_instance.initialized)
                            _instance.Init();
                    }
                }
            }

            return _instance;
        }
    }

    protected virtual void Init()
    {
        initialized = true;
    }

    protected virtual void Start()
    {
        if (!initialized)
            Init();
    }
}
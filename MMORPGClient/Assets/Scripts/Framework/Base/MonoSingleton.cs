using System;
using UnityEngine;

/// <summary>
/// Inherit from this base class to create a singleton.
/// e.g. public class MyClassName : Singleton<MyClassName> {}
/// </summary>
public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    internal static GameObject parent = null;
    private static T m_Instance;

    /// <summary>
    /// Access singleton instance through this propriety.
    /// </summary>
    public static T Instance
    {
        get
        {
            if (m_Instance == null)
            {
                // Search for existing instance.
                m_Instance = (T) FindObjectOfType(typeof(T));

                // Create new instance if one doesn't already exist.
                if (m_Instance == null)
                {
                    // Need to create a new GameObject to attach the singleton to.
                    GameObject singletonObject = new GameObject(typeof(T).ToString() + " (Singleton)");
                    m_Instance = singletonObject.AddComponent<T>();

                    
                    if (parent == null)
                    {
                        parent = GameObject.Find("Singletons");
                        if (parent == null)
                        {
                            parent = new GameObject("Singletons");
                            DontDestroyOnLoad(parent);
                        }
                    }

                    singletonObject.transform.parent = parent.transform;

                    // Make instance persistent.
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return m_Instance;
        }
    }

    /*
     * 没有任何实现的函数，用于保证MonoSingleton在使用前已创建
     */
    public void Startup()
    {
    }

    private void Awake()
    {
        if (gameObject != null && m_Instance == null)
        {
            m_Instance = gameObject.GetComponent<T>();
        }

        Init();
        DontDestroyOnLoad(gameObject);
    }

    protected virtual void Init()
    {
    }


    public void DestroySelf()
    {
        Dispose();
        m_Instance = null;
        Destroy(gameObject);
    }

    public virtual void Dispose()
    {
    }
}
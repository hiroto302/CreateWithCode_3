using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T instance;
    public static T Instance
    {
        get {return instance;}
    }

    public static bool IsInitialized
    {
        get { return instance != null; }
    }

    protected virtual void Awake()
    {
        CheckInstance();
        // if (instance != null)
        // {
        //     Debug.LogError("[Singleton] Trying to instantiate a second instance of a singleton class");
        // }
        // else
        // {
        //     instance = (T) this;
        // }
    }

    protected virtual void OnDestroy()
    {
        if ( instance == this)
        {
            instance = null;
        }
    }

    bool CheckInstance()
    {
        if (instance == null)
        {
            instance = (T)this;
            return true;
        }
        else if (Instance == this)
        {
            return true;
        }
        else
        {
            Destroy(this.gameObject);
            return false;
        }
    }
}

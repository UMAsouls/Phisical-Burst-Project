using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            {
                if (_instance == null)
                {
                    // シーン内でT型のオブジェクトを探す
                    _instance = FindObjectOfType<T>();

                    if (_instance == null)
                    {
                        Debug.LogError($"Singleton of type {typeof(T)} is required in the scene but not found.");
                        throw new System.Exception($"Singleton of type {typeof(T)} is required in the scene but not found.");
                    }
                }
                return _instance;
            }
        }
    }

    //仮想関数にすることでクラス毎のシングルトンになる
    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

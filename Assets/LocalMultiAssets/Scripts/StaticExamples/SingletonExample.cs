using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonExample : MonoBehaviour
{
    public static SingletonExample instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
}

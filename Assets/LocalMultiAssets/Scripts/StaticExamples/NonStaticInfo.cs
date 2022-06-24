using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonStaticInfo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(StaticInfo.staticName);
    }

  
}

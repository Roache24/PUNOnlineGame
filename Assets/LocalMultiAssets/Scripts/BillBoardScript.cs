using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoardScript : MonoBehaviour
{
    GameObject camObject;
    public Transform cam;

    private void Awake()
    {
        camObject = GameObject.Find("Main Camera");
        cam = camObject.transform;
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}

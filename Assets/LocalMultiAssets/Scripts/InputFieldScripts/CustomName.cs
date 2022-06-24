using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CustomName : MonoBehaviour
{
    public GameObject P1Input;
    public GameObject P2Input;
    public void GetName()
    {
        GameBoss.instance.P1name = P1Input.GetComponentInChildren<TMP_InputField>().text;
        GameBoss.instance.P2name = P2Input.GetComponentInChildren<TMP_InputField>().text;
    }
}

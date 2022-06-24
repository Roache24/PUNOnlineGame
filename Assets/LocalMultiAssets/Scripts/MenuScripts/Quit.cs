using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
  public void QuitTheGame()
    {
        Debug.Log("GAME HAS CLOSED");
        Application.Quit();
    }
}

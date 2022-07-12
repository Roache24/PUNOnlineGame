using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    public AudioSource click;
  public void QuitTheGame()
    {
        Debug.Log("GAME HAS CLOSED");
        Application.Quit();
    }
    public void PlayClick()
    {
        click.Play();
    }
}

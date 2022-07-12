using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToLobby : MonoBehaviour
{
    public AudioSource click;
  public void GoLobby()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayClick()
    {
        click.Play();
        Invoke("GoLobby",1f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToLobby : MonoBehaviour
{
  public void GoLobby()
    {
        SceneManager.LoadScene(1);
    }
}

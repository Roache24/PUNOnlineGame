using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class PlayerCounter : MonoBehaviourPunCallbacks
{
    public static PlayerCounter Instance;
    public float pCount;

    public void Start()
    {
        #region Singleton pattern
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        #endregion

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            pCount += 1f;
        }

    }

   
    public void UpdatePlayerCount()
    {
        this.photonView.RPC("MinusPlayerCount", RpcTarget.All);
    }

    [PunRPC]
    public void MinusPlayerCount()
    {
        pCount -= 1f;
    }

  

}

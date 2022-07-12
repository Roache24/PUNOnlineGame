using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;


namespace Com.SteveGames.PUNonline
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        #region PublicFields
        public static GameManager Instance;
        [Tooltip("Prefab used to represent player")]
        public GameObject playerPrefab;
        #endregion
        #region Photon Callbacks
        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
        }
        #endregion
        #region Public Methods
        private void Start()
        {
            Instance = this;

            if (playerPrefab == null)
            {
                Debug.LogError("< Color = Red >< a > Missing </ a ></ Color > playerPrefab Reference.Please set it up in GameObject 'Game Manager'", this);
            }
            if (PlayerManager.LocalPlayerInstance == null)
            {
                Debug.Log("We are Instantiating LocalPlayer from {0}");
                PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
            }
            else
            {
                Debug.Log("Ignoring scene load for {0}");
            }

        }

      
        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        #endregion
        #region Private Methods
        public void LoadArena()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError("Photon Network: Trying to load the level but client isn't the Master Client");
            }
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("GameLevel");
            }
        }
        #endregion
    }
}

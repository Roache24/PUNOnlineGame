using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


namespace Com.SteveGames.PUNonline
{
    public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
    {
        #region PublicFields
        [Tooltip("The current Health of the player")]
        public float Health = 1f;
        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;
        #endregion
        #region PrivateFields
        [Tooltip("The Beams GameObject to Control")]
        [SerializeField]
        private GameObject beams;
        bool IsFiring;
        #endregion
        #region MonoBehaviourCallBacks
        void Awake()
        {
            if(beams == null) 
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> Beams Reference.", this);
            }
            else 
            {
                beams.SetActive(false);
            }

            if (photonView.IsMine)
            {
                PlayerManager.LocalPlayerInstance = this.gameObject;
            }

            DontDestroyOnLoad(this.gameObject);
        }
        void Start()
        {
            CameraWork _cameraWork = this.gameObject.GetComponent<CameraWork>();

            if (_cameraWork != null)
            {
                if (photonView.IsMine)
                {
                    _cameraWork.OnStartFollowing();
                }
            }
            else
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> CameraWork Component on playerPrefab.", this);
            }
        }

        void Update()
        {
            if (photonView.IsMine)
            {
                ProcessInputs();
                if(Health <= 0f) 
                {
                    GameManager.Instance.LeaveRoom();
                
                }
            }
            if(beams != null && IsFiring != beams.activeInHierarchy)
            {
                beams.SetActive(IsFiring);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!photonView.IsMine)
            {
                return;
            }
            if (!other.name.Contains("Beam"))
            {
                return;
            }
            Health -= 0.1f;
        }

        private void OnTriggerStay(Collider other)
        {
            if (!photonView.IsMine) 
            {
                return; 
            }
            if(!other.name.Contains("Beam"))
            {
                return;
            }
            Health -= 0.1f * Time.deltaTime;
        }

        #endregion
        #region Custom
        void ProcessInputs()
        {
            if(Input.GetButtonDown("Fire1"))
            {
                if (!IsFiring)
                {
                    IsFiring = true;
                }
            }

            if(Input.GetButtonUp("Fire1"))
            {
                if(IsFiring)
                {
                    IsFiring = false;
                }    
            }
        }
        #endregion
        #region IPunObservableImplementation
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) 
        {
            if (stream.IsWriting)
            {
                stream.SendNext(IsFiring);
                stream.SendNext(Health);
            }
            else 
            {
                this.IsFiring = (bool)stream.ReceiveNext();
                this.Health = (float)stream.ReceiveNext();
            }
        }
        #endregion
    }
}

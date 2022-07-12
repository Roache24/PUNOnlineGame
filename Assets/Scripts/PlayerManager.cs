using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;




namespace Com.SteveGames.PUNonline
{
    public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
    {
        #region PublicFields
        [Tooltip("The current Health of the player + DeathCount")]
        public float Health = 1f;
        public float maxHealth = 1f;
        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;
        [Tooltip("The Player's UI GameObject Prefab")]
        [SerializeField]
        public GameObject playerUIPrefab;
        public GameObject winnerUI;
        public float deathCount = 1f;
        public float maxDeathCount = 3f;
        public bool gameStarted = false;
        public AudioSource laser;
        #endregion
        #region PrivateFields
        [Tooltip("The Beams GameObject to Control")]
        [SerializeField]
        private GameObject beams;
        bool IsFiring;
        PlayerUI playerUI;
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
            deathCount = maxDeathCount;
            playerUI = playerUIPrefab.GetComponent<PlayerUI>();
            playerUI.deathText.text = deathCount.ToString();

            if (playerUIPrefab!)
            {
                GameObject _uiGo = Instantiate(playerUIPrefab);
                _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
            }
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void Update()
        {
            
            if (photonView.IsMine)
            {
                ProcessInputs();
                
                if (Health <= 0)
                {
                    deathCount -= 1f;
                    if (deathCount != -1)
                    {
                        gameObject.SetActive(false);
                        Invoke("Respawn", 3f);

                    }

                    if(deathCount == -1 && gameStarted == true)
                    {
                        PlayerCounter.Instance.UpdatePlayerCount();
                        GameManager.Instance.LeaveRoom();
                    }
                    
                }
                
            }
            if (beams != null && IsFiring != beams.activeInHierarchy)
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
            Health -= 0.5f;
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
            Health -= 0.5f * Time.deltaTime;
        }
        void OnLevelWasLoaded(int level)
        {
            
            this.CalledOnLevelWasLoaded(level);

            deathCount = maxDeathCount;

            gameStarted = true;

            
            if (playerUIPrefab!)
            {
                GameObject _uiGo = Instantiate(playerUIPrefab);
                _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
            }


        }

        void CalledOnLevelWasLoaded(int level)
        {
            // check if we are outside the Arena and if it's the case, spawn around the center of the arena in a safe zone
            if (!Physics.Raycast(transform.position, -Vector3.up, 5f))
            {
                transform.position = new Vector3(0f, 5f, 0f);
            }
        }

        public override void OnDisable()
        {
            // Always call the base to remove callbacks
            base.OnDisable();
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
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
                    laser.Play();
                }
            }

            if(Input.GetButtonUp("Fire1"))
            {
                if(IsFiring)
                {
                    IsFiring = false;
                    laser.Stop();
                }    
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PlayerCounter.Instance.UpdatePlayerCount();
                GameManager.Instance.LeaveRoom();
            }

        }

        void Respawn()
        {
                transform.position = new Vector3(0f,05,0f);
                Health = maxHealth;
                gameObject.SetActive(true);
        }


        #endregion
        #region IPunObservableImplementation
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) 
        {
            if (stream.IsWriting)
            {
                stream.SendNext(IsFiring);
                stream.SendNext(Health);
                stream.SendNext(deathCount);
            }
            else 
            {
                this.IsFiring = (bool)stream.ReceiveNext();
                this.Health = (float)stream.ReceiveNext();
                this.deathCount = (float)stream.ReceiveNext();
            }
        }
        #endregion
        #region Private Methods
        void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode loadingMode)
        {
            this.CalledOnLevelWasLoaded(scene.buildIndex);
        }


        #endregion
    }
}


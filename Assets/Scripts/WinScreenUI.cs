using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.SteveGames.PUNonline
{
    public class WinScreenUI : MonoBehaviour
    {

        CanvasGroup _canvasGroup;
        public GameObject winnerUI;
        public float playerCount;



        public void Awake()
        {
            this.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
            _canvasGroup = this.GetComponent<CanvasGroup>();

            _canvasGroup.alpha = 0f;

        }

        private void Update()
        {
            playerCount = PlayerCounter.Instance.pCount;

            if (playerCount == 1)
            {
                Debug.Log("playercount is 1");
                _canvasGroup.alpha = 1f;
         
            }
        }


        public void ReturnToMain()
        {
            GameManager.Instance.LeaveRoom();
        }


    }
}

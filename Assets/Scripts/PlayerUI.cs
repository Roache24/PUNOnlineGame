using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.SteveGames.PUNonline
{
    public class PlayerUI : MonoBehaviour
    {
        #region PublicFields
        [Tooltip("Text to display player name")]
        [SerializeField]
        private Text playerNameText;

        [Tooltip("Slider to display player health")]
        [SerializeField]
        private Slider HealthSlider;

        [Tooltip("Pixel Offset from the player target")]
        [SerializeField]
        private Vector3 screenOffset = new Vector3(0f, 30f, 0f);

        #endregion
        #region  PrivateFields
        private PlayerManager target;

        float characterControllerHeight = 0f;
        Transform targetTransform;
        Renderer targetRenderer;
        CanvasGroup _canvasGroup;
        Vector3 targetPosition;
        #endregion
        #region PublicMethods
        public void SetTarget(PlayerManager _target)
        {
            if (_target == null)
            {
                Debug.LogError("<Color = Red >< a > Missing </ a ></ Color > PlayMakerManager target for PlayerUI.SetTarget.", this);
                return;
            }

            target = _target;

            if (playerNameText != null)
            {
                playerNameText.text = target.photonView.Owner.NickName;
            }

            targetTransform = this.target.GetComponent<Transform>();
            targetRenderer = this.target.GetComponent<Renderer>();
            CharacterController characterController = _target.GetComponent<CharacterController>();
            if (characterController != null)
            {
                characterControllerHeight = characterController.height;
            }
        }
        #endregion
        #region MonoBehaviourCallbacks
        private void Awake()
        {
            this.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);

            _canvasGroup = this.GetComponent<CanvasGroup>();
        }
        private void Update()
        {
            if(HealthSlider != null)
            {
                HealthSlider.value = target.Health;
            }

            if(target == null)
            {
                Destroy(this.gameObject);
                return;
            }
        }

        private void LateUpdate()
        {
            if(targetRenderer != null)
            {
                this._canvasGroup.alpha = targetRenderer.isVisible ? 1f : 0f;
            }

            if (targetTransform != null)
            {
                targetPosition = targetTransform.position;
                targetPosition.y += characterControllerHeight;
                this.transform.position = Camera.main.WorldToScreenPoint(targetPosition) + screenOffset;
            }
        }
        #endregion
    }
}
namespace TinyPlay {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Player Controller
    /// </summary>
    public class PlayerController : BaseController, IEventReciever<OnGameStateChanged>, IEventReciever<OnLivesUpdated> {
        [Header("Player Params")] 
        public float Speed = 1f;

        [Header("Player View")] 
        public PlayerView CurrentPlayerView;
        
        // Private Params
        private Camera m_MainCamera;
        private bool isEnabled = false;

        /// <summary>
        /// On Before Scene is Initialized
        /// </summary>
        private void Awake() {
            m_MainCamera = Camera.main;
        }
        
        /// <summary>
        /// On Scene Started
        /// </summary>
        private void Start() {
            EventBus.Subscribe(this);
        }

        /// <summary>
        /// On Scene Destroyed
        /// </summary>
        private void OnDestroy() {
            EventBus.Unsubscribe(this);
        }

        /// <summary>
        /// On Update
        /// </summary>
        private void Update() {
            if (isEnabled && Input.GetMouseButtonDown(0)) {
                Vector3 clickPosition = m_MainCamera.ScreenToWorldPoint(Input.mousePosition);
                clickPosition.z = 0;
                EventBusGeneric<OnInputClicked>.Fire(new OnInputClicked() {
                    clickPosition = clickPosition,
                    speed = Speed
                });
            }
        }

        #region Event Listeners
        /// <summary>
        /// On Game State Changed
        /// </summary>
        /// <param name="e"></param>
        public void OnEventFire(OnGameStateChanged e) {
            isEnabled = e.NewGameState == CurrentGameState.InGame;
            if (isEnabled) {
                CurrentPlayerView.EnableView();
            } else {
                CurrentPlayerView.DisableView();
            }
        }

        /// <summary>
        /// On Lives Updated
        /// </summary>
        /// <param name="e"></param>
        public void OnEventFire(OnLivesUpdated e) {
            if (e.NewLives <= 0) {
                CurrentPlayerView.DisableView();
            } else {
                CurrentPlayerView.TakeDamage();
            }
        }
        #endregion
    }
}
namespace TinyPlay {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Enemy View
    /// </summary>
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyView : BaseGameView, IEventReciever<OnGameStateChanged>, IEventReciever<OnEnemyWaypointGenerated> {
        // Private Params
        private Vector3 newPosition;
        private float movementSpeed;
        private bool isOnWaypoint = false;
        private int currentViewID = 0;
        
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
        /// Set View UID
        /// </summary>
        /// <param name="uid"></param>
        public void SetViewID(int uid) {
            currentViewID = uid;
        }

        /// <summary>
        /// On Update
        /// </summary>
        private void Update() {
            if (Vector3.Distance(newPosition, transform.position) <= 0.1f && !isOnWaypoint) {
                isOnWaypoint = true;
                EventBusGeneric<OnEnemyWaypointStay>.Fire(new OnEnemyWaypointStay());
            } else {
                isOnWaypoint = false;
                Vector3 currentPosition = transform.position;
                transform.position = Vector3.Lerp(currentPosition, newPosition, movementSpeed * Time.deltaTime);
                
                // Rotate View
                Quaternion currentRotation = transform.rotation;
                float AngleRad = Mathf.Atan2(newPosition.y - currentPosition.y, newPosition.x - currentPosition.x);
                float AngleDeg = (180 / Mathf.PI) * AngleRad;
                Quaternion newRotation = Quaternion.Euler(0, 0, AngleDeg);
                transform.rotation = Quaternion.Slerp(currentRotation, newRotation, movementSpeed * 4f * Time.deltaTime);
            }
        }
        
        /// <summary>
        /// On Game State Changed
        /// </summary>
        /// <param name="e"></param>
        public void OnEventFire(OnGameStateChanged e) {
            if (e.NewGameState == CurrentGameState.InGame) {
                EnableView();
            } else {
                DisableView();
            }
        }

        /// <summary>
        /// On Enemy Waypoint Generated
        /// </summary>
        /// <param name="e"></param>
        public void OnEventFire(OnEnemyWaypointGenerated e) {
            if (e.forObject == currentViewID) {
                newPosition = e.newPosition;
                movementSpeed = e.speed;
            }
        }
        
        /// <summary>
        /// On Trigger Enter
        /// </summary>
        /// <param name="triggered"></param>
        private void OnCollisionEnter2D(Collision2D triggered) {
            GameObject triggeredObject = triggered.gameObject;
            if (triggeredObject.GetComponent<IPlayerView>() != null) {
                DisableView();
                EventBusGeneric<OnLivesAdded>.Fire(new OnLivesAdded() {
                    AddedLives = -1
                });
            }
        }
    }
}
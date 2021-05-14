namespace TinyPlay {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Player View
    /// </summary>
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerView : BaseGameView, ICharacterView, IPlayerView, IEventReciever<OnInputClicked> {
        // Private Params
        private Vector3 newPosition;
        private float movementSpeed;
        
        /// <summary>
        /// On Scene Started
        /// </summary>
        private void Start() {
            EventBus.Subscribe(this);
            newPosition = transform.position;
            movementSpeed = 0f;
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
            if (newPosition != transform.position) {
                // Move View
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
        /// Take Damage
        /// </summary>
        public void TakeDamage() {
            
        }

        /// <summary>
        /// On Input Clicked
        /// </summary>
        /// <param name="e"></param>
        public void OnEventFire(OnInputClicked e) {
            newPosition = e.clickPosition;
            movementSpeed = e.speed;
        }
    }
}
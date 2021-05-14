namespace TinyPlay {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Enemy Controller
    /// </summary>
    public class EnemyController : BaseController, IEventReciever<OnEnemyWaypointStay>, IEventReciever<OnGameStateChanged> {
        [Header("Enemy Params")]
        public float Speed = 1f;

        [Header("Enemy Limits")] 
        public float minXPosition = 0f;
        public float maxXPosition = 10f;
        public float minYPosition = 0f;
        public float maxYPosition = 10f;

        [Header("Enemy View")] 
        public EnemyView CurrentView;
        
        // Private Params
        private int ObjectUID = 0;

        /// <summary>
        /// On Scene Started
        /// </summary>
        private void Start() {
            EventBus.Subscribe(this);
            ObjectUID = UIDGenerator.GetUID();
            CurrentView.SetViewID(ObjectUID);
        }

        /// <summary>
        /// On Destroy
        /// </summary>
        private void OnDestroy() {
            EventBus.Unsubscribe(this);
        }

        /// <summary>
        /// Generate new Position
        /// </summary>
        /// <returns></returns>
        public void GenerateNewPosition() {
            float xPos = Random.Range(minXPosition, maxXPosition);
            float yPos = Random.Range(minYPosition, maxYPosition);
            Vector3 newWaypoint = new Vector3(xPos, yPos);
            
            // Fire Event
            EventBusGeneric<OnEnemyWaypointGenerated>.Fire(new OnEnemyWaypointGenerated() {
                newPosition = newWaypoint,
                speed = Speed,
                forObject = ObjectUID
            });
        }

        /// <summary>
        /// On Enemy Waypoint Stay
        /// </summary>
        /// <param name="e"></param>
        public void OnEventFire(OnEnemyWaypointStay e) {
            GenerateNewPosition();
        }

        /// <summary>
        /// On Game State Changed
        /// </summary>
        /// <param name="e"></param>
        public void OnEventFire(OnGameStateChanged e) {
            if (e.NewGameState == CurrentGameState.Menu) {
                GenerateNewPosition();
            }
        }
    }
}
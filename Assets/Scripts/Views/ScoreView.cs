namespace TinyPlay {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Score View
    /// </summary>
    public class ScoreView : BaseGameView, IEventReciever<OnGameStateChanged> {
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
    }
}
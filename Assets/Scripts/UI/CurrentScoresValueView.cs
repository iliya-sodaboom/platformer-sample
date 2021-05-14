namespace TinyPlay {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Current Scores Value View
    /// </summary>
    [RequireComponent(typeof(Text))]
    public class CurrentScoresValueView : BaseValueView, IEventReciever<OnScoresUpdated> {
        /// <summary>
        /// On Start
        /// </summary>
        private void Start() {
            EventBus.Subscribe(this);
        }

        /// <summary>
        /// On Destroy
        /// </summary>
        private void OnDestroy() {
            EventBus.Unsubscribe(this);
        }

        /// <summary>
        /// On High Scores Updated
        /// </summary>
        /// <param name="e"></param>
        public void OnEventFire(OnScoresUpdated e) {
            m_Text.text = e.CurrentScores.ToString("N0") + " ОЧК.";
        }
    }
}
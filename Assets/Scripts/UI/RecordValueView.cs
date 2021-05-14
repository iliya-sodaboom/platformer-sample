namespace TinyPlay {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Record Value View
    /// </summary>
    [RequireComponent(typeof(Text))]
    public class RecordValueView : BaseValueView, IEventReciever<OnHighscoresUpdated> {
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
        public void OnEventFire(OnHighscoresUpdated e) {
            m_Text.text = e.NewHighScores.ToString("N0") + " ОЧК.";
        }
    }
}
namespace TinyPlay {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Current Lives Value View
    /// </summary>
    [RequireComponent(typeof(Text))]
    public class CurrentLivesValueView : BaseValueView, IEventReciever<OnLivesUpdated> {
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
        public void OnEventFire(OnLivesUpdated e) {
            m_Text.text = e.NewLives.ToString("N0") + " ЖИЗ.";
        }
    }
}
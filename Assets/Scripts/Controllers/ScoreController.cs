using System;

namespace TinyPlay {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Score Controller
    /// </summary>
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class ScoreController : BaseController {
        // Public Params
        [Header("Scores Parameter")] 
        public double addToScores = 1;
        
        [Header("Controller View")]
        public ScoreView ScoreElementView;

        /// <summary>
        /// On Trigger Enter
        /// </summary>
        /// <param name="triggered"></param>
        private void OnTriggerEnter2D(Collider2D triggered) {
            GameObject triggeredObject = triggered.gameObject;
            if (triggeredObject.GetComponent<IPlayerView>() != null) {
                ScoreElementView.DisableView();
                EventBusGeneric<OnScoresAdded>.Fire(new OnScoresAdded() {
                    AddedScores = addToScores
                });
            }
        }
    }
}
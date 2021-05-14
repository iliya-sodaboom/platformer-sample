namespace TinyPlay {
    using UnityEngine;
    
    /// <summary>
    /// On Game State Changed Event Structure
    /// </summary>
    public struct OnInputClicked : IEvent {
        public Vector3 clickPosition;
        public float speed;
    }
}
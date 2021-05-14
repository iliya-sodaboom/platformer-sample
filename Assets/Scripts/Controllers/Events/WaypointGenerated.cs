namespace TinyPlay {
    using UnityEngine;
    
    /// <summary>
    /// On Game State Changed Event Structure
    /// </summary>
    public struct OnEnemyWaypointGenerated : IEvent {
        public Vector3 newPosition;
        public float speed;
        public int forObject;
    }
}
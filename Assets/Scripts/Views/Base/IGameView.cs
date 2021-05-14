using UnityEngine;

namespace TinyPlay {
    /// <summary>
    /// General Game View Interface
    /// </summary>
    public interface IGameView
    {
        /// <summary>
        /// Disable View
        /// </summary>
        void DisableView();
        
        /// <summary>
        /// Enable View
        /// </summary>
        void EnableView();
    }
}
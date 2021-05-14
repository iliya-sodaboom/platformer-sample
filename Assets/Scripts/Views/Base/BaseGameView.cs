namespace TinyPlay {
    using UnityEngine;
    
    /// <summary>
    /// Base Game View
    /// </summary>
    public class BaseGameView : MonoBehaviour, IGameView {
        [Header("Render Elements")]
        public SpriteRenderer m_Renderer;
        public BoxCollider2D m_Collider;
        public Rigidbody2D m_Rigidbody;
        
        /// <summary>
        /// Enable View
        /// </summary>
        public void EnableView() {
            m_Renderer.enabled = true;
            if (m_Collider != null) m_Collider.enabled = true;
            if (m_Rigidbody != null) m_Rigidbody.simulated = true;
        }

        /// <summary>
        /// Disable View
        /// </summary>
        public void DisableView() {
            m_Renderer.enabled = false;
            if (m_Collider != null) m_Collider.enabled = false;
            if (m_Rigidbody != null) m_Rigidbody.simulated = false;
        }
    }
}
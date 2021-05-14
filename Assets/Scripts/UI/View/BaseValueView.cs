namespace TinyPlay {
    using UnityEngine;
    using UnityEngine.UI;
    
    /// <summary>
    /// Base Value View
    /// </summary>
    [RequireComponent(typeof(Text))]
    public class BaseValueView : MonoBehaviour, IValueView {
        public Text m_Text;
        
        /// <summary>
        /// On Awake
        /// </summary>
        private void Awake() {
            if(m_Text == null) m_Text = GetComponent<Text>();
        }
    }
}
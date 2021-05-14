namespace TinyPlay {
    using UnityEngine;
    using UnityEngine.UI;
    
    /// <summary>
    /// Base Button Service
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class BaseButtonService : MonoBehaviour, IBaseButtonService {
        // private Params
        private Button m_Button;

        /// <summary>
        /// On Before Scene is Initialized
        /// </summary>
        private void Awake() {
            m_Button = GetComponent<Button>();
        }

        /// <summary>
        /// On Scene Started
        /// </summary>
        private void Start() {
            m_Button.onClick.AddListener(OnButtonClicked);
        }

        /// <summary>
        /// On Object Destroyed
        /// </summary>
        private void OnDestroy() {
            m_Button.onClick.RemoveAllListeners();
        }

        /// <summary>
        /// On Button Clicked
        /// </summary>
        public virtual void OnButtonClicked() {
        }
    }
}
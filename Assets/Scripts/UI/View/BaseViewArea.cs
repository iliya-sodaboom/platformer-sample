namespace TinyPlay {
    using UnityEngine;
    
    /// <summary>
    /// Base View Area
    /// </summary>
    [RequireComponent(typeof(Canvas))]
    public class BaseViewArea : MonoBehaviour, IViewArea, IEventReciever<OnGameStateChanged> {
        // Public Params
        public CurrentGameState GameStateVisibility = CurrentGameState.Loading;
        
        // Private Params
        private Canvas m_Canvas;
        private Animator m_Animator;

        /// <summary>
        /// On Awake
        /// </summary>
        private void Awake() {
            m_Canvas = GetComponent<Canvas>();
            m_Animator = GetComponent<Animator>();
        }
        
        /// <summary>
        /// On View Started
        /// </summary>
        private void Start() {
            EventBus.Subscribe(this);
            OnViewInitialized();
        }

        /// <summary>
        /// On View Destroyed
        /// </summary>
        private void OnDestroy() {
            EventBus.Subscribe(this);
        }

        /// <summary>
        /// On View Initialized
        /// </summary>
        public virtual void OnViewInitialized() {
        }

        /// <summary>
        /// Show Area
        /// </summary>
        public void ShowArea() {
            if (m_Canvas != null) m_Canvas.enabled = true;
            if (m_Animator != null) m_Animator.enabled = true;
        }

        /// <summary>
        /// Hide Area
        /// </summary>
        public void HideArea() {
            if (m_Canvas != null) m_Canvas.enabled = false;
            if (m_Animator != null) m_Animator.enabled = false;
        }

        /// <summary>
        /// On Event Fire
        /// </summary>
        /// <param name="CurrentState"></param>
        public virtual void OnEventFire(OnGameStateChanged CurrentState) {
            if (GameStateVisibility == CurrentState.NewGameState) {
                ShowArea();
            } else {
                HideArea();
            }
        }
    }
}
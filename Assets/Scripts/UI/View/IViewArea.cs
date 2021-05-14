namespace TinyPlay {
    /// <summary>
    /// IView Area
    /// </summary>
    public interface IViewArea {
        /// <summary>
        /// Show View Area
        /// </summary>
        void ShowArea();
        
        /// <summary>
        /// Hide View Area
        /// </summary>
        void HideArea();

        /// <summary>
        /// Virtual Event for Start
        /// </summary>
        void OnViewInitialized();
    }
}
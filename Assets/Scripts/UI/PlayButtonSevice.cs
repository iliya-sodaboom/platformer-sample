namespace TinyPlay {
    /// <summary>
    /// Play Button Service
    /// </summary>
    public class PlayButtonSevice : BaseButtonService {
        /// <summary>
        /// On Button Clicked
        /// </summary>
        public override void OnButtonClicked() {
            // Change Game State
            EventBusGeneric<OnGameStateChanged>.Fire(new OnGameStateChanged() {
                NewGameState = CurrentGameState.InGame
            });
        }
    }
}
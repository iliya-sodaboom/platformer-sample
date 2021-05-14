namespace TinyPlay {
    /// <summary>
    /// Menu Button Service
    /// </summary>
    public class MenuButtonSevice : BaseButtonService {
        public override void OnButtonClicked() {
            EventBusGeneric<OnGameStateChanged>.Fire(new OnGameStateChanged() {
                NewGameState = CurrentGameState.Menu
            });
        }
    }
}
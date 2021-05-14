namespace TinyPlay {
    /// <summary>
    /// On Game State Changed Event Structure
    /// </summary>
    public struct OnGameStateChanged : IEvent {
        public CurrentGameState NewGameState;
    }
}
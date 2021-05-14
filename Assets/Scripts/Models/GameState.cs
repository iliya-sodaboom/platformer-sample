namespace TinyPlay {
    /// <summary>
    /// Game State Model
    /// </summary>
    [System.Serializable]
    public class GameState {
        public CurrentGameState CurrentState;
        public double CurrentScores = 0;
        public double MaxScores = 0;
        public int CurrentLives = 3;
    }

    public enum CurrentGameState {
        Loading = 0,
        Menu = 1,
        InGame = 2,
        EndGame = 3
    }
}
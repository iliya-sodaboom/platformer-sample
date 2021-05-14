namespace TinyPlay {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Global Game Manager
    /// Developed by Ilya Rastorguev
    ///
    /// Use this manager to register game and change states
    /// </summary>
    public class GameManager : MonoBehaviour, IEventReciever<OnGameStateChanged>, IEventReciever<OnLivesAdded>, IEventReciever<OnScoresAdded> {
        // Private Params
        private GameState GameStateData;

        /// <summary>
        /// On Awake
        /// </summary>
        private void Awake() {
            LoadState();
        }

        /// <summary>
        /// On Start Game Object
        /// </summary>
        private void Start() {
            // Setup Events
            EventBus.Subscribe(this);
            
            // Setup Scene
            ChangeGameState(CurrentGameState.Loading);
            StartCoroutine(ResourceLoadingSimulation());
        }

        /// <summary>
        /// It's just for a simple test method for resouces loading
        /// simulation. Don't use it on production :D
        /// </summary>
        /// <returns></returns>
        private IEnumerator ResourceLoadingSimulation() {
            yield return new WaitForSeconds(2f);
            ChangeGameState(CurrentGameState.Menu);
        }
        
        /// <summary>
        /// Change Game State
        /// </summary>
        public void ChangeGameState(CurrentGameState GameState) {
            GameStateData.CurrentState = GameState;
            EventBusGeneric<OnGameStateChanged>.Fire(new OnGameStateChanged() {
                NewGameState = GameState
            });
        }

        /// <summary>
        /// On Object Destroyed
        /// </summary>
        private void OnDestroy() {
            EventBus.Unsubscribe(this);
        }

        #region Event Handlers
        /// <summary>
        /// Game State Change Event
        /// </summary>
        /// <param name="e"></param>
        public void OnEventFire(OnGameStateChanged e) {
            Debug.Log("Game State Changed to: " + e.NewGameState);
            if(e.NewGameState == CurrentGameState.EndGame) SaveState();
            if (e.NewGameState == CurrentGameState.InGame) {
                GameStateData.CurrentLives = 3;
                GameStateData.CurrentScores = 0;
            }
            
            // Fire Events
            EventBusGeneric<OnLivesUpdated>.Fire(new OnLivesUpdated() {
                NewLives = GameStateData.CurrentLives
            });
            EventBusGeneric<OnScoresUpdated>.Fire(new OnScoresUpdated() {
                CurrentScores = GameStateData.CurrentScores
            });
            EventBusGeneric<OnHighscoresUpdated>.Fire(new OnHighscoresUpdated() {
                NewHighScores = GameStateData.MaxScores
            });
        }

        /// <summary>
        /// On Lives Added
        /// </summary>
        /// <param name="e"></param>
        public void OnEventFire(OnLivesAdded e) {
            GameStateData.CurrentLives += e.AddedLives;
            EventBusGeneric<OnLivesUpdated>.Fire(new OnLivesUpdated() {
                NewLives = GameStateData.CurrentLives
            });

            if (GameStateData.CurrentLives == 0) ChangeGameState(CurrentGameState.EndGame);
        }

        /// <summary>
        /// On Events Added
        /// </summary>
        /// <param name="e"></param>
        public void OnEventFire(OnScoresAdded e) {
            GameStateData.CurrentScores += e.AddedScores;
            EventBusGeneric<OnScoresUpdated>.Fire(new OnScoresUpdated() {
                CurrentScores = GameStateData.CurrentScores
            });
            
            // Update HS
            if (GameStateData.CurrentScores > GameStateData.MaxScores) {
                GameStateData.MaxScores = GameStateData.CurrentScores;
                EventBusGeneric<OnHighscoresUpdated>.Fire(new OnHighscoresUpdated() {
                    NewHighScores = GameStateData.MaxScores
                });
            }
        }
        #endregion
        
        #region General State Methods
        /// <summary>
        /// Load Game State
        /// </summary>
        private void LoadState() {
            GameStateData = new GameState();
            string gstate = FileReader.ReadTextFile(Application.persistentDataPath + "/gameData.json");
            if (gstate != null) JsonUtility.FromJson<GameState>(gstate);
        }

        /// <summary>
        /// Save State
        /// </summary>
        private void SaveState() {
            GameState savedState = new GameState();
            savedState.MaxScores = GameStateData.MaxScores;
            string gstate = JsonUtility.ToJson(savedState);
            FileReader.SaveTextFile(Application.persistentDataPath + "/gameData.json", gstate);
        }
        #endregion
    }
}
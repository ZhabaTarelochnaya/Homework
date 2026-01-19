using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Gameplay.Data.Camera;
using DefaultNamespace.Gameplay.Data.Enemy;
using DefaultNamespace.Gameplay.Data.PickUp;

namespace DefaultNamespace.Gameplay.Data
{
    public class GameState
    {
        public int ID { get; set; }
        public int Score { get; set; }
        public GameStateName GameStateName { get; set; }
        public List<PickUpState> PickUps { get; } = new();
        public PlayerState PlayerState {get; set;}
        public CameraState CameraData {get; set;}
        public List<EnemyState> Enemies { get; } = new();

        public GameState(GameData gameData)
        {
            ID = gameData.ID;
            Score = gameData.Score;
            GameStateName = gameData.GameStateName;
            PickUps = gameData.PickUps.Select(p => new PickUpState(p)).ToList();
        }

        public int CreateID() => ID++;
        public GameData ToData()
        {
            var gameData = new GameData();
            gameData.ID = ID;
            gameData.Score = Score;
            gameData.GameStateName = GameStateName;
            gameData.PickUps = PickUps.Select(p => p.ToData()).ToList();
            gameData.PlayerData = PlayerState.ToData();
            gameData.CameraData = CameraData.ToData();
            gameData.Enemies = Enemies.Select(e => e.ToData()).ToList();
            return gameData;
        }
    }
}
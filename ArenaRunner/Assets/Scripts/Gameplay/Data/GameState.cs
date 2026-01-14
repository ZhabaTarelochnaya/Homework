using System.Collections.Generic;
using DefaultNamespace.Gameplay.Data.Camera;
using DefaultNamespace.Gameplay.Data.PickUp;

namespace DefaultNamespace.Gameplay.Data
{
    public class GameState
    {
        public int ID;
        public List<PickUpState> PickUps { get; }
        public PlayerData PlayerState {get; set;}
        public CameraData CameraData {get; set;}

        public GameState(GameData gameData)
        {
            ID = gameData.ID;
        }

        public int CreateID() => ID++;
    }
}
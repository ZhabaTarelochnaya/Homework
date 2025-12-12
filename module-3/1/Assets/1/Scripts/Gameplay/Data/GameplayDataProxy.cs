using _1.Scripts.Utils;

namespace _1.Gameplay.Data
{
    public class GameplayDataProxy
    {
        public ReactiveProperty<int> ShotsFired { get; }
        public ReactiveProperty<int> TargetsHit { get; }

        public GameplayDataProxy(GameplayData gameplayUI)
        {
            ShotsFired = new (gameplayUI.ShotsFired);
            ShotsFired.Subscribe(i => gameplayUI.ShotsFired = i);
            
            TargetsHit = new (gameplayUI.TargetsHit);
            TargetsHit.Subscribe(i => gameplayUI.TargetsHit = i);
        }
    }
}
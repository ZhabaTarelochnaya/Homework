namespace _MultiplayerFPS.Scripts.Utils.LoadingScreenService
{
    public class LoadingScreenService : ILoadingScreenService
    {
        readonly LoadingScreen loadingScreen;

        public LoadingScreenService(LoadingScreen loadingScreen)
        {
            this.loadingScreen = loadingScreen;
        }
        public void Show() => loadingScreen.Show();

        public void Hide() => loadingScreen.Hide();
    }
}
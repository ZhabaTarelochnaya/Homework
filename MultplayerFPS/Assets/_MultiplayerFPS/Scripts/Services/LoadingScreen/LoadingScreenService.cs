namespace _MultiplayerFPS.Scripts.Utils.LoadingScreen
{
    public class LoadingScreenService : ILoadingScreenService
    {
        readonly LoadingScreenView loadingScreen;

        public LoadingScreenService(LoadingScreenView loadingScreen)
        {
            this.loadingScreen = loadingScreen;
        }
        public void Show() => loadingScreen.Show();

        public void Hide() => loadingScreen.Hide();
    }
}
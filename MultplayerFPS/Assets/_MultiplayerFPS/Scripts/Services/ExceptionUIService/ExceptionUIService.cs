namespace _MultiplayerFPS.Scripts.Utils.ExceptionPopUp
{
    public class ExceptionUIService : IExceptionUIService
    {
        readonly ExceptionPopupView exceptionPopupView;

        public ExceptionUIService(ExceptionPopupView exceptionPopupView)
        {
            this.exceptionPopupView = exceptionPopupView;
        }
        public void ShowError(string message)
        {
            exceptionPopupView.gameObject.SetActive(true);
            exceptionPopupView.ShowError(message);
        }
    }
}
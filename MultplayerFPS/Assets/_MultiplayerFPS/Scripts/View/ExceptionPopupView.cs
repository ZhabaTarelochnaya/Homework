using TMPro;
using UnityEngine;

public class ExceptionPopupView : MonoBehaviour
{
    [SerializeField] TMP_Text _errorTypeText;
    [SerializeField] TMP_Text _errorText;

    public void ShowError(string errorType, string error)
    {
        _errorTypeText.text = errorType;
        _errorText.text = error;
    }
    
    public void OnCloseButtonPressed()
    {
        gameObject.SetActive(false);
    }
}

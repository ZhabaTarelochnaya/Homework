using TMPro;
using UnityEngine;

public class ExceptionPopupView : MonoBehaviour
{
    [SerializeField] TMP_Text _errorText;

    public void ShowError(string error)
    {
        _errorText.text = error;
    }
    
    public void OnCloseButtonPressed()
    {
        gameObject.SetActive(false);
    }
}

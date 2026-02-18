using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] GameObject _selectLobbyPanel;
    
    public void OnExitButtonClicked()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
    public void OnConnectButtonClicked()
    {
        _selectLobbyPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}

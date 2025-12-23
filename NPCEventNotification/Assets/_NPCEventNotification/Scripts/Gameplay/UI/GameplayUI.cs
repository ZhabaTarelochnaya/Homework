using NPCEventNotification.Scripts.Gameplay;
using NPCEventNotification.Scripts.Gameplay.UI;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] RectTransform _windows;
    [SerializeField] RectTransform _popUps;
    [SerializeField] GameObject _hudPrefab;
    
    public void Bind(EventManager eventManager)
    {
        if (!_windows) Debug.LogError($"{gameObject.name}: _windows is not set");
        if (!_popUps) Debug.LogError($"{gameObject.name}: _popUps is not set");
        if (!_hudPrefab) Debug.LogError($"{gameObject.name}: _hudPrefab is not set");
        
        var hudInstance = Instantiate(_hudPrefab, _windows);
        var hud = hudInstance.GetComponent<HUD>();
        hud.Bind(eventManager);
    }
}

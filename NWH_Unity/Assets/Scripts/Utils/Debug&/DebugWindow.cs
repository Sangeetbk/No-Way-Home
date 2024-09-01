using UnityEngine;
using TMPro;

public class DebugWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentGameState;
    [SerializeField] private TextMeshProUGUI _timeScale;
    [SerializeField] private TextMeshProUGUI _currentLevelName;

    private void Update()
    {
        UpdateDebugWindow();
    }
    public void UpdateDebugWindow()
    {
        _currentGameState.text = GameManager.Instance.CurrentGameState.ToString();
        _timeScale.text = Time.timeScale.ToString();
        _currentLevelName.text = GameManager.Instance._currentLevelName.ToString(); 
        

    }

}

using UnityEngine;
using TMPro;

public class DebugWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI text2;
    [SerializeField] private TextMeshProUGUI text3;

    private void Update()
    {
        UpdateDebugWindow();
    }
    public void UpdateDebugWindow()
    {
        text.text = GameManager.Instance.CurrentGameState.ToString();
        text2.text = Time.timeScale.ToString();
        text3.text = GameManager.Instance._currentLevelName.ToString(); 
        

    }

}

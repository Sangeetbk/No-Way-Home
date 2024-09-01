using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public MainMenu MainMenu;
    public GameObject PausedGameState;
    public OptionsMenu OptionsMenu;

    public GameObject CreditTab;
    public GameObject PlayerCharacter;

    public GameObject QuestList;

    public Button BackBtn;

    [SerializeField] private Camera _dummyCamera;

    public Events.EventFadeComplete OnMainMenuFadeComplete;
    public Events.EventToMainMenu OnToMainMenuClicked;

    // behaviour on paused menu
    public Events.EventToResumeState OnResumeStateChange;
    public Events.EventToMainMenu OnClickToMainMenu;

    private void Start()
    {
//        DontDestroyOnLoad(gameObject);

       // _mainMenu.OnFadeComplete.AddListener(HandleMainMenuFadeComplete);

        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
        GameManager.Instance.OnGameTypeChanged.AddListener(HandleGameTypeSystem);
        
        BackBtn.onClick.AddListener(BackClickBehaviour);

        OnResumeStateChange.AddListener(HandleResumeStateChange);
        OnClickToMainMenu.AddListener(HandleMainMenuClickStatus);


        if(PausedGameState == true)
        {
            GameManager.Instance.UpdateState(GameManager.GameState.PAUSED);
        }
    }

    private void HandleMainMenuClickStatus(bool isClicked)
    {
        if(isClicked)
        {
           MainMenu.gameObject.SetActive(true);
           PausedGameState.SetActive(false);
           GameManager.Instance.OnClickToMainMenu(true);
        }
    }

    private void HandleResumeStateChange(bool isResumed)
    {
        if(isResumed)
        {
            GameManager.Instance.UpdateState(GameManager.GameState.RUNNING);
        }
    }

    private void BackClickBehaviour()
    {
        OptionsMenu.gameObject.SetActive(false);
    }

    private void Update()
    {
         if(GameManager.Instance.CurrentGameState == GameManager.GameState.PREGAME) { return; }

        if (GameManager.Instance.CurrentGameState == GameManager.GameState.PAUSED)
        {
           // PauseMenu.gameObject.SetActive(true);
        }
    }

    private void HandleGameTypeSystem(GameManager.GameType gameType)
    {
        switch (gameType)
        {
            case GameManager.GameType.SURVIVAL:
                QuestList.SetActive(false);
                
                break;
            case GameManager.GameType.ADVENTURE:
                QuestList.SetActive(true);
                
                break;
        }
    }

    private void HandleMainMenuFadeComplete(bool fadeIn)
    {
        // pass it on
        OnMainMenuFadeComplete.Invoke(fadeIn);
    }

    private void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        switch (currentState)
        {
            case GameManager.GameState.PAUSED:
                PausedGameState.SetActive(true);
                break;

            default:
                PausedGameState.SetActive(false);
                break;
        }

        if(currentState == GameManager.GameState.RUNNING)
        {
            MainMenu.gameObject.SetActive(false);
            PlayerCharacter.SetActive(true);
            SetDummyCameraActive(false);
        } 
    }

    public void SetDummyCameraActive(bool active)
    {
        _dummyCamera.gameObject.SetActive(active);
    }

    public void PausedState(bool state)
    {
        Debug.Log(state);
        if (state == true)
        {
            GameManager.Instance.UpdateState(GameManager.GameState.PAUSED);
        }
        else if (state == false)
        {
            GameManager.Instance.UpdateState(GameManager.GameState.RUNNING);

        }
    }
}

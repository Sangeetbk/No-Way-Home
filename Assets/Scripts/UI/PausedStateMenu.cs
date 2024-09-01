using System;
using SGS.InputSystem;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PausedStateMenu : MonoBehaviour 
{

    public GameObject PausedTab;
    public GameObject InventoryTab;
    public GameObject JournalTab;

    #region PauseMenuBehaviour
    // paused Buttons
    [SerializeField] private Button _resume;
    //[SerializeField] private Button _reloadCheckpoint;
    [SerializeField] private Button _options;
    [SerializeField] private Button _credits;
    [SerializeField] private Button _quitToMainmenu;
    [SerializeField] private Button _quittoDesktop;
    [SerializeField] private Button _backButton;

    [SerializeField] private GameObject _optionsTab;
    [SerializeField] private GameObject _creditsTab;
    [SerializeField] private GameObject _MainPausedMenu;

    [SerializeField] private GameObject _mainMenuTab;

    [Header("Input Settings")]
    [SerializeField] private InputsHandler _playerInput;
    private FrameInput _frameInput;


    [Header("Controller Settings")]
    public Button FirstItemSlot;
    public Button FirstPauseBtn;

    // state event
    private Events.EventToPauseState eventToPauseState;

    private void Start()
    {
        _resume.onClick.AddListener(ResumeButtonClicked);
        _options.onClick.AddListener(OptionsButtonClicked);
        _credits.onClick.AddListener(CreditsTabClicked);
        _quitToMainmenu.onClick.AddListener(QuitToMainMenu);
        _quittoDesktop.onClick.AddListener(QuitToDesktop);

        //back behaviour
        _backButton.onClick.AddListener(OnBackButtonClicked);

    }

    private void ResumeButtonClicked()
    {
        _MainPausedMenu.SetActive(false);
        UIManager.Instance.OnResumeStateChange.Invoke(true);
       
    }
    private void OptionsButtonClicked()
    {
       _optionsTab.SetActive(true);
    }

    private void CreditsTabClicked()
    {
        _creditsTab.SetActive(true);
    }
    private void QuitToMainMenu()
    {
        _mainMenuTab.SetActive(true);
        UIManager.Instance.OnClickToMainMenu.Invoke(true);
    }

    private void QuitToDesktop()
    {
        Application.Quit();
    }


    private void OnBackButtonClicked()
    {
       _creditsTab.SetActive(false);
    }


    // paused Behavior

    #endregion


    private void Update()
    {
        //if(GameManager.Instance.CurrentGameState == GameManager.GameState.PREGAME) { return; }

      _frameInput = _playerInput.FrameInput;
        if(_frameInput.InventoryMenu)
        {
            HandleInventoryState();
            UpdatePausedStateCanvas();
        }

        if(_frameInput.PauseMenu)
        {
            HandlePausedState();
            UpdatePausedStateCanvas();
        }

        if(_frameInput.JournalSystem)
        {
             HandleJournalState();
             UpdatePausedStateCanvas();
        }
    }

    private void HandleJournalState()
    {
        JournalTab.SetActive(!JournalTab.activeSelf);
        if (JournalTab.activeSelf)
        {
            InventoryTab.SetActive(false);
            PausedTab.SetActive(false);
        }
    }

    private void HandleInventoryState()
    {
        InventoryTab.SetActive(!InventoryTab.activeSelf);
        if (InventoryTab.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(FirstItemSlot.gameObject);
            FirstItemSlot.GetComponent<Outline>().enabled = true;
            JournalTab.SetActive(false);
            PausedTab.SetActive(false);
        }
    }

    private void HandlePausedState()
    {
        PausedTab.SetActive(!PausedTab.activeSelf);
        if (PausedTab.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(FirstPauseBtn.gameObject);
            InventoryTab.SetActive(false);
            JournalTab.SetActive(false);

        }
    }

    private void UpdatePausedStateCanvas()
    {
        bool isAnyActiveTab = JournalTab.activeSelf || InventoryTab.activeSelf || PausedTab.activeSelf;
        
      if(isAnyActiveTab)
        {
           UIManager.Instance.PausedState(true);
        }
        else
        {
            UIManager.Instance.PausedState(false);
        }
    }


}
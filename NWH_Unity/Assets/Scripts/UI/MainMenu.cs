using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Main Menu")]
    //button
    [SerializeField] private Button _continueBtn;
    [SerializeField] private Button _newGameBtn;
    [SerializeField] private Button _optionBtn;
    [SerializeField] private Button _creditBtn;
    [SerializeField] private Button _exitBtn;
/// <summary>
/// ///////////////////////
/// </summary>
    [SerializeField] private Button _backBtn;

    public GameObject NewGameType;

    [SerializeField] private Button _typeABtn;
    [SerializeField] private Button _typeBBtn;

    [SerializeField] private bool _haveSavedGame = false;
    private void Start()
    {

        if(!_haveSavedGame)
        {
            _continueBtn.interactable = false;
        } else
        {
            _continueBtn.interactable = true;
        }

        ButtonCLickState();

    }

    private void ButtonCLickState()
    {
        _continueBtn.onClick.AddListener(ContinueGameState);
        _newGameBtn.onClick.AddListener(NewGame);
        _optionBtn.onClick.AddListener(OpenOptions);
        _creditBtn.onClick.AddListener(OpenCredit);
        _exitBtn.onClick.AddListener(ExitGame);

        //GameTyeState
        _typeABtn.onClick.AddListener(() => OnGameTypeButtonClicked(0));
        _typeBBtn.onClick.AddListener(() => OnGameTypeButtonClicked(1));

    }

    private void ContinueGameState()
    {
        //TODO: Continue Game
    }

    private void OnGameTypeButtonClicked(int gameType)
    {
        GameManager.Instance.GameTypeState(gameType);
        NewGameType.SetActive(false);
        GameManager.Instance.ChangeValueToState(1);
        GameManager.Instance.StartGame("Prototype_1");
        
    }

    private void NewGame()
    {
        //AudioManager.Instance.PlayClickSound();
        //UIManager.Instance.NewGameType.SetActive(true);
        NewGameType.SetActive(true);


    }

    private void OpenCredit()
    {
//        AudioManager.Instance.PlayClickSound();
       // mainMenu.SetActive(false);
      // Debug.Log("Open Credit");
       UIManager.Instance.CreditTab.SetActive(true);
       if (UIManager.Instance.CreditTab != null)
       {
            _backBtn.onClick.AddListener(BackClickBehaviour);
       }
    }

    private void BackClickBehaviour()
    {
        UIManager.Instance.CreditTab.SetActive(false);
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    
    private void OpenOptions()
    {
        // AudioManager.Instance.PlayClickSound();
      //  UIManager.Instance.MainMenu.gameObject.SetActive(false);
        UIManager.Instance.OptionsMenu.gameObject.SetActive(true);

    }

    

}
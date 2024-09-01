using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;

public class GamepadToggleUI : MonoBehaviour
{
    public int CurrentIndex = 0;

    [Header("Objects")]
    public Button ToggleRight_Btn;
    public Button ToggleLeft_Btn;

    [Header("Text")]
    public GameObject OptionList;
    public List<GameObject> listsGameObjects = new List<GameObject>();

    private bool isSelected = false;

    private void Awake() 
    {
    }

    private void Start() 
    {
       ToggleRight_Btn.onClick.AddListener(ToggleRight);
       ToggleLeft_Btn.onClick.AddListener(ToggleLeft);
        UpdateToggleButtons();
        ToggleBehaviour(CurrentIndex);
    }


    private void ToggleLeft()
    {
       if(CurrentIndex > 0)
       {
           CurrentIndex--;
           ToggleBehaviour(CurrentIndex);
           UpdateToggleButtons();
       }
    }
    
    private void ToggleRight()
    {
         if(CurrentIndex < listsGameObjects.Count - 1)
       {
           CurrentIndex++;
           ToggleBehaviour(CurrentIndex);
           UpdateToggleButtons();
       }
    }

    private void UpdateToggleButtons()
    {
        ToggleLeft_Btn.interactable = CurrentIndex > 0;
        ToggleRight_Btn.interactable = CurrentIndex < listsGameObjects.Count - 1;
    }

    private void ToggleBehaviour(int index)
    {
       for (int i = 0; i < listsGameObjects.Count; i++)
       {
           listsGameObjects[i].SetActive(i == index);
       }
    }
}

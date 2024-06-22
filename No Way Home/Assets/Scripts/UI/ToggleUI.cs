using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;

public class ToggleUI : MonoBehaviour
{
    public int CurrentIndex = 0;

    [Header("Objects")]
    public Button ToggleRight_Btn;
    public Button ToggleLeft_Btn;

    [Header("Text")]
    public List<GameObject> ToggleTextList;

    private bool isSelected = false;

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
         if(CurrentIndex < ToggleTextList.Count - 1)
       {
           CurrentIndex++;
           ToggleBehaviour(CurrentIndex);
           UpdateToggleButtons();
       }
    }

    private void UpdateToggleButtons()
    {
        ToggleLeft_Btn.interactable = CurrentIndex > 0;
        ToggleRight_Btn.interactable = CurrentIndex < ToggleTextList.Count - 1;
    }

    private void ToggleBehaviour(int index)
    {
       for (int i = 0; i < ToggleTextList.Count; i++)
       {
           ToggleTextList[i].SetActive(i == index);
       }
    }

}

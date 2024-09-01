using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField]
     private List<Button> _allButtons = new List<Button>();
    [SerializeField]
    private List<GameObject> _allOptionTabs = new List<GameObject>();

    [SerializeField]
    private List<Button> _firstSlectableButtons = new List<Button>();

    private void Start() 
    {
        for (int i = 0; i < _allButtons.Count; i++)
        {
            int index = i;
            _allButtons[index].onClick.AddListener(() => ShowOptionPanels(index));
        }
    }

    private void ShowOptionPanels(int index)
    {
        switch (index)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
                for (int i = 0; i < _allOptionTabs.Count; i++)
                {
                    _allOptionTabs[i].SetActive(i == index);
//                    EventSystem.current.SetSelectedGameObject(_firstSlectableButtons[index].gameObject);
                    //SelectFirstUIElement(index);
                }
                break;
        }
    }

    private void SelectFirstUIElement(int index)
    {
        switch (index)
        {
            case 0:
               // EventSystem.current.SetSelectedGameObject(_firstSlectableButtons[index]);
                break;
            case 1:
                EventSystem.current.SetSelectedGameObject(_allOptionTabs[index]);
                break;
            case 2:
                 EventSystem.current.SetSelectedGameObject(_allOptionTabs[index]);
                break;
            case 3:
                 EventSystem.current.SetSelectedGameObject(_allOptionTabs[index]);
                break;
            case 4:
                 EventSystem.current.SetSelectedGameObject(_allOptionTabs[index]);
                break;
                
        }
    }

}

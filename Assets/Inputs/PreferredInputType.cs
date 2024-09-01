using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PreferredInputType : MonoBehaviour
{
    [SerializeField] private GameObject _keyboardMouse;
    [SerializeField] private GameObject _gamepad;
    [SerializeField] private GameObject _dualShock;

    public GamepadToggleUI toggleUI;

    public string currentController;
    public GameObject controllerListHolder;

    public List<string> controllerList = new List<string>();

  private void Start() 
  {
    controllerList.AddRange(InputManager.Instance.connectedDevices);
    UpdateControllerList();
  }
   
  //  public void OnValueChanged(string value)
  //  {
  //        controllerList.Clear();
  //        controllerList.Add(value);
  //        if(currentController != value)
  //        {
  //            UpdateControllerList();
             
  //            currentController = value;
  //        }   
  //  }

   public void UpdateControllerList()
   {
     if (controllerList.Contains("Xbox Controller"))
        {
            Instantiate(_gamepad, controllerListHolder.transform);
            InputManager.Instance.currentDevice = InputManager.Device.XboxGamepad;
             return;
        }
        else if (controllerList.Contains( "DualShock Controller"))
        {
            Instantiate(_dualShock, controllerListHolder.transform);
            InputManager.Instance.currentDevice = InputManager.Device.DualShockGamepad;
            return;
        } else if (controllerList.Contains("Mouse"))
        {
            Instantiate(_keyboardMouse, controllerListHolder.transform);
            InputManager.Instance.currentDevice = InputManager.Device.MouseKeyboard;
        }
   }
}

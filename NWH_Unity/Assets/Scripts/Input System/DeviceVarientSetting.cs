using SGS.InputSystem;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DeviceVarientSetting : MonoBehaviour
{
    [System.Serializable]
    public sealed class DeviceChange
    {
        public InputManager.Device device;
        public int bindindIndex = -1;
    }

    public GameObject ActionImageObj;
    public bool DisableOnPc;


    public DeviceChange[] DeviceChangeActions;
    public InputActionRef ActionReference;

    public void ChangeDevice(InputManager.Device device)
    {
        if(!ActionImageObj)
        {
            ActionImageObj = gameObject;
        }

        if(device == InputManager.Device.MouseKeyboard && DisableOnPc)
        {
            gameObject.SetActive(false);

        }

        foreach(var entry in DeviceChangeActions)
        {
            if(entry.device == device)
            {
                if(entry.bindindIndex >= 0)
                {
                    
                }
            }
        }
    }
}

[System.Serializable]
public class InputActionRef
{
    public string ActionMap;
    public string ActionName;
}
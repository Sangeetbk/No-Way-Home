using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public enum Device
        {
            None,
            MouseKeyboard,
            DualShockGamepad,
            XboxGamepad
        }

        public Device prefferedDevice = Device.None;
}

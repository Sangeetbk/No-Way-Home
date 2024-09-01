using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;

public class InputManager : Singleton<InputManager>
{
    public enum Device
        {
            None,
            MouseKeyboard,
            DualShockGamepad,
            XboxGamepad
        }

      public Device currentDevice = Device.None;

    public List<String> connectedDevices = new List<String>();
    
    protected override void Awake() 
    {
        base.Awake();
    }
    private void OnEnable() 
    {
        InputSystem.onDeviceChange += OnDeviceChange;
        DisplayConnectedDevices();
    }

    private void OnDisable() 
    {
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (change == InputDeviceChange.Added)
        {
            if (device is Gamepad || device is Mouse || device is Keyboard || device is DualShockGamepad)
            {
                AddDevice(device.displayName);
                Debug.Log($"Device added: {device.displayName}");
            }
        }
        else if (change == InputDeviceChange.Removed)
        {
            if (device is Gamepad || device is Mouse || device is Keyboard || device is DualShockGamepad)
            {
                RemoveDevice(device.displayName);
                Debug.Log($"Device removed: {device.displayName}");
            }
        }
    }

    private void DisplayConnectedDevices()
    {
        var devices = InputSystem.devices;

        connectedDevices.Clear();
        foreach (var device in devices)
        {
            if (device is Gamepad || device is Mouse || device is Keyboard || device is DualShockGamepad)
            {
                AddDevice(device.displayName);
            }
        }
    }

    private void AddDevice(string deviceName)
    {
        if (!connectedDevices.Contains(deviceName))
        {
            connectedDevices.Add(deviceName);
        }
    }

    private void RemoveDevice(string deviceName)
    {
        if (connectedDevices.Contains(deviceName))
        {
            connectedDevices.Remove(deviceName);
        }
    }
}





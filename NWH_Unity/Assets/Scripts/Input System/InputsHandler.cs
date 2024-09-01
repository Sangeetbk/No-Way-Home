using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization.SmartFormat.Utilities;

namespace SGS.InputSystem
{
    public class InputsHandler : Singleton<InputsHandler>
    {
        public enum Device
        {
            None,
            MouseKeyboard,
            DualShockGamepad,
            XboxGamepad
        }

        public Device prefferedDevice = Device.None;
        public FrameInput FrameInput { get; private set; }

#region All Inputs
        private PlayerInputAction _playerInputActions;
        private InputAction _move;
        private InputAction _pickUp;
        private InputAction _open;
        private InputAction _cameraLook;
        private InputAction _run;
        private InputAction _jump;
        private InputAction _crouch;
        private InputAction _cameraZoom;
        private InputAction _inspect;

        //Input for UI
        private InputAction _inventoryMenu;
        private InputAction _pauseMenu;
        private InputAction _journalSystem;
        private InputAction _rightClickForItem;


        // Events
        public event Action RightClickEvent;
#endregion
        protected override void Awake()
        {
            base.Awake();
            _playerInputActions = new PlayerInputAction();
            _move = _playerInputActions.Player.Move;
            _pickUp = _playerInputActions.Player.Pickup;
            _open = _playerInputActions.Player.Open;
            _cameraLook = _playerInputActions.Player.Look;
            _run = _playerInputActions.Player.Run;
            _crouch = _playerInputActions.Player.Crouch;
            _jump = _playerInputActions.Player.Jump;
            _cameraZoom = _playerInputActions.Player.CameraZoom;
            _inspect = _playerInputActions.Player.Inspect;

            // for UI
            _inventoryMenu = _playerInputActions.UserInterface.InventoryMenu;
            _pauseMenu = _playerInputActions.UserInterface.PauseMenu;
            _journalSystem = _playerInputActions.UserInterface.JournalSystem;
            _rightClickForItem = _playerInputActions.UserInterface.ItemOption;
           
        }

        private void OnEnable()
        {
            _playerInputActions.Enable();
        }

        private void OnDisable()
        {
            _playerInputActions.Disable();
        }

        private void Update()
        {
            FrameInput = GatherInput();
            if(FrameInput.ItemOption) 
            { 
                RightClickEvent?.Invoke(); 
            }
        }

        private FrameInput GatherInput()
        {
            return new FrameInput
            {
                Move = _move.ReadValue<Vector2>(),
                Open = _open.WasPressedThisFrame(),
                PickUP = _pickUp.WasPressedThisFrame(),
                CameraLook = _cameraLook.ReadValue<Vector2>(),
                Jump = _jump.WasPressedThisFrame(),
                Crouch = _crouch.WasPressedThisFrame(),
                Run = _run.IsPressed(),
                Inspect = _inspect.WasPressedThisFrame(),
                CameraZoom = _cameraZoom.WasPressedThisFrame(),


                // for UI
                InventoryMenu = _inventoryMenu.WasPerformedThisFrame(),
                PauseMenu = _pauseMenu.WasPerformedThisFrame(),
                JournalSystem = _journalSystem.WasPerformedThisFrame(),
                ItemOption = _rightClickForItem.WasPerformedThisFrame()
            };
        }
    }
}


public struct FrameInput 
{
    public Vector2 Move;
    public bool PickUP;
    public bool Open;
    public Vector2 CameraLook;
    public bool Run;
    public bool Crouch;
    public bool Jump;
    public bool Inspect;
    public bool CameraZoom;

    // For UI
    public bool InventoryMenu;
    public bool PauseMenu;
    public bool JournalSystem;
    public bool ItemOption;
}


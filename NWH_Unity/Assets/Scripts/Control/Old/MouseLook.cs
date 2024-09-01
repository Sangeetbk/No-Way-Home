using System;
using System.Collections;
using System.Collections.Generic;
using SGS.InputSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class MouseLook : MonoBehaviour
{
    [SerializeField] [Range(0,1)]
    private float mouseSensitivity = 1f;

    [SerializeField]
    private Transform playerBody;

    private float xRotation = 0f;

     private InputsHandler _inputHandler;
    private FrameInput _frameInput;
    public bool canLook = true;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Start() 
    {
        _inputHandler = GetComponentInParent<InputsHandler>();
    }

    private void LateUpdate() 
    {
        // if(Inventory.Instance.InventoryWindow.activeInHierarchy == true)
        // {
        //     canLook = false;
        // }
        // else
        // {
        //     canLook = true;
        // }


        if(canLook == true)
            CameraLook();
    }

    private void CameraLook()
    {
        _frameInput = _inputHandler.FrameInput;
        xRotation -= _frameInput.CameraLook.y * mouseSensitivity;
        xRotation = Mathf.Clamp(xRotation,-70f,70f);
        
        transform.localRotation = Quaternion.Euler(xRotation ,0f,0f);
        playerBody.Rotate(Vector3.up*_frameInput.CameraLook.x * mouseSensitivity);

    }
    
   
}
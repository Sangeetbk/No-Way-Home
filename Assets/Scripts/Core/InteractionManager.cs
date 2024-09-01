using System;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using SGS.InputSystem;
using UnityEngine.InputSystem;
using SGS.Gameplay;
using SGS.Inventory;

public class InteractionManager : MonoBehaviour
{
    [FormerlySerializedAs("RayCastCheckRate")]
    [Header("RayCast Settings")]
    [SerializeField] private float checkRate = 0.1f;
    [SerializeField] private float lastCheckTime;
    [SerializeField] private float maxCheckDistance = 5f;
    [SerializeField] private LayerMask layerMask;


    private GameObject _curInteractGameObject;
    private IInteractable _curInteractable;

    public TextMeshProUGUI promptText;
    private Camera _mainCam;

    [Header("Handling Inputs")]
    public PlayerInput PlayerInput;
    public InputsHandler PlayerInputHandler;
    private FrameInput _frameInput;

    private void Start()
    {
        _mainCam = Camera.main;
    }

    private void Update()
    {
        OnInteractInput();
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;
            Ray ray = _mainCam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != _curInteractGameObject)
                {
                    _curInteractGameObject = hit.collider.gameObject;
                    _curInteractable = hit.collider.GetComponent<IInteractable>();

                    if (_curInteractable != null)
                    {

                        SetPromptText();
                    }
                }
            }
            else
            {
                _curInteractable = null;
                _curInteractGameObject = null;
                promptText.gameObject.SetActive(false);
            }

        }
    }

    private void OnInteractInput()
    {
        _frameInput = PlayerInputHandler.FrameInput;
        if (_frameInput.PickUP && _curInteractable != null)
        {
            _curInteractable.OnInteract();
            _curInteractGameObject = null;
            _curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }

    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);

        string prompt = _curInteractable.GetInteractPrompt();
        ItemPickup itemPickup = _curInteractable as ItemPickup;
        if (itemPickup != null)
        {
            switch (itemPickup.ItemVariety)
            {
                case ItemVariety.None:
                case ItemVariety.Pickable:
                    promptText.text = string.Format("<b>[E]<b> {0}", prompt);
                    break;
                case ItemVariety.Openable:
                    promptText.text = string.Format("<b>[F]<b> {0}", prompt);
                    break;

            }
        }
        else
        {
            promptText.text = string.Format("<b>[E]</b> {0}", prompt);
        }
    }


}

   
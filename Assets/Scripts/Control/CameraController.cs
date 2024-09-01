using System;
using SGS.Controls;
using SGS.InputSystem;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //

    public TextMeshProUGUI text;
    //
    [SerializeField] private GameObject _cineMachineCam;
     public GameObject MainCamera;
    private float _cinemachineTargetPitch;
    [SerializeField] private float mouseSensitivity = 2.0f;
    [SerializeField] private float verticalSpeed = 2.0f;
    [SerializeField] private float horizontalSpeed = 2.0f;
    [SerializeField] private float clampAngle = 70.0f;

    [SerializeField] private InputsHandler _playerInput;
    private FrameInput _frameInput;

    private GameObject _mainCamera;
    private Vector2 smoothedInput;
    private Vector2 smoothInputVelocity;
    private const float smoothTime = 0.05f; // Smoothing time
    private float _threshold = 0.3f;

    // Store Initial Camera Height
    [SerializeField]
    private float _initialCameraHeight;
    private float _crouchHeight = 0.5f;
    public bool _isCrouching = false;
    private float _crouchTransitionSpeed = 8.0f;

    // Camera zoom settings
    private float _defaultFOV;
    [SerializeField] private float _zoomedFOV = 30f;
    [SerializeField] private float _zoomSpeed = 10f;

    private bool _isZooming = false;
    private CinemachineCamera _cineCam;

    //for debugging
    [SerializeField] private bool _canLook;

    private void Awake()
    {
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
        _initialCameraHeight = MainCamera.transform.localPosition.y;
        if (!_canLook) return;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _cineCam = _cineMachineCam.GetComponent<CinemachineCamera>();
    }

    private void Start() 
    {
        _defaultFOV = _cineCam.Lens.FieldOfView;
    }

    private void Update()
    {
        UpdateFrametime();
    }

    private void LateUpdate()
    {
        if (GameManager.Instance.CurrentGameState == GameManager.GameState.PAUSED) { return; }
        if (!_canLook) return;

        RotateCamera();
        HandleCameraCrouch();
        HandleCameraZoom();
    }

    private void RotateCamera()
    {
        _frameInput = _playerInput.FrameInput;

        if (_frameInput.CameraLook.sqrMagnitude >= _threshold)
        {
            smoothedInput = Vector2.SmoothDamp(smoothedInput, _frameInput.CameraLook, ref smoothInputVelocity, smoothTime);

            _cinemachineTargetPitch += smoothedInput.y * horizontalSpeed * mouseSensitivity;
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, -clampAngle, clampAngle);

            _cineMachineCam.transform.localRotation = Quaternion.Euler(-_cinemachineTargetPitch, 0.0f, 0.0f);

            float _rotationVelocity = smoothedInput.x * verticalSpeed * mouseSensitivity;
            transform.Rotate(Vector3.up * _rotationVelocity);
        }
    }
    private void HandleCameraCrouch()
    {
        _frameInput = _playerInput.FrameInput;
      //  bool isGrounded = _playerInput.GetComponentInParent<FirstPersonController>().IsGrounded;

        if (_frameInput.Crouch && !_isCrouching) 
        {
            _isCrouching = true;
            
        } else if (_frameInput.Crouch && _isCrouching)
        {
            _isCrouching = false;
        }

        float targetHeight = _isCrouching ? -_crouchHeight : _initialCameraHeight;

         // Gradually move the camera to the target position
         Vector3 targetPosition = new Vector3(MainCamera.transform.localPosition.x, targetHeight, MainCamera.transform.localPosition.z);
         MainCamera.transform.localPosition = Vector3.Lerp(MainCamera.transform.localPosition, targetPosition, Time.deltaTime * _crouchTransitionSpeed);
    }

     private void HandleCameraZoom()
    {

        if (Input.GetMouseButtonDown(1))
        {
            _isZooming = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            _isZooming = false;
        }

        float targetFOV = _isZooming ? _zoomedFOV : _defaultFOV;
        _cineCam.Lens.FieldOfView = Mathf.Lerp(_cineCam.Lens.FieldOfView, targetFOV, Time.deltaTime * _zoomSpeed);
    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f) angle += 360f;
        if (angle > 360f) angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }

    // DebugWindow
    private void UpdateFrametime()
    {
//        text.text = _frameInput.CameraLook.sqrMagnitude.ToString();
    }
}

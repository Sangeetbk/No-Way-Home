using SGS.InputSystem;
using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //

    public TextMeshProUGUI text;
    //
    [SerializeField] private GameObject _cineMachineCam;
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

    //for debugging
    [SerializeField] private bool _canLook;

    private void Awake()
    {
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
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

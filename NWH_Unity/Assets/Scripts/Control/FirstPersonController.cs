using System;
using System.Collections;
using System.Collections.Generic;
using SGS.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using SGS.Saving;

namespace SGS.Controls
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerInput))]
    public class FirstPersonController : MonoBehaviour, ISaveable
    {
        [Header("Player")]
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _sprintSpeed = 10f;
        [SerializeField] private float _jumpForce = 15f;

      //  private float _crouchDelta = 2f;
        [SerializeField] private float _gravity = -20f;
        private Animator anim;

        private bool canClimb = false;

        [Header("Ground Check")]
        [SerializeField]
        private Transform groundCheck;
        [SerializeField]
        private float groundDistance = 0.4f;
        [SerializeField]
        private LayerMask _groundMask;
        private CameraController _cameraController;
        public bool AdjustCameraHeight;
        public bool IsGrounded;

        private Rigidbody rb;
        #region Other_Settings
        private CharacterController _playerController;
        [SerializeField]
        private InputsHandler _playerInput;
        private FrameInput _frameInput;

        #endregion
        [HideInInspector]
        public Vector2 movementInput;
        private Vector3 velocity;

        private void Awake()
    {
        _playerInput = GetComponent<InputsHandler>();
        _playerController = GetComponent<CharacterController>();
        _cameraController = GetComponent<CameraController>();
        if(_cameraController == null) { Debug.LogWarning("No CameraController found on " + gameObject.name); }

        /*rb = GetComponent<Rigidbody>();
        ApplyGravity(true);
        anim = GetComponent<Animator>();*/
    }

        
    private void Update()
    {
        HandleMovement();
        HandleJump();
    }

    private void FixedUpdate()
    {
        ApplyGravity(true);
    }

    private void HandleMovement()
    {
       
        _frameInput = _playerInput.FrameInput;
        Vector3 moveDirection = new Vector3(_frameInput.Move.x, 0f, _frameInput.Move.y);
        moveDirection = transform.TransformDirection(moveDirection);
        
        float currentSpeed = _frameInput.Run && IsGrounded ? _sprintSpeed : _moveSpeed;

            if (_frameInput.Run) { Debug.Log("Handling Run Button"); }
        
        _playerController.Move(moveDirection * currentSpeed * Time.deltaTime);
    }

    private void HandleJump()
    {
        /*if ( _frameInput.Jump && canClimb)
        {
            /*if(_cameraController._isCrouching == true) 
            {
                _cameraController._isCrouching = false;
                    return;
            }#1#
            Debug.Log("Player is Jumping");
            velocity.y = Mathf.Sqrt(_jumpForce * -2f * _gravity);
        }*/
        ApplyGravity(false);
        if (Input.GetKeyDown(KeyCode.Space) && canClimb)
        {
            Debug.Log("Clicking Climbing button");
            anim.SetTrigger("Climb");
          
        }
            
        ApplyGravity(true);
        
    }

    private void ApplyGravity(bool arg)
    {
        
        
        IsGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, _groundMask);

        if (IsGrounded && velocity.y < 0 && arg)
        {
            velocity.y = -2f;
        } 

        velocity.y += _gravity * Time.deltaTime;
        _playerController.Move(velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Climbable"))
        {
            canClimb = true;
            Debug.Log("Player can climb");
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Climbable"))
        {
            canClimb = false;
            Debug.Log("Player out of climb range");
        }
    }


    #region SaveMethod
        public object CaptureState()
        {
            Debug.Log("Save Location " + this.transform.position +" , " + this.transform.eulerAngles);
            return new SerializableTransform(this.transform.position, this.transform.eulerAngles);
        }

        public void RestoreState(object state)
        {
            _playerController.enabled = false;
            SerializableTransform serializableTransform = (SerializableTransform)state;
            SerializableVector3 newPosition = serializableTransform.Position;
            SerializableVector3 newRotation = serializableTransform.Rotation;
            this.transform.position = newPosition.GetVector3();
            this.transform.eulerAngles = newRotation.GetVector3();

            _playerController.enabled = true;
        }

#endregion
    }

}
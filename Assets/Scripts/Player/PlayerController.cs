using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float _movementSpeed = 12f;
    [SerializeField]
    public float _jumpforce = 5f;
    [SerializeField]
    public float _distanceToGround = 0.5f;
    [SerializeField]
    public float _smoothRotation = 4f;
    [SerializeField]
    public float _cameraSmoothRotation = 4f;
    [SerializeField]
    public float _maxCameraRotationUp = 14;
    [SerializeField]
    public float _maxCameraRotationDown = -14;
    [SerializeField]
    public float _sprintSpeed = 1.5f;
    

    private PlayerInputActions playerInputActions;
    private Rigidbody rb;
    private PlayerAnimationController playerAnimationController;
    private Stamina stamina;

    private GameObject firstPersonCamera;
    private GameObject cameraAnchor;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
        playerInputActions.Player.Jump.performed += Jump;

        playerAnimationController = GetComponent<PlayerAnimationController>();
        stamina = GetComponent<Stamina>();

        firstPersonCamera = GameObject.FindGameObjectWithTag("FirstPersonCamera");
        cameraAnchor = GameObject.FindGameObjectWithTag("FirstPersonCameraAnchor");

        StartCoroutine(ChangeCameraMode());
    }

    public void Jump(InputAction.CallbackContext Context)
    {
        if (IsGround()) rb.AddForce(Vector3.up * _jumpforce, ForceMode.Impulse);
    }

    private bool IsGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, _distanceToGround);
    }

    public void Move(float upDown, float rightLeft)
    {
        rb.AddForce(new Vector3(rightLeft, 0, upDown), ForceMode.Force);
        playerAnimationController.SetAnimationMove(new Vector2(rightLeft, upDown));
    }

    public void SetRotation()
    {
        Vector2 newRotation = playerInputActions.Player.Aim.ReadValue<Vector2>();
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.y += newRotation.x;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(rotation), _smoothRotation * Time.fixedDeltaTime);
    }

    public void SetCameraRotation()
    {
        Vector2 newPosition = playerInputActions.Player.Aim.ReadValue<Vector2>();
        Vector3 position = cameraAnchor.transform.localPosition;
        position.y += newPosition.y;
        position.y = Mathf.Clamp(position.y, _maxCameraRotationDown, _maxCameraRotationUp);
        position.y = Mathf.Lerp(cameraAnchor.transform.localPosition.y, position.y, _cameraSmoothRotation * Time.fixedDeltaTime);
        cameraAnchor.transform.localPosition = position;
    }

    public IEnumerator ChangeCameraMode()
    {
        while(true)
        {
            float state = playerInputActions.Player.CameraSwitch.ReadValue<float>();
            if (state == 1)
            {
                firstPersonCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = firstPersonCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority == 11 ? 9 : 11;
                yield return new WaitForSeconds(1f);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private bool sprintState = false;
    public IEnumerator Sprint(float directionUpDown, float directionRightLeft)
    {
        sprintState = true;
        float state = playerInputActions.Player.Sprint.ReadValue<float>();
        if(state == 1 && stamina._stamina >= 25 && (directionUpDown != 0 || directionRightLeft != 0) && IsGround())
        {
            playerAnimationController.StartSprint(_sprintSpeed);
            stamina.StaminaDown(25);
            yield return new WaitForSeconds(1);
        }
        else
        {
            playerAnimationController.StopSprint();
        }
        sprintState = false;
    }

    private void FixedUpdate()
    {
        float upDown = playerInputActions.Player.UpDown.ReadValue<float>();
        float rightLeft = playerInputActions.Player.RightLeft.ReadValue<float>();


        Move(upDown, rightLeft);
        SetRotation();
        SetCameraRotation();
        if(!sprintState) StartCoroutine(Sprint(upDown, rightLeft));
    }
}

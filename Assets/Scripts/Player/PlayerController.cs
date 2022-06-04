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


    private PlayerInputActions playerInputActions;
    private Rigidbody rb;
    private PlayerAnimationController playerAnimationController;

    private GameObject firstPersonCamera;
    private GameObject firspPresonCameraAnchor;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
        playerInputActions.Player.Jump.performed += Jump;

        playerAnimationController = GetComponent<PlayerAnimationController>();

        firstPersonCamera = GameObject.FindGameObjectWithTag("FirstPersonCamera");
        firspPresonCameraAnchor = GameObject.FindGameObjectWithTag("FirstPersonCameraAnchor");

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
        rb.AddForce(new Vector3(rightLeft, 0, upDown) * _movementSpeed, ForceMode.Force);
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
        Vector3 position = firspPresonCameraAnchor.transform.position;
        position.y += newPosition.y;
        position.y = Mathf.Clamp(position.y, _maxCameraRotationDown, _maxCameraRotationUp);
        position.y = Mathf.Lerp(firspPresonCameraAnchor.transform.position.y, position.y, _cameraSmoothRotation * Time.fixedDeltaTime);
        firspPresonCameraAnchor.transform.position = position;
    }

    public IEnumerator ChangeCameraMode()
    {
        while(true)
        {
            float some = playerInputActions.Player.CameraSwitch.ReadValue<float>();
            Debug.Log(some);
            if (some == 1)
            {
                firstPersonCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = firstPersonCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority == 10 ? 9 : 10;
                yield return new WaitForSeconds(1f);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void FixedUpdate()
    {
        float upDown = playerInputActions.Player.UpDown.ReadValue<float>();
        float rightLeft = playerInputActions.Player.RightLeft.ReadValue<float>();


        Move(upDown, rightLeft);
        SetRotation();
        SetCameraRotation();
        ChangeCameraMode();
    }
}

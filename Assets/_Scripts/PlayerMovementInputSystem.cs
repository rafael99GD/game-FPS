using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementInputSystem : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float actualMoveSpeed;

    [SerializeField, Range(1, 360)] private float rotationSpeed;

    [SerializeField] private float rotationAngleXLimit = 40f;

    [SerializeField] private float jumpForce, verticalVelocity, terminalVelocity, gravity;

    [SerializeField] private bool isXInverted, isYInverted;

    [SerializeField] Transform cameraTransform;

    private int _inversionX => isXInverted ? -1 : 1;
    private int _inversionY => isYInverted ? -1 : 1;

    private CharacterController _characterController;
    private Vector3 _playerCameraRotation;
    private Vector2 _cameraRotation;

    private void Start()
    {
        InputManager.Instance.JumpPerformed += OnJumpPerformed;

        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        _characterController = GetComponent<CharacterController>();

        _playerCameraRotation = Vector3.zero;
        _cameraRotation = Vector2.zero;

        actualMoveSpeed = moveSpeed;
    }

    private void Update()
    {
        if (!_characterController.isGrounded)
        {
            ApplyGravity();
        }
        else
        {
            if(verticalVelocity < 0)
                verticalVelocity = -1;

        }

        var movementInput = InputManager.Instance.GetHorizontalMovement();

        Vector3 movementDirection = transform.right * movementInput.x +
                                    transform.forward * movementInput.y;

        movementDirection.Normalize();

        // Usamos actualMoveSpeed para reflejar la velocidad real del jugador
        _characterController.Move(movementDirection * (actualMoveSpeed * Time.deltaTime) + Vector3.up * (verticalVelocity * Time.deltaTime));

        _cameraRotation.x = InputManager.Instance.GetMouseDelta().y * _inversionY;
        _cameraRotation.y = InputManager.Instance.GetMouseDelta().x * _inversionX;

        Vector3 rotationVector = new Vector3(_cameraRotation.x, 0, 0);
        cameraTransform.Rotate(rotationVector);

        float angleTransformation = cameraTransform.rotation.eulerAngles.x > 180 ? cameraTransform.localEulerAngles.x - 360 : cameraTransform.localEulerAngles.x;

        rotationVector = new Vector3(Mathf.Clamp(angleTransformation, -rotationAngleXLimit, rotationAngleXLimit), 0, 0);
        cameraTransform.localEulerAngles = rotationVector;

        _playerCameraRotation.y = _cameraRotation.y;

        transform.Rotate(_playerCameraRotation * (rotationSpeed * Time.deltaTime));
    }

    public void ApplySlowness(float slowness)
    {
        actualMoveSpeed -= slowness;
    }

    public void UnapplySlowness()
    {
        actualMoveSpeed = moveSpeed;
    }
    public void ApplySpeedBoost(float boostAmount)
    {
        actualMoveSpeed += boostAmount;
    }

    public void UnapplySpeedBoost()
    {
        actualMoveSpeed = moveSpeed;
    }


    private void ApplyGravity()
    {
        verticalVelocity -= gravity * Time.deltaTime;

        if (verticalVelocity <= -terminalVelocity)
        {
            verticalVelocity = -terminalVelocity;
        }
    }

    private void OnJumpPerformed()
    {
        if(_characterController.isGrounded)
            verticalVelocity = jumpForce;
    }

}

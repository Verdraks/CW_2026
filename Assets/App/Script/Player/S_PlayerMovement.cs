using UnityEngine;

public class S_PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private SSO_PlayerValue playerValues;

    [Header("References")]
    [SerializeField] private Rigidbody rb;

    [Header("Input")]
    [SerializeField] private RSE_OnPlayerMove rse_OnPlayerMove;
    [SerializeField] private RSE_OnPlayerSprint rse_OnPlayerSprint;
    [SerializeField] private RSE_OnPlayerJump rse_OnPlayerJump;

    [Header("Output")]
    [SerializeField] private RSO_PlayerGroundCheckValue rso_PlayerGroundCheckValue;
    [SerializeField] private RSO_PlayerSprintValue rso_PlayerSprintValue;
    [SerializeField] private RSO_PlayerMoveValue rso_PlayerMoveValue;

    private bool isSprinting = false;
    private bool isGrounded = true;
    private float coyoteTimer = 0f;
    private float jumpBufferTimer = 0f;
    private Vector2 moveInput = Vector2.zero;

    private void OnEnable()
    {
        rse_OnPlayerMove.Action += MoveInput;
        rse_OnPlayerSprint.Action += Sprint;
        rse_OnPlayerJump.Action += Jump;
    }
    private void OnDisable()
    {
        rse_OnPlayerMove.Action -= MoveInput;
        rse_OnPlayerSprint.Action -= Sprint;
        rse_OnPlayerJump.Action -= Jump;
    }

    private void FixedUpdate()
    {
        CheckGround();
        Move();
        HandleJump();
    }
    private void Move()
    {
        Vector3 moveDir = transform.forward * moveInput.y + transform.right * moveInput.x;
        moveDir.Normalize();

        float targetSpeed = isSprinting ? playerValues.moveSpeed * playerValues.sprintMultiplier : playerValues.moveSpeed;

        Vector3 targetVelocity = moveDir * targetSpeed;
        Vector3 currentVelocity = rb.linearVelocity;

        Vector3 horizontalVelocity = new Vector3(currentVelocity.x, 0f, currentVelocity.z);
        Vector3 velocityDifference = targetVelocity - horizontalVelocity;

        float control = isGrounded ? 1f : playerValues.airControlMultiplier;
        float accel = playerValues.acceleration * control;

        velocityDifference = Vector3.ClampMagnitude(velocityDifference, accel * Time.fixedDeltaTime);

        rb.AddForce(velocityDifference, ForceMode.VelocityChange);

        // Friction quand aucun input
        if (isGrounded && moveInput.magnitude < 0.1f)
        {
            rb.AddForce(-horizontalVelocity * playerValues.groundFriction * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
    private void MoveInput(Vector2 input)
    {
        moveInput = input;
        rso_PlayerMoveValue.Set(moveInput);
    }

    private void Sprint(bool sprinting)
    {
        isSprinting = sprinting;
        rso_PlayerSprintValue.Set(isSprinting);
    }
    private void Jump(bool jump)
    {
        if (jump) jumpBufferTimer = playerValues.jumpBufferTime;
    }
    private void HandleJump()
    {

        jumpBufferTimer -= Time.fixedDeltaTime;

        if(jumpBufferTimer > 0f && coyoteTimer > 0f)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * playerValues.jumpForce, ForceMode.Impulse);
            jumpBufferTimer = 0f;
            coyoteTimer = 0f;
        }

    }
    private void CheckGround()
    {
        bool wasGrounded = isGrounded;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerValues.groundCheckDistance, playerValues.groundLayer);
        Debug.DrawLine(transform.position, transform.position + Vector3.down * playerValues.groundCheckDistance, isGrounded ? Color.green : Color.red);
        if (isGrounded)
        {
            coyoteTimer = playerValues.coyoteTime;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
        }
        rso_PlayerGroundCheckValue.Set(isGrounded);
    }
}
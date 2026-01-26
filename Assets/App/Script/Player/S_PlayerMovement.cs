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

    [Header("Output")]
    [SerializeField] private RSO_PlayerGroundCheckValue rso_PlayerGroundCheckValue;
    [SerializeField] private RSO_PlayerSprintValue rso_PlayerSprintValue;
    [SerializeField] private RSO_PlayerMoveValue rso_PlayerMoveValue;

    private bool isSprinting = false;
    private bool isGrounded = true;
    private float speed = 0;
    private Vector2 moveInput = Vector2.zero;

    private void OnEnable()
    {
        rse_OnPlayerMove.Action += MoveInput;
        rse_OnPlayerSprint.Action += Sprint;
    }
    private void OnDisable()
    {
        rse_OnPlayerMove.Action -= MoveInput;
        rse_OnPlayerSprint.Action -= Sprint;
    }
    private void Update()
    {
        CheckGround();
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        Vector3 moveDir = transform.forward * moveInput.y + transform.right * moveInput.x;
        moveDir.Normalize();

        if (isSprinting)
        {
            speed = playerValues.moveSpeed * playerValues.sprintMultiplier;
        }
        else
        {
            speed = playerValues.moveSpeed;
        }
        Debug.Log("Speed: " + speed);
        Vector3 targetVelocity = moveDir * speed;
        Vector3 currentVelocity = rb.linearVelocity;

        Vector3 velocityChange = targetVelocity - new Vector3(currentVelocity.x, 0, currentVelocity.z);
        rb.AddForce(velocityChange, ForceMode.VelocityChange);
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

    private void CheckGround()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerValues.groundCheckDistance, playerValues.groundLayer);
        Debug.DrawLine(transform.position, transform.position + Vector3.down * playerValues.groundCheckDistance, isGrounded ? Color.green : Color.red);
        Debug.Log("Is Grounded: " + isGrounded);
        rso_PlayerGroundCheckValue.Set(isGrounded);
    }
}
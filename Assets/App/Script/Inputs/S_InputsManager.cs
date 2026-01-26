using UnityEngine;
using UnityEngine.InputSystem;

public class S_InputsManager : MonoBehaviour
{
    //[Header("Settings")]
    [Header("References")]
    [SerializeField] private PlayerInput playerInput;
    //[Header("Input")]
    [Header("Output")]
    [SerializeField] private RSE_OnPlayerInteract rse_OnPlayerInteract;
    [SerializeField] private RSE_OnPlayerJump rse_OnPlayerJump;
    [SerializeField] private RSE_OnPlayerLook rse_OnPlayerLook;
    [SerializeField] private RSE_OnPlayerMove rse_OnPlayerMove;
    [SerializeField] private RSE_OnPlayerSprint rse_OnPlayerSprint;

    private IA_PlayerInputs ia_PlayerInputs = null;

    private void Awake()
    {
        ia_PlayerInputs = new IA_PlayerInputs();
        playerInput.actions = ia_PlayerInputs.asset;
    }
    private void OnEnable()
    {
        var player = ia_PlayerInputs.Player;

        player.Move.performed += OnMoveInput;
        player.Move.canceled += OnMoveInput;
        player.Jump.performed += OnJumpInput;
        player.Interact.performed += OnInteractInput;
        player.Look.performed += OnLookInput;
        player.Look.canceled += OnLookInput;
        player.Sprint.performed += OnSprintInput;
        player.Sprint.canceled += OnSprintInput;
    }

    private void OnDisable()
    {
        var player = ia_PlayerInputs.Player;

        player.Move.performed -= OnMoveInput;
        player.Move.canceled -= OnMoveInput;
        player.Jump.performed -= OnJumpInput;
        player.Interact.performed -= OnInteractInput;
        player.Look.performed -= OnLookInput;
        player.Look.canceled -= OnLookInput;
        player.Sprint.performed -= OnSprintInput;
        player.Sprint.canceled -= OnSprintInput;
    }
    private void OnMoveInput(InputAction.CallbackContext ctx)
    {
        rse_OnPlayerMove.Call(ctx.ReadValue<Vector2>());
    }
    private void OnJumpInput(InputAction.CallbackContext ctx)
    {
        rse_OnPlayerJump.Call();
    }
    private void OnInteractInput(InputAction.CallbackContext ctx)
    {
        rse_OnPlayerInteract.Call();
    }
    private void OnLookInput(InputAction.CallbackContext ctx)
    {
        rse_OnPlayerLook.Call(ctx.ReadValue<Vector2>());
    }
    private void OnSprintInput(InputAction.CallbackContext ctx)
    {
        rse_OnPlayerSprint.Call(ctx.ReadValueAsButton());
    }
}
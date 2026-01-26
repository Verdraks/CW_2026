using UnityEngine;

[CreateAssetMenu(fileName = "SSO_PlayerValue", menuName = "SSO/Player/PlayerValue")]
public class SSO_PlayerValue : ScriptableObject
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float sprintMultiplier = 1.5f;
    public float acceleration = 12f;
    public float airControlMultiplier = 0.4f;
    public float groundFriction = 8f;

    [Header("Look")]
    public float mouseSensitivity = 1f;
    public float maxLookAngle = 80f;

    [Header("Jump")]
    public float jumpForce = 6f;
    public float coyoteTime = 0.15f;
    public float jumpBufferTime = 0.15f;

    [Header("FOV")]
    public float walkFOV = 60f;
    public float sprintFOV = 75f;
    public float fovSmooth = 8f;

    [Header("Headbob")]
    public float headbobFrequency = 8f;
    public float headbobAmplitude = 0.05f;
    public float headbobWalkingMultiplier = 1f;
    public float headbobSprintingMultiplier = 1.4f;
    public float headbobSmooth = 10f;

    [Header("Ground Check")]
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.35f;
}
using System;
using Unity.Cinemachine;
using UnityEngine;

public class S_PlayerCamera : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private SSO_PlayerValue playerValues;

    [Header("References")]
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private Transform cameraRoot;
    [SerializeField] private CinemachineCamera cameraCinemachine;

    [Header("Input")]
    [SerializeField] private RSE_OnPlayerLook rse_OnPlayerLook;
    [SerializeField] private RSO_PlayerGroundCheckValue rso_PlayerGroundCheckValue;
    [SerializeField] private RSO_PlayerSprintValue rso_PlayerSprintValue;
    [SerializeField] private RSO_PlayerMoveValue rso_PlayerMoveValue;

    private Vector2 lookInput;
    private float xRotation = 0f;
    private float headbobTimer = 0f;
    private Vector3 cameraRootInitialPos;
    private float speed = 0f;
    private float targetFOV = 0f;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cameraRootInitialPos = cameraRoot.localPosition;
    }
    private void OnEnable()
    {
        rse_OnPlayerLook.Action += LookInput;
    }
    private void OnDisable()
    {
        rse_OnPlayerLook.Action -= LookInput;
    }
    void Update()
    {
        Look();
        Headbob();
        HandleFOV();
    }

    private void Look()
    {
        Vector2 look = lookInput * playerValues.mouseSensitivity;
        xRotation -= look.y;
        xRotation = Mathf.Clamp(xRotation, -playerValues.maxLookAngle, playerValues.maxLookAngle);

        cameraTarget.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * look.x);
    }

    private void LookInput(Vector2 input)
    {
        lookInput = input;
    }

    private void Headbob()
    {
        if (!rso_PlayerGroundCheckValue.Get() || rso_PlayerMoveValue.Get().magnitude < 0.1f)
        {
            headbobTimer = 0f;
            cameraRoot.localPosition = Vector3.Lerp(cameraRoot.localPosition, cameraRootInitialPos, Time.deltaTime * playerValues.headbobSmooth);
            return;
        }

        if(rso_PlayerSprintValue.Get())
        {
            speed = playerValues.headbobSprintingMultiplier;
        }
        else
        {
            speed = playerValues.headbobWalkingMultiplier;
        }

        headbobTimer += Time.deltaTime * playerValues.headbobFrequency * speed;

        float headbobOffsetY = Mathf.Sin(headbobTimer) * playerValues.headbobAmplitude;

        Vector3 targetPos = cameraRootInitialPos + Vector3.up * headbobOffsetY;

        cameraRoot.localPosition = Vector3.Lerp(cameraRoot.localPosition, targetPos, Time.deltaTime * playerValues.headbobSmooth);
    }

    private void HandleFOV()
    {
        if(rso_PlayerSprintValue.Get())
        {
            targetFOV = playerValues.sprintFOV;
        }
        else
        {
            targetFOV = playerValues.walkFOV;
        }

        cameraCinemachine.Lens.FieldOfView = Mathf.Lerp(cameraCinemachine.Lens.FieldOfView, targetFOV, Time.deltaTime * playerValues.fovSmooth);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private Camera cam;

    public Action<InputAction.CallbackContext> customInteractionEvent;

    public Vector2 RawMovementInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool GlideInput { get; private set; }

    [SerializeField]
    private float jumpInputHoldTime = .2f;

    public float glideInputHoldTime = 2f;

    private float jumpInputStartTime;
    private float glideInputStartTime;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        cam = Camera.main;
        customInteractionEvent = DummyFunction;

    }

    private void Update()
    {
        CheckJumpInputHoldTime();
        CheckGlideInputHoldTime();
    }

    private void DummyFunction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("bye champ");
        }
    }

    public void OnInteractionInput(InputAction.CallbackContext context)
    {
        customInteractionEvent(context);
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        if (Mathf.Abs(RawMovementInput.x) > 0.5f)
        {
            NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        }
        else
        {
            NormInputX = 0;
        }
        if (Mathf.Abs(RawMovementInput.y) > 0.5f)
        {
            NormInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
        }
        else
        {
            NormInputY = 0;
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            JumpInputStop = false;
            jumpInputStartTime = Time.time;

        }

        if (context.canceled)
        {
            JumpInputStop = true;
        }
    }

    public void OnGlideInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            glideInputStartTime = Time.time;
            GlideInput = true;

        }

        if (context.canceled)
        {
            GlideInput = false;
        }
    }

    public void UseJumpInput() => JumpInput = false;

    public void CheckJumpInputHoldTime()
    {
        if (Time.time >= jumpInputStartTime + jumpInputHoldTime)
        {
            JumpInput = false;
        }
    }
    public void CheckGlideInputHoldTime()
    {
        if (Time.time >= glideInputStartTime + glideInputHoldTime)
        {
            GlideInput = false;
        }
    }

    public void OnEscapeInput()
    {
        Application.Quit();
    }
}

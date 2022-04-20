using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "playerData", menuName = "Data/Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10f;

    [Header("Jump State")]
    public float jumpVelocity = 15f;
    public int amountsOfJumps = 1;

    [Header("Glide State")]
    public float glideVelocity = -1f;
    public int amountsOfGlides = 1;
    public float glideParticleDuration = .5f;

    [Header("In Air State")]
    public float coyoteTime = .2f;
    public float variableJumpHeightMultiplier = .5f;

    [Header("Crouch States")]
    public float crouchMovementVelocity = 5f;
    public float crouchColliderHeight = .8f;
    public float standColliderHeight = 1.6f;

    [Header("Check Variables")]
    public float groundCheckRadius = .3f;
    public LayerMask whatIsGround;
}


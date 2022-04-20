using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGlideState : PlayerAbilityState
{
    private int xInput;

    private int amountOfGlidesLeft;

    private bool glideInput;
    private bool isGrounded;

    public PlayerGlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        amountOfGlidesLeft = playerData.amountsOfGlides;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(xInput, playerData.glideVelocity);
        amountOfGlidesLeft--;
        
        player.GliderParticle.Play();
    }

    public override void Exit()
    {
        base.Exit();

        player.GliderParticle.Stop();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        glideInput = player.InputHandler.GlideInput;
        xInput = player.InputHandler.NormInputX;
        isGrounded = player.CheckIfGrounded();

        player.CheckIfShouldFlip(xInput);
        player.SetVelocity(playerData.movementVelocity * xInput, playerData.glideVelocity);

        if (isGrounded || !glideInput)
        {
            isAbilityDone = true;
        }
    }

    public bool CanGlide()
    {
        if (amountOfGlidesLeft > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetAmountOfGlidesLeft() => amountOfGlidesLeft = playerData.amountsOfGlides;
}

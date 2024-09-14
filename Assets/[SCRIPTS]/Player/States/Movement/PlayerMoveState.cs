using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    private float slopeCheckDistance = 0.5f;  // Distance for slope detection
    private float maxSlopeAngle = 45f;        // Maximum walkable slope angle
    private bool isOnSlope;
    private float slopeAngle;
    private Vector2 slopeNormal;

    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(13, null);
    }

    public override void Exit()
    {
        base.Exit();
        AudioManager.instance.StopSFX(13);
    }

    public override void Update()
    {
        base.Update();

        HandlePlayerFlip();

        SlopeCheck();

        if (isOnSlope)
        {
            MoveOnSlope();
        }
        else
        {
            player.rb.velocity = new Vector2(xInput * player.moveSpeed, player.rb.velocity.y);
        }

        if (xInput == 0 || player.IsWallDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    private void HandlePlayerFlip()
    {
        if (xInput > 0 && !player.facingRight)
        {
            player.Flip();
        }
        else if (xInput < 0 && player.facingRight)
        {
            player.Flip();
        }
    }

    // Detect slope beneath player and check if it can be walked on
    private void SlopeCheck()
    {
        Vector2 checkPos = new Vector2(player.transform.position.x, player.transform.position.y - player.GetColliderHeight() / 2);
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance, player.groundMask);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos - new Vector2(0.1f, 0), Vector2.down, slopeCheckDistance, player.groundMask);

        if (slopeHitFront)
        {
            slopeAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
            isOnSlope = slopeAngle > 0 && slopeAngle <= maxSlopeAngle;
            slopeNormal = slopeHitFront.normal;
        }
        else if (slopeHitBack)
        {
            slopeAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
            isOnSlope = slopeAngle > 0 && slopeAngle <= maxSlopeAngle;
            slopeNormal = slopeHitBack.normal;
        }
        else
        {
            isOnSlope = false;
        }
    }

    private void MoveOnSlope()
    {
        float moveSpeed = player.moveSpeed;
        Vector2 slopeDirection = new Vector2(slopeNormal.y, -slopeNormal.x).normalized;

        // Calculate the horizontal movement based on input and slope direction
        float targetSpeed = xInput * moveSpeed;
        Vector2 targetVelocity = slopeDirection * targetSpeed;

        // Set the velocity, ensuring that the vertical component is adjusted properly
        player.rb.velocity = new Vector2(targetVelocity.x, player.rb.velocity.y);

        if (slopeAngle > 0)
        {
            player.rb.velocity = new Vector2(player.rb.velocity.x, Mathf.Max(player.rb.velocity.y, 0));
        }
    }
}

using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        // Check if the player presses jump to initiate wall jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.wallJumpState);
            return;
        }

        // Stop sliding if player moves in the opposite direction of the wall
        if (xInput != 0 && player.facingDir != xInput)
        {
            stateMachine.ChangeState(player.idleState);
            return; // Avoid further execution in the update loop
        }

        // Slide slower if holding down, otherwise normal slide
        if (yInput < 0)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y * .7f);
        }

        // If the player reaches the ground, switch to idle state
        if (player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }

        // Check if the player is no longer on the wall (i.e., edge of wall reached)
        if (!player.IsWallDetected())
        {
            stateMachine.ChangeState(player.airState);  // Transition to air state only once
        }
    }
}

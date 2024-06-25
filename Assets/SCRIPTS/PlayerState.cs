using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;

    private string animBoolName;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        Debug.Log("Enter " + animBoolName);
    }

    public virtual void Update()
    {
        Debug.Log("In " + animBoolName);
    }

    public virtual void Exit()
    {
        Debug.Log("Exit " + animBoolName);
    }
}

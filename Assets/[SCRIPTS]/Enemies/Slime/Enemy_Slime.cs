using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy_Slime : Enemy
{


    #region States

    public Slime_IdleState idleState { get; private set; }
    public Slime_MoveState moveState { get; private set; }
    public Slime_BattleState battleState { get; private set; }
    public Slime_AttackState attackState { get; private set; }
    public Slime_StunnedState stunnedState { get; private set; }
    public Slime_DeadState deathState { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();

        SetupDefaultFacingDir(-1);

        idleState = new Slime_IdleState(this, stateMachine, "Idle", this);
        moveState = new Slime_MoveState(this, stateMachine, "Move", this);
        battleState = new Slime_BattleState(this, stateMachine, "Move", this);
        attackState = new Slime_AttackState(this, stateMachine, "Attack", this);
        stunnedState = new Slime_StunnedState(this, stateMachine, "Stunned", this);
        deathState = new Slime_DeadState(this, stateMachine, "Idle", this);
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            stateMachine.ChangeState(stunnedState);
            return true;
        }

        return false;
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deathState);
    }
}

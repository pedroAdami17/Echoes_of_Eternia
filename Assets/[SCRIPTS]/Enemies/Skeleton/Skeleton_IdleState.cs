using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_IdleState : Skeleton_GroundedState
{
    public Skeleton_IdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton enemy) : base(_enemyBase, _stateMachine, _animBoolName, enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.idleTime;
    }

    public override void Exit()
    {
        base.Exit();

        AudioManager.instance.PlaySFX(22, enemy.transform);
    }

    public override void Update()
    {
        base.Update();

        if(stateTimer < 0f)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }
}

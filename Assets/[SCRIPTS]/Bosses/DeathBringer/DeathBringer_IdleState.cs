using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBringer_IdleState : EnemyState
{
    private Boss_DeathBringer enemy;

    public DeathBringer_IdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Boss_DeathBringer _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
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

        if (stateTimer < 0f)
        {
            stateMachine.ChangeState(enemy.teleportState);
        }
    }
}

using System.Diagnostics;
using UnityEngine;

public class DeathBringer_TeleportState : EnemyState
{
    private Boss_DeathBringer enemy;

    public DeathBringer_TeleportState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Boss_DeathBringer _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        UnityEngine.Debug.Log("Teleport state triggered");
        enemy.stats.MakeInvincible(true);
        //enemy.FindPosition();
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            if (enemy.CanDoSpellCast())
                stateMachine.ChangeState(enemy.spellCastState);
            else
                stateMachine.ChangeState(enemy.battleState);

        }
    }

    public override void Exit()
    {
        base.Exit();

        enemy.stats.MakeInvincible(false);
    }
}

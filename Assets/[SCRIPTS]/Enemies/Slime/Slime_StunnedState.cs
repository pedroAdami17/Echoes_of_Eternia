using UnityEngine;

public class Slime_StunnedState : EnemyState
{
    private Enemy_Slime enemy;
    public Slime_StunnedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Slime _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.fx.InvokeRepeating("RedColorBlink", 0, .1f);

        stateTimer = enemy.stunDuration;

        rb.velocity = new Vector2(-enemy.facingDir * enemy.stunDirection.x, enemy.stunDirection.y);
    }

    public override void Exit()
    {
        base.Exit();

        enemy.fx.Invoke("CancelColorChange", 0);
        enemy.stats.MakeInvincible(false);
    }

    public override void Update()
    {
        base.Update();

        if (rb.velocity.y < .1f && enemy.IsGroundDetected())
        {
            enemy.fx.Invoke("CancelColorChange", 0);
            enemy.anim.SetTrigger("StunFold");
            enemy.stats.MakeInvincible(true);
        } 

        if (stateTimer < 0)
            stateMachine.ChangeState(enemy.idleState);
    }
}

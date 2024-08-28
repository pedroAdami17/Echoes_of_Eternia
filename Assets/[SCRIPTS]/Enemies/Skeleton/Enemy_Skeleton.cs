public class Enemy_Skeleton : Enemy
{

    #region States

    public Skeleton_IdleState idleState { get; private set; }
    public Skeleton_MoveState moveState { get; private set; }
    public Skeleton_BattleState battleState { get; private set; }
    public Skeleton_AttackState attackState { get; private set; }
    public Skeleton_StunnedState stunnedState { get; private set; }
    public Skeleton_DeathState deathState { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();

        idleState = new Skeleton_IdleState(this, stateMachine, "Idle", this);
        moveState = new Skeleton_MoveState(this, stateMachine, "Move", this);
        battleState = new Skeleton_BattleState(this, stateMachine, "Move", this);
        attackState = new Skeleton_AttackState(this, stateMachine, "Attack", this);
        stunnedState = new Skeleton_StunnedState(this, stateMachine, "Stunned", this);
        deathState = new Skeleton_DeathState(this, stateMachine, "Idle", this);
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
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

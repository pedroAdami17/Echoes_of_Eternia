using UnityEngine;

public class Enemy_Archer : Enemy
{
    [Header("Archer Specific Info")]
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private float arrowSpeed;
    [SerializeField] private float arrowDamage;

    public Vector2 jumpVelocity;
    public float jumpCooldown;
    public float safeDistance;
    [HideInInspector] public float lastTimeJumped;

    [Header("Additional collision Check")]
    [SerializeField] private Transform groundBehindCheck;
    [SerializeField] private Vector2 groundBehindCheckSize;

    #region States

    public Archer_IdleState idleState { get; private set; }
    public Archer_MoveState moveState { get; private set; }
    public Archer_BattleState battleState { get; private set; }
    public Archer_AttackState attackState { get; private set; }
    public Archer_StunnedState stunnedState { get; private set; }
    public Archer_DeathState deathState { get; private set; }
    public Archer_JumpState jumpState { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();

        idleState = new Archer_IdleState(this, stateMachine, "Idle", this);
        moveState = new Archer_MoveState(this, stateMachine, "Move", this);
        battleState = new Archer_BattleState(this, stateMachine, "Idle", this);
        attackState = new Archer_AttackState(this, stateMachine, "Attack", this);
        stunnedState = new Archer_StunnedState(this, stateMachine, "Stunned", this);
        deathState = new Archer_DeathState(this, stateMachine, "Move", this);
        jumpState = new Archer_JumpState(this, stateMachine, "Jump", this);
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

    public override void SpecialAttackTrigger()
    {
        GameObject newArrow = Instantiate(arrowPrefab, attackCheck.position, Quaternion.identity);

        newArrow.GetComponent<ArrowController>().SetupArrow(arrowSpeed * facingDir, stats);
    }

    public bool GroundBehindCheck() => Physics2D.BoxCast(groundBehindCheck.position, groundBehindCheckSize, 0, Vector2.zero, groundMask);
    public bool WallBehind() => Physics2D.Raycast(wallCheck.position, Vector3.right * -facingDir, wallCheckDistance + 2, groundMask);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireCube(groundBehindCheck.position, groundBehindCheckSize);
    }
}
using UnityEngine;

public enum SlimeType { big, medium, small }

public class Enemy_Slime : Enemy
{
    [Header("Slime specific")]
    [SerializeField] private SlimeType slimeType;
    [SerializeField] private int slimesToCreate;
    [SerializeField] private GameObject slimePrefab;
    [SerializeField] private Vector2 minCreateVelocity;
    [SerializeField] private Vector2 maxCreateVelocity;

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

        if (slimeType == SlimeType.small)
            return;

        CreateSlimes(slimesToCreate, slimePrefab);
    }

    private void CreateSlimes(int _amountOfSlimes, GameObject _slimePrefab)
    {
        for (int i = 0; i < _amountOfSlimes; i++)
        {
            GameObject newSlime = Instantiate(_slimePrefab, transform.position, Quaternion.identity);

            newSlime.GetComponent<Enemy_Slime>().SetupSlime(facingDir);
        }
    }

    public void SetupSlime(int _facingDir)
    {
        if (_facingDir != facingDir)
            Flip();

        float xVel = Random.Range(minCreateVelocity.x, maxCreateVelocity.x);
        float yVel = Random.Range(minCreateVelocity.y, maxCreateVelocity.y);

        isKnocked = true;

        GetComponent<Rigidbody2D>().velocity = new Vector2(xVel * -facingDir, yVel);

        Invoke("CancelKnockback", 1.5f);
    }

    private void CancelKnockback() => isKnocked = false;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_DeathBringer : Enemy
{
    [Header("Teleport details")]
    [SerializeField] private BoxCollider2D arena;
    [SerializeField] private Vector2 surroundingCheckSize;
    public float chanceToTeleport;
    public float defaultChanceToTeleport;

    #region States

    public DeathBringer_BattleState battleState {  get; private set; }
    public DeathBringer_AttackState attackState { get; private set; }
    public DeathBringer_IdleState idleState { get; private set; }
    public DeathBringer_SpellCastState spellCastState { get; private set; }
    public DeathBringer_TeleportState teleportState { get; private set; }
    public DeathBringer_DeathState deathState { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();

        SetupDefaultFacingDir(-1);

        idleState = new DeathBringer_IdleState(this, stateMachine, "Idle", this);
        attackState = new DeathBringer_AttackState(this, stateMachine, "Attack", this);
        battleState = new DeathBringer_BattleState(this, stateMachine, "Move", this);
        deathState = new DeathBringer_DeathState(this, stateMachine, "Idle", this);
        teleportState = new DeathBringer_TeleportState(this, stateMachine, "Teleport", this);
        spellCastState = new DeathBringer_SpellCastState(this, stateMachine, "SpellCast", this);
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deathState);
    }

    public void FindPosition()
    {
        float x = Random.Range(arena.bounds.min.x + 3, arena.bounds.max.x - 3);
        float y = Random.Range(arena.bounds.min.y + 3, arena.bounds.max.y - 3);

        transform.position = new Vector3(x, y);
        transform.position = new Vector3(transform.position.x, transform.position.y - GroundBelow().distance + (cd.size.y / 2));

        if(!GroundBelow() || SomethingIsAround())
        {
            FindPosition();
        }
    }

    private RaycastHit2D GroundBelow() => Physics2D.Raycast(transform.position, Vector2.down, 100, groundMask);
    private bool SomethingIsAround() => Physics2D.BoxCast(transform.position, surroundingCheckSize, 0, Vector2.zero, 0, groundMask);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - GroundBelow().distance));
        Gizmos.DrawWireCube(transform.position, surroundingCheckSize);
    }

    public bool CanTeleport()
    {
        if(Random.Range(0,100) <= chanceToTeleport)
            return true;


        return false;
    }
}

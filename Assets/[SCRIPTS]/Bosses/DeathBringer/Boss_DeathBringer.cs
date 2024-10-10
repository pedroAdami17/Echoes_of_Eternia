using UnityEngine;

public class Boss_DeathBringer : Enemy
{

    #region States

    public DeathBringer_BattleState battleState { get; private set; }
    public DeathBringer_AttackState attackState { get; private set; }
    public DeathBringer_IdleState idleState { get; private set; }
    public DeathBringer_SpellCastState spellCastState { get; private set; }
    public DeathBringer_TeleportState teleportState { get; private set; }
    public DeathBringer_DeathState deathState { get; private set; }

    #endregion

    [Space]
    public bool bossFightBegun;

    [Header("Spell cast details")]
    [SerializeField] private GameObject spellPrefab;
    public int amountOfSpells;
    public float spellCooldown;
    public float lastTimeCast;
    [SerializeField] private float spellStateCooldown;
    [SerializeField] private Vector2 spellOffset;

    [Header("Teleport details")]
    [SerializeField] private BoxCollider2D arena;
    [SerializeField] private Vector2 surroundingCheckSize;
    public float chanceToTeleport;
    public float defaultChanceToTeleport = 25;

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

    public void CastSpell()
    {
        Player player = PlayerManager.instance.player;


        float xOffset = 0;

        if (player.rb.velocity.x != 0)
            xOffset = player.facingDir * spellOffset.x;

        Vector3 spellPosition = new Vector3(player.transform.position.x + xOffset, player.transform.position.y + spellOffset.y);

        GameObject newSpell = Instantiate(spellPrefab, spellPosition, Quaternion.identity);
        newSpell.GetComponent<DeathBringerSpell_Controller>().SetupSpell(stats);
    }

    public void FindPosition()
    {
        // Get a random position within the arena
        float x = Random.Range(arena.bounds.min.x + 3, arena.bounds.max.x - 3);
        float y = Random.Range(arena.bounds.min.y + 3, arena.bounds.max.y - 3);

        // Log the attempted position
        Debug.Log($"Attempting teleport to: ({x}, {y})");

        // Adjust the boss's position based on the ground below
        transform.position = new Vector3(x, y);
        RaycastHit2D groundHit = GroundBelow();

        if (groundHit.collider != null)
        {
            transform.position = new Vector3(transform.position.x, groundHit.point.y + (cd.size.y / 2));
            Debug.Log($"Teleport successful to: {transform.position}");
        }
        else
        {
            Debug.Log("No ground found. Trying another position.");
        }

        // If the ground or surroundings are invalid, try again
        if (!groundHit || SomethingIsAround())
        {
            int maxAttempts = 5;
            for (int i = 0; i < maxAttempts; i++)
            {
                x = Random.Range(arena.bounds.min.x + 3, arena.bounds.max.x - 3);
                y = Random.Range(arena.bounds.min.y + 3, arena.bounds.max.y - 3);
                transform.position = new Vector3(x, y);
                groundHit = GroundBelow();

                if (groundHit.collider != null && !SomethingIsAround())
                {
                    transform.position = new Vector3(transform.position.x, groundHit.point.y + (cd.size.y / 2));
                    Debug.Log($"Teleport successful on attempt {i + 1} to: {transform.position}");
                    break; // Valid position found, exit loop
                }
                else
                {
                    Debug.Log($"Attempt {i + 1} failed.");
                }
            }
        }
    }


    private RaycastHit2D GroundBelow()
    {
        Physics2D.Raycast(transform.position, Vector2.down, 100, groundMask);
        return Physics2D.Raycast(transform.position, Vector2.down, 100, groundMask);
    }
    private bool SomethingIsAround()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, surroundingCheckSize, 0, Vector2.zero, 0, groundMask);
        return hit.collider != null; 
    }


    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - GroundBelow().distance));
        Gizmos.DrawWireCube(transform.position, surroundingCheckSize);
    }


    public bool CanTeleport()
    {
        if (Random.Range(0, 100) <= chanceToTeleport)
        {
            chanceToTeleport = defaultChanceToTeleport;
            return true;
        }


        return false;
    }

    public bool CanDoSpellCast()
    {
        if (Time.time >= lastTimeCast + spellStateCooldown)
        {
            return true;
        }

        return false;
    }
}

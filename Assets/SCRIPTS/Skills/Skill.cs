using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] protected float cooldown;
    protected float cooldownTimer;

    protected Player player;
    [SerializeField] private float detectionRadius = 25f; // Add this field for visualization

    protected virtual void Start()
    {
        player = PlayerManager.instance.player;
    }

    protected virtual void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }

    public virtual bool CanUseSkill()
    {
        if (cooldownTimer < 0)
        {
            UseSkill();
            cooldownTimer = cooldown;
            return true;
        }

        return false;
    }

    public virtual void UseSkill()
    {

    }

    protected virtual Transform FindClosestEnemy(Transform _checkTransform)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_checkTransform.position, detectionRadius);
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                float distanceToEnemy = Vector2.Distance(_checkTransform.position, hit.transform.position);
                Debug.Log($"Enemy found: {hit.transform.name}, Distance: {distanceToEnemy}"); // Debugging line

                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = hit.transform;
                }
            }
        }

        if (closestEnemy != null)
        {
            Debug.Log($"Closest enemy: {closestEnemy.name}, Distance: {closestDistance}"); // Debugging line
        }
        else
        {
            Debug.Log("No enemies found within range."); // Debugging line
        }

        return closestEnemy;
    }

    private void OnDrawGizmos()
    {
        // Draw a sphere gizmo at the position of the object with the detection radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}

using System.Collections;
using System.Xml.Serialization;
using UnityEngine;

public class Clone_Skill : Skill
{
    [Header("Clone info")]
    [SerializeField] private float attackMultiplier;
    [SerializeField] private GameObject clonePrefab;
    [SerializeField] private float cloneDuration;
    [Space]

    [Header("Clone Attack")]
    [SerializeField] private UI_SkillTreeSlots cloneAttackUnlockButton;
    [SerializeField] private float cloneAttackMultiplier;
    [SerializeField] private bool canAttack;

    [Header("Agressive Clone")]
    [SerializeField] private UI_SkillTreeSlots agressiveCloneUnlockButton;
    [SerializeField] private float aggressiveCloneAttackMultiplier;
    public bool canApplyOnHitEffect { get; private set; }

    [Header("Multiple Clone")]
    [SerializeField] private UI_SkillTreeSlots multipleCloneUnlockButton;
    [SerializeField] private float multipleCloneAttackMultiplier;
    [SerializeField] private bool canDuplicateClone;
    [SerializeField] private float chanceToDuplicate;

    [Header("Crystal instead of clone")]
    [SerializeField] private UI_SkillTreeSlots crystalInsteadUnlockButton;
    public bool crystalInsteadOfClone;

    protected override void Start()
    {
        base.Start();

        cloneAttackUnlockButton.OnSkillUnlocked += UnlockCloneAttack;
        agressiveCloneUnlockButton.OnSkillUnlocked += UnlockAggressiveClone;
        multipleCloneUnlockButton.OnSkillUnlocked += UnlockMultiClone;
        crystalInsteadUnlockButton.OnSkillUnlocked += UnlockCrystalInstead;
    }

    #region Unlock Skills

    private void UnlockCloneAttack()
    {
        if(cloneAttackUnlockButton.unlocked)
        {
            canAttack = true;
            attackMultiplier = cloneAttackMultiplier;
        }
    }

    private void UnlockAggressiveClone()
    {
        if(agressiveCloneUnlockButton.unlocked)
        {
            canApplyOnHitEffect = true;
            attackMultiplier = aggressiveCloneAttackMultiplier;
        }
    }

    private void UnlockMultiClone()
    {
        if(multipleCloneUnlockButton.unlocked)
        {
            canDuplicateClone = true;
            attackMultiplier = cloneAttackMultiplier;
        }
    }

    private void UnlockCrystalInstead()
    {
        if(crystalInsteadUnlockButton.unlocked)
        {
            crystalInsteadOfClone = true;
        }
    }

    #endregion


    public void CreateClone(Transform _clonePosition, Vector3 _offset)
    {
        if (crystalInsteadOfClone)
        {
            SkillManager.instance.crystal.CreateCrystal();
            return;
        }

        GameObject newClone = Instantiate(clonePrefab, _clonePosition.position + _offset, Quaternion.identity);
        Clone_Skill_Controller cloneController = newClone.GetComponent<Clone_Skill_Controller>();

        // Ensure the clone checks for enemies around itself
        Transform closestEnemy = FindClosestEnemy(newClone.transform);
        cloneController.SetupClone(newClone.transform, cloneDuration, canAttack, _offset, closestEnemy, canDuplicateClone, chanceToDuplicate, player, attackMultiplier);
    }



    public void CreateCloneWithDelay(Transform _enemyTransform)
    {
        StartCoroutine(CloneDelayCorotine(_enemyTransform, new Vector3(.7f * player.facingDir, 0)));
    }

    private IEnumerator CloneDelayCorotine(Transform _transform, Vector3 _offset)
    {
        yield return new WaitForSeconds(.4f);
        CreateClone(_transform, _offset);
    }

    protected override Transform FindClosestEnemy(Transform _checkTransform)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_checkTransform.position, 25);
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                float distanceToEnemy = Vector2.Distance(_checkTransform.position, hit.transform.position);
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = hit.transform;
                }
            }
        }

        return closestEnemy;
    }
}

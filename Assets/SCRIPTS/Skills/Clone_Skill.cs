using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone_Skill : Skill
{
    [Header("Clone info")]
    [SerializeField] private GameObject clonePrefab;
    [SerializeField] private float cloneDuration;
    [Space]
    [SerializeField] private bool canAttack;

    [SerializeField] private bool canCreateCloneOnDashStart;
    [SerializeField] private bool canCreateCloneOnDashEnd;
    [SerializeField] private bool canCreateCloneOnCounterAttack;
    [Header("Clone can duplicate")]
    [SerializeField] private bool canDuplicateClone;
    [SerializeField] private float chanceToDuplicate;
    [Header("Crystal instead of clone")]
    public bool crystalInseadOfClone;


    public void CreateClone(Transform _clonePosition, Vector3 _offset)
    {
        GameObject newClone = Instantiate(clonePrefab, _clonePosition.position + _offset, Quaternion.identity);
        Clone_Skill_Controller cloneController = newClone.GetComponent<Clone_Skill_Controller>();

        // Ensure the clone checks for enemies around itself
        Transform closestEnemy = FindClosestEnemy(newClone.transform);
        cloneController.SetupClone(newClone.transform, cloneDuration, canAttack, _offset, closestEnemy);
    }

    public void CreateCloneOnDashStart()
    {
        if (canCreateCloneOnDashStart)
            CreateClone(player.transform, Vector3.zero);
    }
    
    public void CreateCloneOnDashEnd()
    {
        if (canCreateCloneOnDashEnd)
            CreateClone(player.transform, Vector3.zero);
    }

    public void CreateCloneOnCounterAttack(Transform _enemyTransform)
    {
        if (canCreateCloneOnCounterAttack)
            StartCoroutine(CreateCloneWithDelay(_enemyTransform, new Vector3(2 * player.facingDir, 0)));
            
    }

    private IEnumerator CreateCloneWithDelay(Transform _transform, Vector3 _offset)
    {
        yield return new WaitForSeconds(.4f);
        CreateClone(_transform, _offset);
    }
}

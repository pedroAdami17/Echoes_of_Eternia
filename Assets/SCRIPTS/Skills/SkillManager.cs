using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;


    public DashSkill dash { get; private set; }
    public Clone_Skill clone { get; private set; }
    public Sword_Skill sword { get; private set; }

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        dash = GetComponent<DashSkill>();
        clone = GetComponent<Clone_Skill>();
        sword = GetComponent<Sword_Skill>();
    }
}

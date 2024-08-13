using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashSkill : Skill
{
    [Header("Dash")]
    [SerializeField] private UI_SkillTreeSlots dashUnlockButton;
    public bool dashUnlocked;  //{ get; private set; }

    [Header("Clone on dash")]
    [SerializeField] private UI_SkillTreeSlots cloneOnDashUnlockButton;
    public bool cloneOnDashUnlocked; //{ get; private set; }

    [Header("Clone on arrival")]
    [SerializeField] private UI_SkillTreeSlots cloneOnArrivalUnlockButton;
    public bool cloneOnArrivalUnlocked { get; private set; }



    public override void UseSkill()
    {
        base.UseSkill();
    }

    protected override void Start()
    {
        base.Start();

        dashUnlockButton.OnSkillUnlocked += UnlockDash;
        cloneOnDashUnlockButton.OnSkillUnlocked += UnlockCloneOnDash;
        cloneOnArrivalUnlockButton.OnSkillUnlocked += UnlockCloneOnArrival;
    }


    public void UnlockDash()
    {
        Debug.Log("Attempting to unlock Dash. Current state: " + dashUnlockButton.unlocked);
        if (dashUnlockButton.unlocked)
            dashUnlocked = true;
        Debug.Log("DashUnlocked state: " + dashUnlocked);
    }

    private void UnlockCloneOnDash()
    {
        if (cloneOnDashUnlockButton.unlocked)
            cloneOnDashUnlocked = true;
    }

    private void UnlockCloneOnArrival()
    {
        if (cloneOnArrivalUnlockButton.unlocked)
            cloneOnArrivalUnlocked = true;
    }


    public void CloneOnDash()
    {
        if (cloneOnDashUnlocked)
            SkillManager.instance.clone.CreateClone(player.transform, Vector3.zero);
    }

    public void CloneOnArrival()
    {
        if (cloneOnArrivalUnlocked)
            SkillManager.instance.clone.CreateClone(player.transform, Vector3.zero);
    }
}

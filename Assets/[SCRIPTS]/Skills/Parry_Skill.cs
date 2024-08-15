using UnityEngine;
using UnityEngine.UI;

public class Parry_Skill : Skill
{
    [Header("Parry")]
    [SerializeField] private UI_SkillTreeSlots parryUnlockButton;
    public bool parryUnlocked;

    [Header("Parry Restore")]
    [SerializeField] private UI_SkillTreeSlots restoreUnlockedButton;
    public bool restoreUnlocked;
    [Range(0f, 1f)]
    [SerializeField] private float restoreHealthPercentage;

    [Header("Parry With Mirage")]
    [SerializeField] private UI_SkillTreeSlots parryWithMirageButton;
    public bool parryWithMirageUnlocked;


    public override void UseSkill()
    {
        base.UseSkill();

        if (restoreUnlocked)
        {
            int restoreAmount = Mathf.RoundToInt(player.stats.GetMaxHealthValue() * restoreHealthPercentage);
            player.stats.IncreaseHealthBy(restoreAmount);
        }
    }
    protected override void Start()
    {
        base.Start();

        parryUnlockButton.OnSkillUnlocked += UnlockParry;
        restoreUnlockedButton.OnSkillUnlocked += UnlockParryRestore;
        parryWithMirageButton.OnSkillUnlocked += UnlockParryMirage;
    }

    protected override void CheckUnlock()
    {
        UnlockParry();
        UnlockParryRestore();
        UnlockParryMirage();
    }

    private void UnlockParry()
    {
        if (parryUnlockButton.unlocked)
            parryUnlocked = true;
    }

    private void UnlockParryRestore()
    {
        if (restoreUnlockedButton.unlocked)
            restoreUnlocked = true;
    }

    private void UnlockParryMirage()
    {
        if (parryWithMirageButton.unlocked)
            parryWithMirageUnlocked = true;
    }

    public void MakeMirrageOnParry(Transform _transform)
    {
        if (parryWithMirageUnlocked)
            SkillManager.instance.clone.CreateCloneWithDelay(_transform);
    }
}

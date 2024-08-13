using UnityEngine;

public class Dodge_Skill : Skill
{
    [Header("Dodge")]
    [SerializeField] UI_SkillTreeSlots unlockDodgeButton;
    [SerializeField] private int evasionAmount;
    public bool dodgeUnlocked;

    [Header("Mirage dodge")]
    [SerializeField] private UI_SkillTreeSlots unlockMirageButton;
    public bool dodgeMirageUnlocked;

    protected override void Start()
    {
        base.Start();

        unlockDodgeButton.OnSkillUnlocked += UnlockDodge;
        unlockMirageButton.OnSkillUnlocked += UnlockDodgeMirage;
    }

    private void UnlockDodge()
    {
        if (unlockDodgeButton.unlocked)
        {
            player.stats.evasion.AddModifier(evasionAmount);
            Inventory.instance.UpdateStatsUI();
            dodgeUnlocked = true;
        }
    }

    private void UnlockDodgeMirage()
    {
        if (unlockMirageButton.unlocked)
            dodgeMirageUnlocked = true;
    }

    public void CreateMirageOnDodge()
    {
        if (dodgeMirageUnlocked)
            SkillManager.instance.clone.CreateClone(player.transform, new Vector3(1.5f * player.facingDir, 0));
    }
}

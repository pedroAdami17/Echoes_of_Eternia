using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_DeathBringerTriggers : Enemy_AnimationTriggers
{
    private Boss_DeathBringer deathBringer => GetComponentInParent<Boss_DeathBringer>();

    private void Relocate() => deathBringer.FindPosition();

    private void MakeInvisible() => deathBringer.fx.MakeTransparent(true);
    private void MakeVisible() => deathBringer?.fx.MakeTransparent(false);
}

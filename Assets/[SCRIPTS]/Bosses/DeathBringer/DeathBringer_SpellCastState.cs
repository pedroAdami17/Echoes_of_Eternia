using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBringer_SpellCastState : EnemyState
{
    private Boss_DeathBringer enemy;

    public DeathBringer_SpellCastState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Boss_DeathBringer _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }
}

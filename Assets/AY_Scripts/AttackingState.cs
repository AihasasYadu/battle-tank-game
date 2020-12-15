using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingState : NpcStates
{
    private bool reloading = false;
    public override NpcStates Enter(EnemyController e)
    {
        Initialize(e);
        return LogicUpdate();
    }

    protected override void Initialize(EnemyController e)
    {
        base.Initialize(e);
    }

    protected override NpcStates LogicUpdate()
    {
        LookAtPlayer();
        if (!reloading)
            StartCoroutine(DelayedAttack());

        if (enemy.playerTransform)
            return enemy.attacking;

        return enemy.patrolling;
    }

    private void AttackPlayer()
    {
        Debug.Log("In Attack");
        ShellService.Instance.SetTankTransform = enemy.shellPosition.transform;
        ShellService.Instance.GetShell(enemy.damage, enemy.speed, TankSides.Enemy);
    }

    private void LookAtPlayer()
    {
        Debug.Log("In Look");
        enemy.turret.transform.LookAt(enemy.playerTransform);
    }

    private IEnumerator DelayedAttack()
    {
        Debug.Log("In Delay");
        reloading = true;
        AttackPlayer();
        yield return new WaitForSeconds(1f);
        reloading = false;
    }
}

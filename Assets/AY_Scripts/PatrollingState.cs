using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingState : NpcStates
{
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
        Movement();
        NameLookAtCamera();
        if (enemy.playerTransform)
            return enemy.attacking;

        return enemy.patrolling;
    }

    private void Movement()
    {
        //enemy.rb.MovePosition(enemy.transform.position + enemy.newPosition * Time.deltaTime);
        Turn();
    }

    private void Turn()
    {
        enemy.transform.rotation = Quaternion.LookRotation(enemy.newPosition, Vector3.up);
        enemy.turret.transform.rotation = Quaternion.LookRotation(enemy.newPosition, Vector3.up);
    }

    private void NameLookAtCamera()
    {
        enemy.floatingName.transform.LookAt(Camera.main.transform.position);
        enemy.floatingName.transform.Rotate(0, 180, 0);
    }
}

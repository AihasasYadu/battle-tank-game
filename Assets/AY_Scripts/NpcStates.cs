using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NpcStates : MonoBehaviour
{
    protected EnemyController enemy;
    public virtual NpcStates Enter(EnemyController e) { return this; }
    protected virtual void Initialize(EnemyController e) 
    {
        enemy = e;
    }
    protected virtual NpcStates LogicUpdate() { return this; }
};

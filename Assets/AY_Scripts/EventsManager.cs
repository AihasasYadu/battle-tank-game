using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoSingletonGeneric<EventsManager>
{
    public delegate void Executable();
    public static event Executable EnemyDeath;
    public static event Executable BulletFired;
    public static event Executable PlayerDead;

    public delegate void HealthEvent(int health);
    public static event HealthEvent Health;

    public void EnemyDeathEvent()
    {
        EnemyDeath?.Invoke();
    }

    public void ExecuteHealthEvent(int health)
    {
        Health?.Invoke(health);
    }

    public void ExecuteBulletEvent()
    {
        BulletFired?.Invoke();
    }

    public void PlayerDeathEvent()
    {
        PlayerDead?.Invoke();
    }
}
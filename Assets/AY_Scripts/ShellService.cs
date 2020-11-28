using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellService : MonoSingletonGeneric<ShellService>
{
    [SerializeField] private ShellController shell;
    private Transform tankTransform;
    public Transform SetTankTransform {set  { tankTransform = value;} }

    public void GetShell(int dmg, float speed, TankSides side)
    {
        ShellController shellInstance = Instantiate(shell, tankTransform.position, tankTransform.rotation);
        shellInstance.Initialize(dmg, speed, side);
    }
}

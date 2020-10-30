using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellService : MonoSingletonGeneric<ShellService>
{
    [SerializeField] private ShellController shell;

    public void GetShell(int dmg, float speed)
    {
        Transform temp = TankService.Instance.GetShellPos.transform;
        ShellController shellInstance = Instantiate(shell, temp.position, temp.rotation);
        shellInstance.Initialize(dmg, speed);
    }
}

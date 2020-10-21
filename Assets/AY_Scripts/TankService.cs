using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankService : MonoSingletonGeneric<TankService>
{
    public GameObject tank;
    void Start()
    {
        TankService.Instance.GetTank();
    }

    private void GetTank()
    {
        Instantiate(tank);
    }
}

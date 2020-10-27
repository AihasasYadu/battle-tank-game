using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootButtonController : MonoBehaviour
{
    public TankController tank;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(tank.Shoot);
    }
}

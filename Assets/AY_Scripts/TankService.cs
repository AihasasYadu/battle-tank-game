using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankService : MonoSingletonGeneric<TankService>
{
    [SerializeField] private TankController tank;
    [SerializeField] private Joystick joystick;
    [SerializeField] private Button shootButton;
    [SerializeField] private List<TankTypesScriptable> type;
    [SerializeField] private List<Image> ammo;
    [SerializeField] private Transform lineTarget;
    private TankController currTank;
    void Start()
    {
        GetTank();
    }

    private void GetTank()
    {
        int playerChoice = PlayerPrefs.GetInt("TankSelected");
        currTank = Instantiate(tank);
        currTank.Initialize(type[playerChoice], joystick, shootButton, ammo, lineTarget);
        shootButton.GetComponent<ShootButtonController>().setTank = currTank;
    }
}

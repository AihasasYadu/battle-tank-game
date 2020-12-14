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
    void Start()
    {
        GetTank();
    }

    private void GetTank()
    {
        int playerChoice = PlayerPrefs.GetInt("TankSelected");
        Instantiate(tank).Initialize(type[playerChoice], joystick, shootButton);
    }
}

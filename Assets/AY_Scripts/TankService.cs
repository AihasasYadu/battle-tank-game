using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankService : MonoSingletonGeneric<TankService>
{
    public TankController tank;
    [SerializeField] private Joystick joystick;
    public GameObject shellPosition;
    public List<TankTypesScriptable> type;
    void Start()
    {
        TankService.Instance.GetTank();
    }

    private void GetTank()
    {
        int playerChoice = PlayerPrefs.GetInt("Player's Choice");
        Instantiate(tank).Initialize(type[playerChoice], joystick);
    }
}

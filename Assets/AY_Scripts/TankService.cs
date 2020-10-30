using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankService : MonoSingletonGeneric<TankService>
{
    [SerializeField] private TankController tank;
    [SerializeField] private Joystick joystick;
    [SerializeField] private GameObject shellPosition;
    [SerializeField] private Button shootButton;
    [SerializeField] private List<TankTypesScriptable> type;

    public GameObject GetShellPos { get { return shellPosition; } }
    public GameObject SetShellPos { set {
                                            Vector3 pos = shellPosition.transform.position;
                                            pos.x = value.transform.position.x;
                                            pos.z = value.transform.position.z;
                                            shellPosition.transform.position = pos;
                                            shellPosition.transform.rotation = value.transform.rotation;
                                        } }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public float speed = 10;

    private Joystick joystick;
    private Transform tankParent;
    private float horizontalMove;
    private float verticalMove;
    private Vector3 lookDirection;
    
    void Awake()
    {
        joystick = FindObjectOfType<FixedJoystick>();
        tankParent = transform.parent;
    }
    void Update()
    {
        horizontalMove = joystick.Horizontal * speed;
        verticalMove = joystick.Vertical * speed;
        Vector3 pos = tankParent.position;
        pos.x += horizontalMove * Time.deltaTime;
        pos.z += verticalMove * Time.deltaTime;
        tankParent.position = pos;
        if (joystick.Direction.x != 0 && joystick.Direction.y != 0)
        {
            lookDirection = new Vector3(joystick.Direction.x, 0, joystick.Direction.y);
        }
        transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
    }
}

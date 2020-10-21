using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public float speed = 10;

    private Joystick joystick;
    private float horizontalMove;
    private float verticalMove;
    
    void Awake()
    {
        joystick = FindObjectOfType<FixedJoystick>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = joystick.Horizontal * speed;
        verticalMove = joystick.Vertical * speed;
        if (joystick.Horizontal > 0.2f)
        {
            horizontalMove = 1 * speed;
        }
        else if (joystick.Horizontal < -0.2f)
        {
            horizontalMove = -1 * speed;
        }
        else
        {
            horizontalMove = 0;
        }
        Vector3 pos = transform.position;
        pos.x += horizontalMove * Time.deltaTime;
        pos.z += verticalMove * Time.deltaTime;
        transform.position = pos;
    }
}

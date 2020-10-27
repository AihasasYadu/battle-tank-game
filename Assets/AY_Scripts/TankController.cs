using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoSingletonGeneric<TankController>
{
    public GameObject tankBody;
    public ShellController shell;
    
    /*---Private---*/
    private Joystick joystick;

    /*---Tank Properties---*/
    private TextMesh floatingName;
    private string tankName;
    private float speed;
    private int damage;
    private int health;

    /*---Movement---*/
    private float horizontalMove;
    private float verticalMove;
    private Vector3 lookDirection;
    private Rigidbody rb;
    private Vector3 movement;
    
    public void Initialize(TankTypesScriptable t, Joystick jt)
    {
        tankName = t.tankName;
        speed = t.speed;
        damage = t.damage;
        health = t.health;
        joystick = jt;
        Debug.Log("Player : " + tankName + " Speed : " + speed);
    }
    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        floatingName = GetComponentInChildren<TextMesh>();
        floatingName.text = tankName;
    }

    void Update()
    {
        Movement();
        NameLookAtCamera();
        TankService.Instance.shellPosition = tankBody;
    }

    private void Movement()
    {
        horizontalMove = joystick.Horizontal * speed;
        verticalMove = joystick.Vertical * speed;
        movement = new Vector3(horizontalMove, 0, verticalMove);
        rb.MovePosition(transform.position + movement * Time.deltaTime);
        Turn();
    }

    private void Turn()
    {
        if (joystick.Direction.x != 0 && joystick.Direction.y != 0)
        {
            lookDirection = new Vector3(joystick.Direction.x, 0, joystick.Direction.y);
        }
        tankBody.transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
    }

    private void NameLookAtCamera()
    {
        floatingName.transform.LookAt(Camera.main.transform.position);
        floatingName.transform.Rotate(0, 180, 0);
    }

    public void Shoot()
    {
        ShellController shellObject = Instantiate(shell, TankService.Instance.shellPosition.transform.position, TankService.Instance.shellPosition.transform.rotation);
        Rigidbody shellRB = shellObject.GetComponent<Rigidbody>();
        shellRB.AddForce(TankService.Instance.shellPosition.transform.forward * speed);
    }
}

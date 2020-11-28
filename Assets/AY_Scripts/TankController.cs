﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class TankController : MonoSingletonGeneric<TankController>
{
    [SerializeField] private GameObject tankBody;
    [SerializeField] private TextMesh floatingName;

    /*---Private---*/
    private Joystick joystick;
    private Button shootButton;
    private bool isDead = false;

    /*---Tank Properties---*/
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
    
    public void Initialize(TankTypesScriptable t, Joystick jt, Button b)
    {
        tankName = t.tankName;
        speed = t.speed;
        damage = t.damage;
        health = t.health;
        joystick = jt;
        shootButton = b;
    }
    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        shootButton.onClick.AddListener(ShootShell);
        floatingName.text = tankName;
    }

    void Update()
    {
        if (!isDead)
        {
            Movement();
            NameLookAtCamera();
            TankService.Instance.SetShellPos = tankBody;
        }
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

    public void TakeDamage(int damageDealt)
    {
        health -= damageDealt;
        if (health <= 0)
            PlayerDeath();
    }

    private void PlayerDeath()
    {
        ParticlesService.Instance.GetTankExplosion(transform);
        StartCoroutine("Delay");
        Destroy(tankBody);
        Destroy(floatingName);
        GetComponent<BoxCollider>().enabled = false;
        rb.isKinematic = true;
        isDead = true;
        EnemySpawnerService.Instance.DestroyEnemies();
    }

    private void PauseGame()
    {
        Time.timeScale = 0.1f;
    }

    private void ContinueGame()
    {
        Time.timeScale = 1;
    }

    private IEnumerator Delay()
    {
        PauseGame();
        float seconds = 3;
        float pause = Time.realtimeSinceStartup + seconds;
        Debug.Log("Pause : " + pause);
        while (Time.realtimeSinceStartup < pause)
        {
            yield return 0;
        }
        ContinueGame();
    }

    public void ShootShell()
    {
        ShellService.Instance.GetShell(damage, speed);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class TankController : MonoSingletonGeneric<TankController>, IDamageable
{
    [SerializeField] private GameObject tankBody;
    [SerializeField] private TextMesh floatingName;
    [SerializeField] private GameObject shellPosition;
    private Transform lineTarget;
    private List<Image> ammo;

    /*---Private---*/
    private Joystick joystick;
    private Button shootButton;
    private Image shootButtonImage;
    private bool reloading = false;
    private int magazineSize;
    private float reloadFillRate;
    private float reloadWaitPerStep;
    private const int GROUND_LAYER = 14;

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

    [HideInInspector] public TankSides tankSide;
    public bool isReloading { get { return reloading; } }

    public void Initialize(TankTypesScriptable t, Joystick jt, Button b, List<Image> bullets, Transform lr)
    {
        tankName = t.tankName;
        speed = t.speed;
        damage = t.damage;
        health = t.health;
        joystick = jt;
        shootButton = b;
        ammo = bullets;
        lineTarget = lr;
        EventsManager.Instance.ExecuteHealthEvent(health);
    }
    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        magazineSize = ammo.Count;
        floatingName.text = tankName;
        shootButtonImage = shootButton.GetComponent<Image>();
        tankSide = TankSides.Player;
        reloadFillRate = 0.05f;
        reloadWaitPerStep = 0.02f;
        EventsManager.BulletFired += UpdateMagazine;
    }

    void Update()
    {
        if (tankBody)
        {
            Movement();
            NameLookAtCamera();
        }
    }

    private void Movement()
    {
        horizontalMove = joystick.Horizontal;
        verticalMove = joystick.Vertical;
        movement = new Vector3(horizontalMove, 0, verticalMove);
        rb.MovePosition(transform.position + movement * speed * Time.deltaTime);
        Turn();
    }

    private void Turn()
    {
        if (joystick.Direction.x != 0 && joystick.Direction.y != 0)
        {
            lookDirection = new Vector3(joystick.Direction.x, 0, joystick.Direction.y);
        }
        tankBody.transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        lineTarget.transform.rotation = tankBody.transform.rotation;
        lineTarget.transform.position = shellPosition.transform.position + 100*shellPosition.transform.forward;
    }

    private void NameLookAtCamera()
    {
        floatingName.transform.LookAt(Camera.main.transform.position);
        floatingName.transform.Rotate(0, 180, 0);
    }

    public void TakeDamage(int damageDealt, TankSides tank)
    {
        if (tank == tankSide)
            return;
        health -= damageDealt;
        EventsManager.Instance.ExecuteHealthEvent(health);
        if (health <= 0)
            PlayerDeath();
    }

    private void PlayerDeath()
    {
        ParticlesService.Instance.GetTankExplosion(transform);
        StartCoroutine(Delay());
        Destroy(tankBody);
        rb.isKinematic = true;
        EventsManager.Instance.PlayerDeathEvent();
    }

    private void SlowMoGame()
    {
        Time.timeScale = 0.1f;
    }

    private void ContinueGame()
    {
        Time.timeScale = 1;
    }

    private IEnumerator Delay()
    {
        SlowMoGame();
        float seconds = 3;
        float pause = Time.realtimeSinceStartup + seconds;
        while (Time.realtimeSinceStartup < pause)
        {
            yield return 0;
        }
        ContinueGame();
    }

    public void ShootShell()
    {
        if(tankBody && !reloading)
        {
            ShellService.Instance.SetTankTransform = shellPosition.transform;
            ShellService.Instance.GetShell(damage, speed, TankSides.Player);
            EventsManager.Instance.ExecuteBulletEvent();
        }
    }

    private void UpdateMagazine()
    {
        for(int i = 0; i < magazineSize ; i++)
        {
            if(ammo[i].IsActive())
            {
                ammo[i].enabled = false;
                break;
            }
        }

        if (!ammo[magazineSize - 1].IsActive())
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        int i = 0;
        shootButtonImage.fillAmount = 0;
        reloading = true;
        while(shootButtonImage.fillAmount != 1)
        {
            shootButtonImage.fillAmount += reloadFillRate;
            yield return new WaitForSeconds(reloadWaitPerStep);
            if (i < magazineSize)
            {
                ammo[i].enabled = true;
                i++;
            }
        }
        reloading = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour, IDamageable
{
    private NpcStates currentState;
    private int obstacleLayer = 8;
    private int destructablesLayer = 9;
    private int playerLayer = 12;

    /*---Tank Properties---*/
    private string tankName;
    private int health;
    [HideInInspector] public float speed;
    [HideInInspector] public int damage;

    /*---Public Variables---*/
    public GameObject shellPosition;
    public TextMesh floatingName;
    public MeshFilter turret;

    [HideInInspector] public Vector3 newPosition;
    [HideInInspector] public Transform playerTransform;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public PatrollingState patrolling;
    [HideInInspector] public AttackingState attacking;
    [HideInInspector] public TankSides tankSide;

    public void Initialize(TankTypesScriptable t)
    {
        tankName = t.tankName;
        speed = t.speed / 2;
        damage = t.damage;
        health = t.health;
    }

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        floatingName.text = tankName;
        tankSide = TankSides.Enemy;
        patrolling = GetComponent<PatrollingState>();
        attacking = GetComponent<AttackingState>();
        currentState = patrolling;
        SetNewCoordinates();
    }

    private void Update()
    {
        currentState = currentState.Enter(this);
    }

    public void TakeDamage(int damageDealt, TankSides tank)
    {
        if (tank == tankSide)
            return;
        health -= damageDealt;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetNewCoordinates()
    {
        int randX = 0, randZ = 0;
        do
        {
            randX = Random.Range(-1, 2);
            randZ = Random.Range(-1, 2);
        } while (randX == 0 || randZ == 0);
        newPosition = new Vector3(randX * speed, 0, randZ * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer.Equals(obstacleLayer) || collision.gameObject.layer.Equals(destructablesLayer))
        {
            SetNewCoordinates();
        }

        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage, tankSide);
            SetNewCoordinates();
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.layer.Equals(playerLayer))
        {
            playerTransform = collision.transform;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer.Equals(playerLayer))
        {
            playerTransform = null;
        }
    }
}
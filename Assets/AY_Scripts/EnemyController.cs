using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private TextMesh floatingName;
    
    private int obstacleLayer = 8;
    private int destructablesLayer = 9;
    private int playerLayer = 12;
    private Vector3 newPosition;
    private Rigidbody rb;

    /*---Tank Properties---*/
    private string tankName;
    private float speed;
    private int damage;
    private int health;
    public void Initialize(TankTypesScriptable t)
    {
        tankName = t.tankName;
        speed = t.speed;
        damage = t.damage;
        health = t.health;
    }
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        floatingName.text = tankName;
        SetNewCoordinates();
    }

    void Update()
    {
        Movement();
        NameLookAtCamera();
    }

    private void Movement()
    {
        rb.MovePosition(transform.position + newPosition * Time.deltaTime);
        Turn();
    }

    private void Turn()
    {
        transform.rotation = Quaternion.LookRotation(newPosition, Vector3.up);
    }

    private void NameLookAtCamera()
    {
        floatingName.transform.LookAt(Camera.main.transform.position);
        floatingName.transform.Rotate(0, 180, 0);
    }

    private void SetNewCoordinates()
    {
        int randX = 0, randZ = 0;
        do
        {
            randX = Random.Range(-1, 2);
            randZ = Random.Range(-1, 2);
        } while (randX == 0 || randZ == 0);
        newPosition = new Vector3(randX * speed, 0, randZ * speed);
    }

    public void TakeDamage(int damageDealt)
    {
        health -= damageDealt;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer.Equals(obstacleLayer) || collision.gameObject.layer.Equals(destructablesLayer))
        {
            SetNewCoordinates();
        }
        if(collision.gameObject.layer.Equals(playerLayer))
        {
            collision.gameObject.GetComponent<TankController>().TakeDamage(damage);
            SetNewCoordinates();
        }
    }
}

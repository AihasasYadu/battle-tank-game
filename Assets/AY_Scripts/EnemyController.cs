using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private int obstacleLayer = 8;
    private int destructablesLayer = 9;
    private Vector3 newPosition;
    private Rigidbody rb;
    private TextMesh floatingName;

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
        Debug.Log("Enemy : " + tankName + " Speed : " + speed);
    }
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        floatingName = GetComponentInChildren<TextMesh>();
        floatingName.text = tankName;
        RandomCoordinates();
    }

    void Update()
    {
        Movement();
        NameLookAtCamera();
    }

    private void Movement()
    {
        rb.MovePosition(transform.position + newPosition * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(newPosition, Vector3.up);
    }

    private void NameLookAtCamera()
    {
        floatingName.transform.LookAt(Camera.main.transform.position);
        floatingName.transform.Rotate(0, 180, 0);
    }

    private void RandomCoordinates()
    {
        int randX = 0, randZ = 0;
        do
        {
            randX = Random.Range(-1, 2);
            randZ = Random.Range(-1, 2);
        } while (randX == 0 || randZ == 0);
        newPosition = new Vector3(randX * speed, 0, randZ * speed);
    }

    public void Damage(int damageDealt)
    {
        if (health >= 0)
            health -= damageDealt;
        else
            Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer.Equals(obstacleLayer) || collision.gameObject.layer.Equals(destructablesLayer))
        {
            RandomCoordinates();
        }
    }
}

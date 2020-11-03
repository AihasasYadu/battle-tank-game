using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShellController : MonoBehaviour
{
    public int shellDamage;
    public ParticleSystem explosion;
    private int enemyLayer = 10;
    private int shellLimitLayer = 13;
    private int launchForce;
    private Rigidbody rb;
 
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Movement();
    }
    private void Movement()
    {
        rb.velocity = launchForce * transform.forward;
        Debug.Log("Force : " + launchForce);
    }
    public void Initialize(int dmg, float speed)
    {
        shellDamage = dmg;
        launchForce = (int)(speed + (speed/2));
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer.Equals(enemyLayer))
        {
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(shellDamage);
            ParticleSystem explodeInstance = Instantiate(explosion, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            explodeInstance.Play();
            launchForce = 0;
            Destroy(gameObject);
        }
        if(collision.gameObject.layer.Equals(shellLimitLayer))
        {
            Destroy(gameObject);
        }
    }
}

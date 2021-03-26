using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShellController : MonoBehaviour
{
    private int shellDamage;
    private int enemyLayer = 10;
    private int playerLayer = 12;
    private const int SHELL_LIMIT_LAYER = 13;
    private int launchForce;
    private TankSides tankSide;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        DestroyAfterSeconds(5f);
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
    public void Initialize(int dmg, float speed, TankSides side)
    {
        shellDamage = dmg;
        launchForce = (int)(speed + (speed/2));
        tankSide = side;
    }
    private void DestroyAfterSeconds(float time)
    {
        Destroy(gameObject, time);
    }
    private void OnCollisionEnter(Collision collision)
    {
        /*if (collision.gameObject.layer.Equals(playerLayer) && tankSide == enemyLayer)
        {
            collision.gameObject.GetComponent<TankController>().TakeDamage(shellDamage);
            ParticlesService.Instance.GetShellExplosion(collision.gameObject.transform);
            launchForce = 0;
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer.Equals(enemyLayer) && tankSide == playerLayer)
        {
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(shellDamage);
            ParticlesService.Instance.GetShellExplosion(collision.gameObject.transform);
            launchForce = 0;
            Destroy(gameObject);
        }*/

        if (collision.gameObject.layer.Equals(SHELL_LIMIT_LAYER))
        {
            Destroy(gameObject);
        }

        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if(damageable != null)
        {
            damageable.TakeDamage(shellDamage, tankSide);
            ParticlesService.Instance.GetShellExplosion(collision.gameObject.transform);
            launchForce = 0;
            Destroy(gameObject);
        }
    }
}

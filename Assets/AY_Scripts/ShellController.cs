using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    public int shellDamage;
    public ParticleSystem explosion;
    private int enemyLayer = 10;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer.Equals(enemyLayer))
        {
            collision.gameObject.GetComponent<EnemyController>().Damage(shellDamage);
            explosion.Play();
            Destroy(gameObject, 2f);
        }
    }
}

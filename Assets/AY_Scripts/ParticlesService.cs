using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesService : MonoSingletonGeneric<ParticlesService>
{
    [SerializeField] private ParticleSystem tankExplosion;
    [SerializeField] private ParticleSystem shellExplosion;

    public void GetTankExplosion(Transform tr)
    {
        ParticleSystem ps = Instantiate(tankExplosion, tr.position, tr.rotation);
        ps.Play();
        Destroy(ps.gameObject, 3f);
    }

    public void GetShellExplosion(Transform tr)
    {
        ParticleSystem ps = Instantiate(shellExplosion, tr.position, tr.rotation);
        ps.Play();
        Destroy(ps.gameObject, 3f);
    }
}

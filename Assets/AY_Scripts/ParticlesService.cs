using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public enum ParticleType
{
    TankExplosion,
    ShellExplosion
}
[System.Serializable]
public struct Explosion
{
    public ParticleType explosionType;
    public ParticleSystem explosionParticle;
}

public class ParticlesService : MonoSingletonGeneric<ParticlesService>
{
    [SerializeField] private List<Explosion> explosionList;
    private Transform instTransform;

    private ParticleType particle;

    private void GetExplosion()
    {
        ParticleSystem ps = Instantiate(explosionList[(int)particle].explosionParticle, instTransform.position, instTransform.rotation);
        ps.Play();
        Destroy(ps.gameObject, 3f);
    }

    public void GetTankExplosion(Transform tr)
    {
        particle = ParticleType.TankExplosion;
        instTransform = tr;
        GetExplosion();
    }

    public void GetShellExplosion(Transform tr)
    {
        particle = ParticleType.ShellExplosion;
        instTransform = tr;
        GetExplosion();
    }
}

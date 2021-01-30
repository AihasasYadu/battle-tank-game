using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPoolController : MonoSingletonGeneric<ObjectPoolController>
{
    [SerializeField] private int poolSize;
    protected GameObject prefab;
    protected Queue<GameObject> pool;

    protected virtual void Start()
    {
        for(int i = 0; i < poolSize ; i++)
        {
            pool.Enqueue(prefab);
        }
    }
}

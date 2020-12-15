using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemySpawnerService : MonoSingletonGeneric<EnemySpawnerService>
{
    [SerializeField] private EnemyController enemy;
    [SerializeField] private GameObject enemyPositioner;
    [SerializeField] private Transform minX;
    [SerializeField] private Transform maxX;
    [SerializeField] private Transform minZ;
    [SerializeField] private Transform maxZ;
    [SerializeField] private List<TankTypesScriptable> enemyType;
    private List<EnemyController> enemyList;
    private void Start()
    {
       enemyList  = new List<EnemyController>();
        EventsManager.PlayerDead += DestroyEnemies;
        EventsManager.EnemyDeath += RemoveEmptyElements;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        int enumCount = 0;
        foreach (TankType type in Enum.GetValues(typeof(TankType)))
        {
            enumCount++;
        }
        int randomIndex = UnityEngine.Random.Range(0, enumCount - 1);
        Transform randomPos = GetRandomCoordinates();
        EnemyController enemyInstance = Instantiate(enemy, randomPos.position, Quaternion.identity);
        enemyInstance.Initialize(enemyType[randomIndex]);
        enemyList.Add(enemyInstance);
    }
    private Transform GetRandomCoordinates()
    {
        float x1 = minX.position.x;
        float x2 = maxX.position.x;
        float z1 = minZ.position.z;
        float z2 = maxZ.position.z;
        Transform temp = enemyPositioner.transform;
        temp.position = new Vector3(UnityEngine.Random.Range(x1, x2), 0, UnityEngine.Random.Range(z1, z2));
        enemyPositioner.transform.position = temp.position;
        return temp;
    }

    public void DestroyEnemies()
    {
        StartCoroutine(DestroyEnemyDelay());
    }

    private IEnumerator DestroyEnemyDelay()
    {
        while(enemyList.Count != 0)
        {
            Destroy(enemyList[0].gameObject);
            enemyList.RemoveAt(0);
            yield return new WaitForSeconds(1);
        }
    }

    public void RemoveEmptyElements()
    {
        int len = enemyList.Count;
        for(int i = 0 ; i < len ; i++)
        {
            if (enemyList[i] == null)
            {
                enemyList.RemoveAt(i);
                len--;
            }
        }
    }
}
